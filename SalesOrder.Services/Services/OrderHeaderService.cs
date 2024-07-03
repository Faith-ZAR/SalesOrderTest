using SalesOrder.Data.Interfaces;
using SalesOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Services.Services
{
    public class OrderHeaderService
    {
        private readonly IRepository<OrderHeader> _repository;
        private SalesOrderList _salesOrders;

        public OrderHeaderService(IRepository<OrderHeader> repository, SalesOrderList salesOrderList)
        {
            _repository = repository;
            _salesOrders = salesOrderList;
        }

        public IEnumerable<OrderHeader> GetAllOrderHeaders()
        {
            List<OrderHeader> orderHeaderList = new List<OrderHeader>();
            try
            {
                if (_salesOrders.salesOrderList != null)
                {
                    foreach (var salesOrder in _salesOrders.salesOrderList)
                    {
                        orderHeaderList.Add(salesOrder.orderHeader);
                    }

                }
                return orderHeaderList;
            }
            catch (Exception)
            {

                return orderHeaderList;
            }
        }

        public OrderHeader GetOrderHeader(int id)
        {
            OrderHeader orderHeader = new OrderHeader();
            try
            {
                if (_salesOrders.salesOrderList.Any(x => x.orderHeader.OrderHeaderId == id))
                {
                    var salesOrder = _salesOrders.salesOrderList.FirstOrDefault(x => x.orderHeader.OrderHeaderId == id);
                    if (salesOrder != null)
                    {
                        orderHeader = salesOrder.orderHeader;
                    }
                }
                return orderHeader;
            }
            catch (Exception)
            {
                return orderHeader;
            }
        }

        public OrderHeader AddOrderHeader(OrderHeader orderHeader)
        {
            try
            {
                if (orderHeader.OrderHeaderId <= 0)
                {
                    int OrderHeaderId = 1;
                    Data.Models.SalesOrder lastSalesOrder = new Data.Models.SalesOrder();
                    if (_salesOrders != null && _salesOrders.salesOrderList != null && _salesOrders.salesOrderList.Count() > 0)
                    {
                        lastSalesOrder = _salesOrders.salesOrderList.OrderByDescending(x => x.orderHeader.OrderHeaderId).First();
                        OrderHeaderId = lastSalesOrder.orderHeader.OrderHeaderId + 1;
                    }

                    orderHeader.OrderHeaderId = OrderHeaderId;
                    Data.Models.SalesOrder salesOrder = new Data.Models.SalesOrder()
                    {
                        orderHeader = orderHeader
                    };

                    _salesOrders.salesOrderList.Add(salesOrder);
                    //SaveXmlSalesOrders(SalesOrders);
                }
                return orderHeader;
            }
            catch (Exception)
            {
                return orderHeader;
            }
        }

        public OrderHeader UpdateOrderHeader(OrderHeader orderHeader)
        {
            try
            {
                if (orderHeader.OrderHeaderId >= 0 && _salesOrders.salesOrderList.Any(x => x.orderHeader.OrderHeaderId == orderHeader.OrderHeaderId))
                {
                    Data.Models.SalesOrder salesOrder = _salesOrders.salesOrderList.FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderHeader.OrderHeaderId);
                    _salesOrders.salesOrderList.Remove(salesOrder);
                    salesOrder.orderHeader = orderHeader;
                    _salesOrders.salesOrderList.Add(salesOrder);
                    //SaveXmlSalesOrders(SalesOrders);

                }
                return orderHeader;
            }
            catch (Exception exception)
            {
                return orderHeader;
            }
        }

        public void DeleteOrderHeader(OrderHeader orderHeader)
        {
            try
            {
                if (orderHeader.OrderHeaderId >= 0 && _salesOrders.salesOrderList.Any(x => x.orderHeader.OrderHeaderId == orderHeader.OrderHeaderId))
                {
                    Data.Models.SalesOrder salesOrder = _salesOrders.salesOrderList.FirstOrDefault(x => x.orderHeader.OrderHeaderId == orderHeader.OrderHeaderId);
                    _salesOrders.salesOrderList.Remove(salesOrder);
                    //SaveXmlSalesOrders(_salesOrders);

                }
            }
            catch (Exception)
            {

                return; ;
            }
        }
    }
}
