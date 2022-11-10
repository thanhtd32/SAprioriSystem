using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// The SAprioriCategory class 
    /// 1 SAprioriCategory  has many SAprioriProducts
    /// Data set Category.json sample
    /// </summary>
    public class SAprioriCategory
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<SAprioriProduct> Products { get; set; }
        public SAprioriCategory()
        {
            this.Products = new List<SAprioriProduct>();
        }
    }
}
