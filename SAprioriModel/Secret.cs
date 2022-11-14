using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SAprioriModel
{
    public abstract class Secret
    {
        /**
         * This function use to encrypt the text with key follow the Vigenere Cipher algorithm 
         * @param text
         * @param key
         * @return
         */
        public abstract String Encrypt(String originalText, String key);
        /**
         * This function use to decrypt the text with key follow the Vigenere Cipher algorithm 
         * @param text
         * @param key
         * @return
         */
        public abstract String Decrypt(String decryptedText, String key);
        /**
         * This function use to expand key for the same length with text
         * @param key
         * @param text
         * @return
         */
        public String ExpandedKey(String key, String text)
        {
            StringBuilder sb = new StringBuilder(text.Length + key.Length - 1);
            while (sb.Length < text.Length)
            {
                sb.Append(key);
            }
            sb.Length = text.Length;

            String expandedKey = sb.ToString();

            return expandedKey.ToUpper();

        }
        public int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        public string Cipher(string input, string key, bool encipher)
        {
            for (int i = 0; i < key.Length; ++i)
                if (!char.IsLetter(key[i]))
                    return null; // Error

            string output = string.Empty;
            int nonAlphaCharCount = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                if (char.IsLetter(input[i]))
                {
                    bool cIsUpper = char.IsUpper(input[i]);
                    char offset = cIsUpper ? 'A' : 'a';
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
                    k = encipher ? k : -k;
                    char ch = (char)((Mod(((input[i] + k) - offset), 26)) + offset);
                    output += ch;
                }
                else
                {
                    output += input[i];
                    ++nonAlphaCharCount;
                }
            }

            return output;
        }
      
    }
}
