using CoreMVC.Models.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models.Entities
{
    public class Employee:BaseEntity
    {
        public string FirstName { get; set; }
        public UserRole UserRole { get; set; }
        //Relational Properties

        public IList<Order> Orders { get; set; }

    }
}
