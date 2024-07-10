using SalesOrder.Data.Interfaces;
using SalesOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Services.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly IXmlFileHandler _xmlFileHandler;

        public SalesOrderRepository(IXmlFileHandler fileHandler)
        {
            _xmlFileHandler = fileHandler;
        }

        public SalesOrders GetSalesOrders()
        {
            return _xmlFileHandler.LoadSalesOrders();
        }
    }
}