﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MembershipTransaction
{
    public Guid Id { get; set; }

    public Guid MembershipId { get; set; }

    public Guid StoreOwnerId { get; set; }

    public string Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Membership Membership { get; set; }

    public virtual StoreOwner StoreOwner { get; set; }
}