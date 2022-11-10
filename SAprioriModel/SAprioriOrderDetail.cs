using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// This class is SAprioriOrderDetail
    /// 1 SAprioriOrderDetail can know SAprioriOrder and SAprioriProduct
    /// </summary>
    public class SAprioriOrderDetail
    {
        public string OrderID { get; set; }
        public string OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [JsonIgnore]
        public SAprioriOrder Order { get; set; }
        [JsonIgnore]
        public SAprioriProduct Product { get; set; }
    }
}
