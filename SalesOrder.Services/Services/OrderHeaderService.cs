using SalesOrder.Data.Interfaces;
using SalesOrder.Data.Models;
using SalesOrder.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Services.Services
{
    public class OrderHeaderService
    { 
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IXmlFileHandler _fileHandler;

        public OrderHeaderService(ISalesOrderRepository salesOrderRepository, IXmlFileHandler fileHandler)
        {
            _salesOrderRepository = salesOrderRepository;
            _fileHandler = fileHandler;
        }

        public IEnumerable<OrderHeader> GetAllOrderHeaders()
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            return salesOrders.salesOrderList.Select(x => x.orderHeader).ToList();
        }

        public OrderHeader GetOrderHeader(int orderHeaderId)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            return salesOrders.salesOrderList.FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderHeaderId)?.orderHeader;
        }

        public OrderHeader AddOrderHeader(OrderHeader orderHeader)
        {
            if (orderHeader.OrderHeaderId > 0) return orderHeader;

            var salesOrders = _salesOrderRepository.GetSalesOrders();
            int orderHeaderId = 1;

            if (salesOrders.salesOrderList.Any())
            {
                var lastSalesOrder = salesOrders.salesOrderList.OrderByDescending(x => x.orderHeader.OrderHeaderId).First();
                orderHeaderId = lastSalesOrder.orderHeader.OrderHeaderId + 1;
            }

            orderHeader.OrderHeaderId = orderHeaderId;
            var salesOrder = new Data.Models.SalesOrder { orderHeader = orderHeader };
            salesOrders.salesOrderList.Add(salesOrder);

            _fileHandler.SaveSalesOrders(salesOrders);
            return orderHeader;
        }

        public OrderHeader UpdateOrderHeader(OrderHeader orderHeader)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();

            var salesOrder = salesOrders.salesOrderList
                .FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderHeader.OrderHeaderId);
            if (salesOrder == null) return orderHeader;

            salesOrders.salesOrderList.Remove(salesOrder);
            salesOrder.orderHeader = orderHeader;
            salesOrders.salesOrderList.Add(salesOrder);

            _fileHandler.SaveSalesOrders(salesOrders);
            return orderHeader;
        }

        public void DeleteOrderHeader(OrderHeader orderHeader)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            var salesOrder = salesOrders.salesOrderList
                .FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderHeader.OrderHeaderId);
            if (salesOrder == null) return;

            salesOrders.salesOrderList.Remove(salesOrder);
            _fileHandler.SaveSalesOrders(salesOrders);
        }

    }
}
