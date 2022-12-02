using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// The SAprioriEmployee class 
    /// 1 SAprioriEmployee  has many SAprioriOrder
    /// Data set Employee.json sample
    /// </summary>
    public class SAprioriEmployee
    {
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public List<SAprioriOrder> Orders { get; set; }
    }
}
