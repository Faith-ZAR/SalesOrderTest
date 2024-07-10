using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrder.Data.Interfaces
{
    public interface IDataSource
    {
        IDbConnection Connection { get; }
    }
}
