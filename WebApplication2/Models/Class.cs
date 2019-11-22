using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Company
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string iexId { get; set; }
        public string isEnabled{ get; set; }
    }
}
