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
    public class OrderLineRepository : IRepository<OrderLine>
    {
        private readonly IDataSource _dataSource;

        public OrderLineRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public void Add(OrderLine orderLine)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ProductCode", orderLine.ProductCode);
            parameters.Add("@ProductType", orderLine.ProductType);
            parameters.Add("@ProductTypeId", orderLine.ProductTypeId);
            parameters.Add("@CostPrice", orderLine.CostPrice);
            parameters.Add("@SalesPrice", orderLine.SalesPrice);
            parameters.Add("@Quantity", orderLine.Quantity);
            parameters.Add("@OrderHeaderId", orderLine.OrderHeaderId);
            parameters.Add("@LineNumber", orderLine.LineNumber);
            _dataSource.Connection.Execute("STP_ORDERLINE_ADD", parameters, commandType: CommandType.StoredProcedure);
        }

        public void Delete(OrderLine orderLine)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderLineId", orderLine.OrderLineId);
            _dataSource.Connection.Execute("STP_ORDERLINE_DELETE", parameters, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<OrderLine> GetAll()
        {
            return _dataSource.Connection.Query<OrderLine>("STP_ORDERLINE_LIST", commandType: CommandType.StoredProcedure).ToList();
        }

        public OrderLine GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderLineId", id);
            return _dataSource.Connection.Query<OrderLine>("STP_ORDERLINE", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public void Update(OrderLine orderLine)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@OrderLineId", orderLine.OrderLineId);
            parameters.Add("@OrderHeaderId", orderLine.OrderHeaderId);
            parameters.Add("@ProductCode", orderLine.ProductCode);
            parameters.Add("@ProductType", orderLine.ProductType);
            parameters.Add("@ProductTypeId", orderLine.ProductTypeId);
            parameters.Add("@CostPrice", orderLine.CostPrice);
            parameters.Add("@SalesPrice", orderLine.SalesPrice);
            parameters.Add("@Quantity", orderLine.Quantity);
            _dataSource.Connection.Execute("STP_ORDERLINE_UPDATE", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
