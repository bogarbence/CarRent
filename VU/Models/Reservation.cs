using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VU.Objects
{
    class Reservation
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Car choosenCar { get; set; }
        public string comment { get; set; }
        public int choosenCarID { get; set; }
    }
}
