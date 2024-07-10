using Dapper;
using SalesOrder.Data.Interfaces;
using SalesOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Services.Repositories
{
    public class OrderHeaderRepository : IRepository<OrderHeader>
    {
        private readonly IDataSource _dataSource;

        public OrderHeaderRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        public void Add(OrderHeader orderHeader)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderNumber", orderHeader.OrderNumber);
            parameters.Add("@OrderType", orderHeader.OrderType);
            parameters.Add("@OrderTypeId", orderHeader.OrderTypeId);
            parameters.Add("@CustomerName", orderHeader.CustomerName);
            parameters.Add("@CreateDate", orderHeader.CreateDate);
            parameters.Add("@OrderStatusId", orderHeader.OrderStatusId);
            parameters.Add("@OrderStatus", orderHeader.OrderStatus);

            _dataSource.Connection.Execute("STP_ORDERHEADER_ADD", parameters, commandType: CommandType.StoredProcedure);
        }

        public void Delete(OrderHeader orderHeader)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderHeaderId", orderHeader.OrderHeaderId);
            _dataSource.Connection.Execute("STP_ORDERHEADER_DELETE", parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<OrderHeader> GetAll()
        {
            return _dataSource.Connection.Query<OrderHeader>("STP_ORDERHEADER", commandType: CommandType.StoredProcedure).ToList();
        }

        public OrderHeader GetById(int orderHeaderId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderHeaderId", orderHeaderId);
            return _dataSource.Connection.Query<OrderHeader>("STP_ORDERHEADER", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public void Update(OrderHeader orderHeader)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderHeaderId", orderHeader.OrderHeaderId);
            parameters.Add("@OrderNumber", orderHeader.OrderNumber);
            parameters.Add("@OrderType", orderHeader.OrderType);
            parameters.Add("@OrderTypeId", orderHeader.OrderTypeId);
            parameters.Add("@CustomerName", orderHeader.CustomerName);
            parameters.Add("@CreateDate", orderHeader.CreateDate);
            parameters.Add("@OrderStatusId", orderHeader.OrderStatusId);
            parameters.Add("@OrderStatus", orderHeader.OrderStatus);

            _dataSource.Connection.Execute("STP_ORDERHEADER_UPDATE", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
