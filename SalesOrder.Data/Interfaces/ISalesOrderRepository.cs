using SalesOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Data.Interfaces
{
    public interface ISalesOrderRepository
    {
        SalesOrders GetSalesOrders();
    }
}
