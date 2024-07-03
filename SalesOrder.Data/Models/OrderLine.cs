using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Data.Models
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public int OrderHeaderId { get; set; }
        public int LineNumber { get; set; }
        public string ProductCode { get; set; }
        public string ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public float CostPrice { get; set; }
        public float SalesPrice { get; set; }
        public int Quantity { get; set; }
        public bool isDeleted { get; set; }
    }
}
