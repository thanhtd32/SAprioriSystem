using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// This class stores the Rule when run SApriori algorithm
    /// </summary>
    public class SAprioriRule : IComparable<SAprioriRule>
    {
        public string X { get; set; }
        public string Y { get; set; }
        public double Confidence { get; set; }

        public List<SAprioriProduct> X_Results { get; set; }
        public List<SAprioriProduct> Y_Results { get; set; }
        public SAprioriRule(string x, string y, double confidence)
        {
            this.X = x;
            this.Y = y;
            this.Confidence = confidence;
        }
        public string X_Results_Description
        {
            get
            {
                return getItems(X_Results);
            }
        }
        public string Y_Results_Description
        {
            get
            {
                return getItems(Y_Results);
            }
        }
        private string getItems(List<SAprioriProduct> items)
        {
            if (X_Results == null) return "";
            string result = "";
            foreach (SAprioriProduct p in items)
            {
                result = result + p.Name + ",";
            }
            return result.Remove(result.Length - 1);
        }      
                
        public int CompareTo(SAprioriRule other)
        {
            return X.CompareTo(other.X);
        }
    }
}
