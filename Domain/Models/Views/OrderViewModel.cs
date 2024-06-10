﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Recipient { get; set; }

        public double Amount { get; set; }

        public string? PaymentMethod { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreateAt { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();
    }
}