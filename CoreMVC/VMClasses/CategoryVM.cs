﻿using CoreMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.VMClasses
{
    public class CategoryVM
    {
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
    }
}
