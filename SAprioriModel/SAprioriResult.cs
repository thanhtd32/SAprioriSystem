using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// The class store the result of SApriori algorithm running
    /// </summary>
    public class SAprioriResult
    {
        public Dictionary<string, Dictionary<string, double>> ClosedItemSets { get; set; }
        public List<SAprioriRule> Rules { get; set; }
        public List<SAprioriRule> StrongRules { get; set; }
    }
}
