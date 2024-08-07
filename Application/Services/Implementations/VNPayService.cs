﻿using Application.Services.Interfaces;
using AutoMapper;
using Common.Constants;
using Common.Helpers;
using Data;
using Data.Repositories.Interfaces;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.VNPay;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations
{
    public class VnPayService(IUnitOfWork unitOfWork, IMapper mapper) : BaseService(unitOfWork, mapper), IVNPayService
    {
        private readonly ITransactionRepository _transactionRepository = unitOfWork.Transaction;
        private readonly IOrderRepository _orderRepository = unitOfWork.Order;
        private void UpdateFailedTransaction(ICollection<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                transaction.Status = TransactionStatuses.Failed;
            }
            _transactionRepository.UpdateRange(transactions);
        }

        public async Task<bool> AddRequest(Guid userId, Guid orderId, VnPayRequestModel model)
        {
            var oldTransactions = await _transactionRepository.Where(tr => tr.CustomerId.Equals(userId) && tr.OrderId.Equals(orderId)).ToListAsync();

            if (oldTransactions.Any())
            {
                UpdateFailedTransaction(oldTransactions);
            }

            var transaction = new Transaction
            {
                Amount = model.Amount,
                CreateAt = DateTimeHelper.VnNow,
                CustomerId = userId,
                OrderId = orderId,
                Status = TransactionStatuses.Pending,
                Id = model.TxnRef,
            };
            _transactionRepository.Add(transaction);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> AddResponse(VnPayResponseModel model)
        {
            var transaction = await _transactionRepository.Where(transaction => transaction.Id.Equals(model.TxnRef)).FirstOrDefaultAsync();
            if (transaction != null)
            {
                if (model.TransactionStatus.Equals("02"))
                {
                    transaction.Status = TransactionStatuses.Canceled;
                    _transactionRepository.Update(transaction);
                    await _unitOfWork.SaveChangesAsync();
                }
                if (model.TransactionStatus.Equals("00") && transaction.Status != TransactionStatuses.Successful)
                {
                    transaction.Status = TransactionStatuses.Successful;
                    _transactionRepository.Update(transaction);

                    var order = await _orderRepository.Where(x => x.Id.Equals(transaction.OrderId)).FirstOrDefaultAsync();

                    if (order != null)
                    {
                        order.Status = OrderStatuses.PAID;
                        order.IsPayment = true;
                        _orderRepository.Update(order);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
