using SalesOrder.Data.Interfaces;
using SalesOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SalesOrder.Services.Repositories
{
    public class XmlFileHandlerRepository : IXmlFileHandler
    {
        private readonly string _filePath;

        public XmlFileHandlerRepository()
        {
            // Construct the file path dynamically
            string currentDirectory = Directory.GetCurrentDirectory();
            _filePath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\SalesOrderTest\SalesOrderTest\DBSource\SalesOrderList.xml"));
        }

        public SalesOrders LoadSalesOrders()
        {
            var salesOrders = new SalesOrders();
            try
            {
                if (!File.Exists(_filePath)) return salesOrders;

                var serializer = new XmlSerializer(typeof(SalesOrders));
                using (var stream = new FileStream(_filePath, FileMode.Open))
                {
                    salesOrders = (SalesOrders)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred while loading sales orders: {ex.Message}");
            }
            return salesOrders;
        }

        public void SaveSalesOrders(SalesOrders salesOrders)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(SalesOrders));
                using (var writer = new StreamWriter(_filePath))
                {
                    serializer.Serialize(writer, salesOrders);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred while saving sales orders: {ex.Message}");
            }
        }
    }
}
