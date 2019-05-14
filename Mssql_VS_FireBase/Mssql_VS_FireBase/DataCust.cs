using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mssql_VS_FireBase
{
    class DataCust
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name:{Name}, Country: {Country}, Age:{Age} ";
        }
    }
}
