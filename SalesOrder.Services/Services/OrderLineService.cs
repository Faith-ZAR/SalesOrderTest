using SalesOrder.Data.Interfaces;
using SalesOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Services.Services
{
    public class OrderLineService
    {
        private readonly IRepository<OrderLine> _repository;

        public OrderLineService(IRepository<OrderLine> repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrderLine> GetAllOrderLines()
        {
            return _repository.GetAll();
        }

        public OrderLine GetOrderLine(int id)
        {
            return _repository.GetById(id);
        }

        public void AddOrderLine(OrderLine header)
        {
            _repository.Add(header);
        }

        public void UpdateOrderLine(OrderLine header)
        {
            _repository.Add(header);
        }

        public void DeleteOrderLine(OrderLine header)
        {
            _repository.Add(header);
        }
    }
}
