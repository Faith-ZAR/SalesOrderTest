using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Data.Models
{
    public class SalesOrder
    {
        public OrderHeader orderHeader;

        public List<OrderLine> orderLineList;
    }
}
