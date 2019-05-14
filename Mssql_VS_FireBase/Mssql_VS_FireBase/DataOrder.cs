using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql_VS_FireBase
{
    class DataOrder
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Price { get; set; }
        public int Date { get; set; }

        public override string ToString()
        {
            return $"Id :{Id}, Customer_Id:{Customer_Id}, Price:{Price}, Date:{Date} ";
        }
    }
}
