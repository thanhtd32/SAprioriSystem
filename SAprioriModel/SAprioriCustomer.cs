using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// This class is SAprioriCustomer
    /// 1 SAprioriCustomer  has many SAprioriOrder
    /// Data set Customer.json sample
    /// </summary>
    public class SAprioriCustomer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public List<SAprioriOrder> Orders { get; set; }
    }
}
