﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Customer
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Phone { get; set; }

    public string Name { get; set; }

    public string AvatarUrl { get; set; }

    public string Rank { get; set; }

    public string Status { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderTransaction> OrderTransactions { get; set; } = new List<OrderTransaction>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}