using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql_VS_FireBase
{
    class Program
    {
        static void Main(string[] args)
        {
            ////////////Work with MSSQL:

            DAOMSSQLProvider providor = new DAOMSSQLProvider();

            Customer c = new Customer(5, "ddd", "israel", 34);
            providor.AddCustomer(c);

            Order o = new Order(2, 1, 300, 050519);
            providor.AddOrder(o);

            providor.GetAllCustomers();
            providor.GetAllOrders();
            providor.GetAllOrdersByCustomerId(1);
            providor.GetCustomerByID(2);
            providor.GetOrderById(1);
            // providor.RemoveCustomer(2);
            // providor.RemoveOrder(1);
            providor.UpdateCustomer(c);

            ////////////Work with Fire Base:

            DAOFireBaseProvider DBprovidor = new DAOFireBaseProvider();

            Customer f = new Customer(5, "rrr", "usa", 22);
            Order e = new Order(7, 1, 445, 050519);

            Order g= new Order(8, 1, 666, 777);

            DBprovidor.AddCustomer(f);
           // DBprovidor.AddOrder(e);
              DBprovidor.GetAllCustomers();
              DBprovidor.GetAllOrders();
             DBprovidor.GetAllOrderCustomer();
              DBprovidor.GetAllOrdersByCustomerId(5);
            DBprovidor.RemoveCustomer(10);
            DBprovidor.RemoveOrder(2);
            DBprovidor.UpdateOrder(g);

        }
    }
}
