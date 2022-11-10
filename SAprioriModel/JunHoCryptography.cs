using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    public class JunHoCryptography : Secret
    {
        public override string decrypt(string encryptedText, string key)
        {
            String res = "";
            key = expandedKey(key, encryptedText);
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

        public override string encrypt(string originalText, string key)
        {
            key = expandedKey(key, originalText);
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
    }
}
