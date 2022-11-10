using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// This class is SAprioriProduct
    /// 1 SAprioriProduct can know SAprioriCategory
    /// </summary>
    public class SAprioriProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public int CategoryID { get; set; }
        [JsonIgnore]
        public SAprioriCategory Category { get; set; }
    }
}
