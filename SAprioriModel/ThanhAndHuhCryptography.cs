using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    public class ThanhAndHuhCryptography : Secret
    {       
        public override string Decrypt(string encryptedText, string key)
        {
            String res = "";
            key = ExpandedKey(key, encryptedText);
            for (int i = 0, j = 0; i < encryptedText.Length; i++)
            {
                int d = encryptedText[i];
                int k = key[j] - 65;
                if (d == 45) res += " ";
                else if (d >= 65 && d <= 90)
                    if ((d - k) < 65) res += (char)(d - k + 26);
                    else res += (char)(d - k);
                else if (d >= 97 && d <= 122)
                    if ((d - k) < 97) res += (char)(d - k + 26);
                    else res += (char)(d - k);
                else res += (char)(d);
                j = ++j % key.Length;
            }
            return res;
        }

        public override string Encrypt(string originalText, string key)
        {
            key = ExpandedKey(key, originalText);
            String res = "";
            for (int i = 0, j = 0; i < originalText.Length; i++)
            {
                int c = originalText[i];
                int k = key[j] - 65;
                if (c == 32) res += "-";
                else if (c >= 65 && c <= 90)
                    if ((c + k) > 90) res += (char)(c + k - 26);
                    else res += (char)(c + k);
                else if (c >= 97 && c <= 122)
                    if ((c + k) > 122) res += (char)(c + k - 26);
                    else res += (char)(c + k);
                else res += (char)(c);
                j = ++j % key.Length;
            }
            return res;
        }
        public double Entropy(string s)
        {
            var map = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (!map.ContainsKey(c))
                    map.Add(c, 1);
                else
                    map[c] += 1;
            }

            double result = 0.0;
            int len = s.Length;
            foreach (var item in map)
            {
                var frequency = (double)item.Value / len;
                result -= frequency * (Math.Log(frequency) / Math.Log(2));
            }

            return result;
        }
        public string HiddenText(string s,char mask,double hiddenPercent, HiddenMode mode=HiddenMode.Random)
        {
            char[] arr = s.ToCharArray();
            int nHidden=(int)(arr.Length* hiddenPercent/100);
            if (nHidden >= arr.Length - 1)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = mask;
                }
            }
            else
            {
                if (mode ==HiddenMode.Random)
                {
                    Random rd = new Random();
                    HashSet<int> set = new HashSet<int>();
                    while (set.Count < nHidden)
                    {
                        int i = rd.Next(0, arr.Length);
                        set.Add(i);
                    }
                    foreach (int i in set)
                    {
                        arr[i] = mask;
                    }
                }
                else if(mode ==HiddenMode.LeftToRight)
                {
                    for (int i = 0; i < nHidden; i++)
                    {
                        arr[i] = mask;
                    }                    
                }
                else if (mode == HiddenMode.RightToLeft)
                {
                    int i = arr.Length-1;
                    int count = 0;
                    while(count <= nHidden)
                    {
                        arr[i--] = mask;
                        count++;
                    }
                }
                else if(mode==HiddenMode.Center)
                {
                    int c = arr.Length / 2;
                    int half = nHidden / 2;
                    int i = c;
                    for (; i <= c+ half; i++)
                    {
                        arr[i] = mask;
                    }
                    int count = 0;
                    i = c-1;
                    while (count <= half)
                    {
                        arr[i--] = mask;
                        count++;
                    }
                }
            }
            return new string(arr);
        }
    }
}
