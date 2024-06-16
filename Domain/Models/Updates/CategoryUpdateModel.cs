﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Updates
{
    public class CategoryUpdateModel
    {
        public string? Name { get; set; }
        public string TargetAudience { get; set; } = null!;

        public string AgeRange { get; set; } = null!;

        public string MilkType { get; set; } = null!;

        public string? Icon { get; set; }
    }
}
