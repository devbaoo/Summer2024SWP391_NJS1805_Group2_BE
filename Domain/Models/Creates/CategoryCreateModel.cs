﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Creates
{
    public class CategoryCreateModel
    {
/*        public int Id { get; set; }
*/        public string Name { get; set; } = null!;

        public string TargetAudience { get; set; } = null!;

        public string AgeRange { get; set; } = null!;

        public string MilkType { get; set; } = null!;

        public string? Icon { get; set; }
    }
}
