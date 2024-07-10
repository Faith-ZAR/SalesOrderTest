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
    public class OrderLineService
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IXmlFileHandler _fileHandler;

        public OrderLineService(ISalesOrderRepository salesOrderRepository, IXmlFileHandler fileHandler)
        {
            _salesOrderRepository = salesOrderRepository;
            _fileHandler = fileHandler;
        }

        public OrderLine GetOrderLine(int orderLineId)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            return salesOrders.salesOrderList
                .SelectMany(x => x.orderLineList)
                .FirstOrDefault(x => x.OrderLineId == orderLineId);
        }

        public IEnumerable<OrderLine> GetOrderLines(int orderHeaderId)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            var salesOrder = salesOrders.salesOrderList
                .FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderHeaderId);
            return salesOrder?.orderLineList;
        }

        public OrderLine AddOrderLine(OrderLine orderLine)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            var salesOrder = salesOrders.salesOrderList
                .FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderLine.OrderHeaderId);
            if (salesOrder == null) return orderLine;

            int orderLineId = salesOrders.salesOrderList.SelectMany(x => x.orderLineList).Max(x => x.OrderLineId) + 1;
            int lineNumber = salesOrder.orderLineList.Any()
                ? salesOrder.orderLineList.Max(x => x.LineNumber) + 1
                : 1;

            orderLine.OrderLineId = orderLineId;
            orderLine.LineNumber = lineNumber;

            salesOrder.orderLineList.Add(orderLine);
            _fileHandler.SaveSalesOrders(salesOrders);
            return orderLine;
        }

        public OrderLine UpdateOrderLine(OrderLine orderLine)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            var salesOrder = salesOrders.salesOrderList
                .FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderLine.OrderHeaderId);
            if (salesOrder == null) return orderLine;

            var existingOrderLine = salesOrder.orderLineList
                .FirstOrDefault(x => x.OrderLineId == orderLine.OrderLineId);
            if (existingOrderLine == null) return orderLine;

            salesOrder.orderLineList.Remove(existingOrderLine);
            salesOrder.orderLineList.Add(orderLine);

            _fileHandler.SaveSalesOrders(salesOrders);
            return orderLine;
        }

        public void DeleteOrderLine(OrderLine orderLine)
        {
            var salesOrders = _salesOrderRepository.GetSalesOrders();
            var salesOrder = salesOrders.salesOrderList
                .FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderLine.OrderHeaderId);
            if (salesOrder == null) return;

            var existingOrderLine = salesOrder.orderLineList
                .FirstOrDefault(x => x.OrderLineId == orderLine.OrderLineId);
            if (existingOrderLine == null) return;

            salesOrder.orderLineList.Remove(existingOrderLine);
            _fileHandler.SaveSalesOrders(salesOrders);
        }
    }
}
