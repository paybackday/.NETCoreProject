using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Models.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public int CategoryID { get; set; }
        //RelationalProperties
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
