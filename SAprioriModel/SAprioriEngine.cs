using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// The main class to run SApriori model
    /// </summary>
    public class SAprioriEngine
    {
        public SAprioriDatabase Database { get; set; }
        public double MinSupport { get; set; }
        public double MinConfident { get; set; }
        private Dictionary<int, string> Transactions = new Dictionary<int, string>();
        private Dictionary<string, double> FrequentItems = new Dictionary<string, double>();
        private Dictionary<string, string> ProductNames;
        private int lastTransId = 1;
        public SAprioriResult AprioriResult { get; set; }
        /// <summary>
        /// This function use to run S-Apriori model
        /// <para/>Filter datadase between date
        /// </summary>
        /// <param name="database">SAprioriDatabase </param>
        /// <param name="from">start date</param>
        /// <param name="to">end date</param>
        /// <param name="minSupport">Min Support</param>
        /// <param name="minConfident">Min Confident</param>
        /// <returns>SAprioriResult</returns>
        public SAprioriResult runSAprioriModel(SAprioriDatabase database, DateTime from, DateTime to, double minSupport, double minConfident)
        {
            database.FilterOrders(from, to);
            this.Database = database;
            this.MinSupport = minSupport;
            this.MinConfident = minConfident;
            SAprioriResult result = runSApriori();
            return result;
        }
        /// <summary>
        /// This function use to run S-Apriori model
        /// <para/>Filter datadase on period
        /// </summary>
        /// <param name="database">SAprioriDatabase</param>
        /// <param name="period">the time to filter</param>
        /// <param name="minSupport">Min Support</param>
        /// <param name="minConfident">Min Confident</param>
        /// <returns>SAprioriResult</returns>
        public SAprioriResult runSAprioriModel(SAprioriDatabase database, DateTime period, double minSupport, double minConfident)
        {
            database.FilterOrders(period);
            this.Database = database;
            this.MinSupport = minSupport;
            this.MinConfident = minConfident;
            SAprioriResult result = runSApriori();
            return result;
        }
        /// <summary>
        /// This function use to run S-Apriori model
        /// <para/>Filter datadase by season
        /// </summary>
        /// <param name="database">SAprioriDatabase</param>
        /// <param name="season">season to filter</param>
        /// <param name="minSupport">Min Support</param>
        /// <param name="minConfident">Min Confident</param>
        /// <returns></returns>
        public SAprioriResult runSAprioriModel(SAprioriDatabase database, SAprioriSeason season, double minSupport, double minConfident)
        {
            database.FilterOrders(season);
            this.Database = database;
            this.MinSupport = minSupport;
            this.MinConfident = minConfident;
            SAprioriResult result = runSApriori();
            return result;
        }
        /// <summary>
        /// This function use to run S-Apriori model
        /// <para/>get all datadase
        /// </summary>
        /// <param name="database">SAprioriDatabase</param>
        /// <param name="minSupport">Min Support</param>
        /// <param name="minConfident">Min Confident</param>
        /// <returns></returns>
        public SAprioriResult runSAprioriModel(SAprioriDatabase database, double minSupport, double minConfident)
        {
            this.Database = database;
            this.MinSupport = minSupport;
            this.MinConfident = minConfident;
            SAprioriResult result = runSApriori();
            return result;
        }
        private SAprioriResult runSApriori()
        {
            List<string> strBuild = new List<string>();
            Dictionary<string, string> mapItemsName = new Dictionary<string, string>();
            ProductNames = new Dictionary<string, string>();
            List<string> alphaFull = new List<string>();
            List<string> fullName = new List<string>();
            foreach (KeyValuePair<int, SAprioriProduct> item in Database.DicProducts)
            {
                string build = item.Key.ToString();
                strBuild.Add(build);
                mapItemsName.Add(item.Value.Name, build);
                ProductNames.Add(build, item.Value.Name);
                alphaFull.Add(build);
                fullName.Add(item.Value.Name);
            }
            getStringData(fullName, mapItemsName);

            SAprioriResult result = analyseSAprioriAlgorithm(alphaFull);
            AprioriResult = result;
            return result;
        }

        private void getStringData(List<string> root, Dictionary<string, string> mapItemsName)
        {
            foreach (SAprioriOrder order in Database.FilteringOrders)
            {
                lastTransId++;

                List<string> itemNames = new List<string>();
                foreach (SAprioriOrderDetail orderDetail in order.OrderDetails)
                {
                    itemNames.Add(orderDetail.Product.Name);
                }

                String strResult = "";
                List<string> arrResult = new List<string>();
                for (int i = 0; i < root.Count; i++)
                {
                    String str = root[i];

                    if (itemNames.Contains(str))
                    {
                        arrResult.Add(mapItemsName[str]);
                    }

                }
                strResult = string.Join("_", arrResult);

                Transactions.Add(lastTransId, strResult);

            }
        }
        private SAprioriResult analyseSAprioriAlgorithm(List<string> keys)
        {
            SAprioriResult result = new SAprioriResult();

            double dMinSupport = MinSupport / 100;
            double dMinConfidence = MinConfident / 100;

            Dictionary<string, double> frequentItemsL1 = GetL1FrequentItems(dMinSupport, keys);

            Dictionary<string, double> frequentItems = frequentItemsL1;
            Dictionary<string, double> candidates = new Dictionary<string, double>();
            do
            {
                candidates = GenerateCandidates(frequentItems);
                frequentItems = GetFrequentItems(candidates, dMinSupport);
            }
            while (candidates.Count != 0);

            Dictionary<string, Dictionary<string, double>> closedItemSets = GetClosedItemSets();

            List<SAprioriRule> lstRules = GenerateRules();
            List<SAprioriRule> lstStrongRules = GetStrongRules(dMinConfidence, lstRules);

            result.ClosedItemSets = closedItemSets;
            result.Rules = lstRules;
            result.StrongRules = lstStrongRules;
            return result;
        }
        public string getItems(string items)
        {
            string result = "";
            string[] arrItems = items.Split(new char[] { '_' });
            for (int i = 0; i < arrItems.Length; i++)
            {
                string charItem = arrItems[i];
                result += ProductNames[charItem.ToString()] + ",";
            }

            return result.Remove(result.Length - 1);
        }
        private List<SAprioriRule> GetStrongRules(double dMinConfidence, List<SAprioriRule> lstRules)
        {
            List<SAprioriRule> lstStrongRulesReturn = new List<SAprioriRule>();
            foreach (SAprioriRule Rule in lstRules)
            {
                string strXY = SortCandidate(Rule.X + "_" + Rule.Y);
                AddStrongRule(Rule, strXY, ref lstStrongRulesReturn, dMinConfidence);
            }
            lstStrongRulesReturn.Sort();
            return lstStrongRulesReturn;
        }
        private double GetConfidence(string strX, string strXY)
        {
            double dSupport_X, dSupport_XY;
            dSupport_X = FrequentItems[strX];
            dSupport_XY = FrequentItems[strXY];
            return dSupport_XY / dSupport_X;
        }
        private List<SAprioriProduct> buildProductsForRule(string items)
        {
            List<SAprioriProduct> sAprioriProducts = new List<SAprioriProduct>();
            string[] arrItems = items.Split(new char[] { '_' });
            for (int i = 0; i < arrItems.Length; i++)
            {
                string charItem = arrItems[i];
                int id = int.Parse(charItem);
                sAprioriProducts.Add(Database.DicProducts[id]);
            }
            return sAprioriProducts;
        }
        private void AddStrongRule(SAprioriRule rule, string strXY, ref List<SAprioriRule> lstStrongRules, double minConf)
        {
            double dConfidence = GetConfidence(rule.X, strXY);
            SAprioriRule newRule;
            if (dConfidence >= minConf)
            {
                newRule = new SAprioriRule(rule.X, rule.Y, dConfidence);
                newRule.X_Results = buildProductsForRule(newRule.X);
                newRule.Y_Results = buildProductsForRule(newRule.Y);
                lstStrongRules.Add(newRule);
            }
            dConfidence = GetConfidence(rule.Y, strXY);
            if (dConfidence >= minConf)
            {
                newRule = new SAprioriRule(rule.Y, rule.X, dConfidence);
                newRule.X_Results = buildProductsForRule(newRule.X);
                newRule.Y_Results = buildProductsForRule(newRule.Y);
                lstStrongRules.Add(newRule);
            }
        }
        private void GenerateCombination(string strItem, int nCombinationLength, ref List<SAprioriRule> lstRulesReturn)
        {
            string[] arr = strItem.Split(new char[] { '_' });
            int nItemLength = arr.Length;
            if (nItemLength == 2)
            {
                AddItem(arr[0].ToString(), strItem, ref lstRulesReturn);
                return;
            }
            else if (nItemLength == 3)
            {
                for (int i = 0; i < nItemLength; i++)
                {
                    AddItem(arr[i].ToString(), strItem, ref lstRulesReturn);
                }
                return;
            }
            else
            {
                for (int i = 0; i < nItemLength; i++)
                {
                    GetCombinationRecursive(arr[i].ToString(), strItem, nCombinationLength, ref lstRulesReturn);
                }
            }
        }
        private void AddItem(string strCombination, string strItem, ref List<SAprioriRule> lstRules)
        {
            string strRemaining = GetRemaining(strCombination, strItem);
            SAprioriRule rule = new SAprioriRule(strCombination, strRemaining, 0);
            rule.X_Results = buildProductsForRule(rule.X);
            rule.Y_Results = buildProductsForRule(rule.Y);
            lstRules.Add(rule);
        }
        private string GetRemaining(string strChild, string strParent)
        {
            string[] arrChild = strChild.Split(new char[] { '_' });
            string[] arrParent = strParent.Split(new char[] { '_' });
            List<string> ap = arrParent.ToList();
            for (int i = 0; i < arrChild.Length; i++)
            {
                int nIndex = 0;
                for (nIndex = 0; nIndex < ap.Count; nIndex++)
                {
                    if (ap[nIndex] == arrChild[i])
                    {
                        ap.RemoveAt(nIndex);
                        break;
                    }
                }

            }
            string sp = string.Join("_", ap);
            return sp;
        }
        private string GetCombinationRecursive(string strCombination, string strItem, int nCombinationLength, ref List<SAprioriRule> lstRulesReturn)
        {
            AddItem(strCombination, strItem, ref lstRulesReturn);
            List<string> arrChild = strCombination.Split(new char[] { '_' }).ToList();
            string cLastTokenCharacter = arrChild[arrChild.Count - 1];
            List<string> arrParent = strItem.Split(new char[] { '_' }).ToList();
            int nLastTokenCharcaterIndexInParent = -1;
            for (int i = 0; i < arrParent.Count; i++)
            {
                if (arrParent[i] == cLastTokenCharacter)
                {
                    nLastTokenCharcaterIndexInParent = i;
                    break;
                }
            }
            string cNextCharacter;
            string cLastItemCharacter = arrParent[arrParent.Count - 1];
            if (arrChild.Count == nCombinationLength)
            {
                if (cLastTokenCharacter != cLastItemCharacter)
                {
                    arrChild.RemoveAt(arrChild.Count - 1);
                    strCombination = string.Join("_", arrChild);
                    cNextCharacter = arrParent[nLastTokenCharcaterIndexInParent + 1];
                    string strNewToken = strCombination + "_" + cNextCharacter;
                    return (GetCombinationRecursive(strNewToken, strItem, nCombinationLength, ref lstRulesReturn));
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                if (strCombination != cLastItemCharacter.ToString())
                {
                    cNextCharacter = arrParent[nLastTokenCharcaterIndexInParent + 1];
                    string strNewToken = strCombination + "_" + cNextCharacter;
                    return (GetCombinationRecursive(strNewToken, strItem, nCombinationLength, ref lstRulesReturn));
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private List<SAprioriRule> GenerateRules()
        {
            List<SAprioriRule> lstRulesReturn = new List<SAprioriRule>();
            foreach (string strItem in FrequentItems.Keys)
            {
                string[] arr = strItem.Split(new char[] { '_' });
                if (arr.Length > 1)
                {
                    int nMaxCombinationLength = arr.Length / 2;
                    GenerateCombination(strItem, nMaxCombinationLength, ref lstRulesReturn);
                }
            }
            return lstRulesReturn;
        }
        private Dictionary<string, Dictionary<string, double>> GetClosedItemSets()
        {
            Dictionary<string, Dictionary<string, double>> dicClosedItemSetsReturn = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, double> dicParents;
            for (int i = 0; i < FrequentItems.Count; i++)
            {
                string strChild = FrequentItems.Keys.ElementAt(i);
                dicParents = GetItemParents(strChild, i + 1);
                if (IsClosed(strChild, dicParents))
                    dicClosedItemSetsReturn.Add(strChild, dicParents);
            }
            return dicClosedItemSetsReturn;
        }
        private Dictionary<string, double> GetItemParents(string strChild, int nIndex)
        {
            Dictionary<string, double> dicParents = new Dictionary<string, double>();
            for (int j = nIndex; j < FrequentItems.Count; j++)
            {
                string strParent = FrequentItems.Keys.ElementAt(j);
                string[] arrParent = strParent.Split(new char[] { '_' });
                string[] arrChild = strChild.Split(new char[] { '_' });
                if (arrParent.Length == arrChild.Length + 1)
                {
                    if (IsSubstring(strChild, strParent))
                    {
                        dicParents.Add(strParent, FrequentItems[strParent]);
                    }
                }
            }
            return dicParents;
        }

        private bool IsClosed(string strChild, Dictionary<string, double> dicParents)
        {
            foreach (string strParent in dicParents.Keys)
            {
                if (FrequentItems[strChild] == FrequentItems[strParent])
                {
                    return false;
                }
            }
            return true;
        }
        private Dictionary<string, double> GetL1FrequentItems(double dMinSupport, List<string> list)
        {
            Dictionary<string, double> dic_FrequentItemsReturn = new Dictionary<string, double>();

            foreach (string item in list)
            {
                double dSupport = GetSupport(item);
                double dx = (dSupport / (double)(lastTransId - 1));
                if (dx >= dMinSupport)
                {
                    dic_FrequentItemsReturn.Add(item, dSupport);
                    FrequentItems.Add(item, dSupport);
                }
            }
            return dic_FrequentItemsReturn;
        }

        private double GetSupport(string strGeneratedCandidate)
        {
            double dSupportReturn = 0;
            foreach (string strTransaction in Transactions.Values)
            {
                if (IsSubstring(strGeneratedCandidate, strTransaction))
                {
                    dSupportReturn++;
                }
            }
            return dSupportReturn;
        }
        private bool IsSubstring(string strChild, string strParent)
        {
            string[] arrChild = strChild.Split(new char[] { '_' });
            string[] arrParent = strParent.Split(new char[] { '_' });
            foreach (string c in arrChild)
            {
                if (!arrParent.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
        private Dictionary<string, double> GenerateCandidates(Dictionary<string, double> dic_FrequentItems)
        {
            Dictionary<string, double> dic_CandidatesReturn = new Dictionary<string, double>();
            for (int i = 0; i < dic_FrequentItems.Count - 1; i++)
            {
                string strFirstItem = SortCandidate(dic_FrequentItems.Keys.ElementAt(i));
                for (int j = i + 1; j < dic_FrequentItems.Count; j++)
                {
                    string strSecondItem = SortCandidate(dic_FrequentItems.Keys.ElementAt(j));
                    string strGeneratedCandidate = GetCandidate(strFirstItem, strSecondItem);
                    if (strGeneratedCandidate != string.Empty)
                    {
                        strGeneratedCandidate = SortCandidate(strGeneratedCandidate);
                        double dSupport = GetSupport(strGeneratedCandidate);
                        dic_CandidatesReturn.Add(strGeneratedCandidate, dSupport);
                    }
                }
            }
            return dic_CandidatesReturn;
        }
        private string SortCandidate(string strToken)
        {
            string[] arr = strToken.Split(new char[] { '_' });
            Array.Sort(arr, StringComparer.InvariantCulture);
            return string.Join("_", arr); ;
        }
        private Dictionary<string, double> GetFrequentItems(Dictionary<string, double> dic_Candidates, double dMinSupport)
        {
            Dictionary<string, double> dic_FrequentReturn = new Dictionary<string, double>();
            for (int i = dic_Candidates.Count - 1; i >= 0; i--)
            {
                string strItem = dic_Candidates.Keys.ElementAt(i);
                double dSupport = dic_Candidates[strItem];
                if ((dSupport / (double)(lastTransId - 1) >= dMinSupport))
                {
                    dic_FrequentReturn.Add(strItem, dSupport);
                    FrequentItems.Add(strItem, dSupport);
                }
            }
            return dic_FrequentReturn;
        }
        private string GetCandidate(string strFirstItem, string strSecondItem)
        {
            string[] arr = strFirstItem.Split(new char[] { '_' });
            int nLength = arr.Length;
            if (nLength == 1)
            {
                return strFirstItem + "_" + strSecondItem;
            }
            else
            {
                List<string> arr2 = arr.ToList();
                arr2.RemoveAt(arr2.Count - 1);
                string[] arrFirstSubString = arr2.ToArray();
                string strFirstSubString = string.Join("_", arrFirstSubString);

                arr = strSecondItem.Split(new char[] { '_' });
                List<string> arr3 = arr.ToList();
                string strLastSecondItem = arr3[arr3.Count - 1];
                arr3.RemoveAt(arr3.Count - 1);
                string[] arrSecondSubString = arr3.ToArray();
                string strSecondSubString = string.Join("_", arrSecondSubString);
                if (strFirstSubString == strSecondSubString)
                {
                    return strFirstItem + "_" + strLastSecondItem;
                }
                else
                    return string.Empty;
            }
        }
    }
}
