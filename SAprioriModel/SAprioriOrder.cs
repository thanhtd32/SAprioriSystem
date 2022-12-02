using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// This class is SAprioriOrder
    /// OrderDate is to filter Oder by date
    /// 1 SAprioriOrder has many SAprioriOrderDetail
    /// 1 SAprioriOrder can know SAprioriCustomer and SAprioriEmployee
    /// </summary>
    public class SAprioriOrder
    {
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        [JsonIgnore]
        public List<SAprioriOrderDetail> OrderDetails { get; set; }
        public SAprioriOrder()
        {
            OrderDetails = new List<SAprioriOrderDetail>();
        }
        [JsonIgnore]
        public SAprioriCustomer Customer { get; set; }
        [JsonIgnore]
        public SAprioriEmployee Employee { get; set; }
    }
}

