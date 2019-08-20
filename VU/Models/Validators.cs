using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VU.Models
{
    class Validators
    {
        public bool IsNullOrEmpty(string value)
        {
            if(value == null)
            {
                return true;
            }
            if(value == "")
            {
                return true;
            }
            return false;
        }
        public bool IsNumber(string value)
        {
            int n;
            bool isNumeric = int.TryParse(value, out n);
            return isNumeric;
        }
    }
}
