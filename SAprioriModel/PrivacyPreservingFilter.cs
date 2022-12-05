using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    public  class PrivacyPreservingFilter
    {
        ThanhAndHuhCryptography ThanhAndHuhCryptography = new ThanhAndHuhCryptography();
        string KEY = "SERECT";
        public void DataPrivacy(Object obj, string[] features)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (features.Contains(prop.Name))
                {
                    string original = null;
                    if (prop.GetValue(obj, null) != null)
                    {
                        original = prop.GetValue(obj, null).ToString();
                        if(original.Trim() != "")
                        {
                            string encryptedProp = ThanhAndHuhCryptography.Encrypt(original,KEY);
                            prop.SetValue(obj, encryptedProp);
                        }                            
                    }
                }
            }
        }
        public void CustomerDataPrivacy(SAprioriCustomer customer, string[] features)
        {            
            foreach (var prop in customer.GetType().GetProperties())
            {
                if (features.Contains(prop.Name))
                {
                    string original = "";
                    if (prop.GetValue(customer, null) != null)
                    {
                        original = prop.GetValue(customer, null).ToString();
                        if(original.Trim()!="")
                        {
                            string encryptedProp = ThanhAndHuhCryptography.Encrypt(original,KEY);
                            prop.SetValue(customer, encryptedProp);
                        }                        
                    }
                }
            }            
        }
        public void CustomerDataPrivacy(List<SAprioriCustomer> customers, string[] features)
        {
            foreach (SAprioriCustomer customer in customers)
            {
                CustomerDataPrivacy(customer, features);
            }
        }
        public void CustomerDataPrivacy(SAprioriDatabase database,string[]features)
        {
            CustomerDataPrivacy(database.Customers, features);
        }
        public void CustomerTransactionPrivacy(SAprioriOrder order, string[] features)
        {
            foreach (var prop in order.GetType().GetProperties())
            {
                if (features.Contains(prop.Name))
                {
                    string original = "";
                    if (prop.GetValue(order, null) != null)
                    {
                        original = prop.GetValue(order, null).ToString();
                        if(original.Trim()!="")
                        {
                            string encryptedProp = ThanhAndHuhCryptography.Encrypt(original,KEY);
                            prop.SetValue(order, encryptedProp);
                        }                        
                    }
                }
            }
            CustomerTransactionPrivacy(order.OrderDetails, features);
        }
        public void CustomerTransactionPrivacy(List<SAprioriOrderDetail> orderDetails, string[] features)
        {
            foreach (SAprioriOrderDetail orderDetail in orderDetails)
            {
                CustomerTransactionPrivacy(orderDetail, features);
            }
        }
        public void CustomerTransactionPrivacy(SAprioriOrderDetail orderDetail, string[] features)
        {
            foreach (var prop in orderDetail.GetType().GetProperties())
            {
                if (features.Contains(prop.Name))
                {
                    string original = "";
                    if (prop.GetValue(orderDetail, null) != null)
                    {
                        original = prop.GetValue(orderDetail, null).ToString();
                        if(original.Trim()!="")
                        {
                            string encryptedProp = ThanhAndHuhCryptography.Encrypt(original,KEY);
                            prop.SetValue(orderDetail, encryptedProp);
                        }                        
                    }
                }
            }
        }
        public void CustomerTransactionPrivacy(SAprioriCustomer customers, string[] features)
        {
            foreach (SAprioriOrder order in customers.Orders)
            {
                CustomerTransactionPrivacy(order, features);
            }
        }
    }
}
