using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public abstract String encrypt(String originalText, String key);
        /**
         * This function use to decrypt the text with key follow the Vigenere Cipher algorithm 
         * @param text
         * @param key
         * @return
         */
        public abstract String decrypt(String decryptedText, String key);
        /**
         * This function use to expand key for the same length with text
         * @param key
         * @param text
         * @return
         */
        public String expandedKey(String key, String text)
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
    }
}
