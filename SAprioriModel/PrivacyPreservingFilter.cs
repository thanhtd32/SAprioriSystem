using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    public  class PrivacyPreservingFilter
    {
        JunHoCryptography junHoCryptography = new JunHoCryptography();
        string KEY = "SERECT";
        public void CustomerDataPrivacy(SAprioriCustomer customer, string[] features)
        {            
            foreach (var prop in customer.GetType().GetProperties())
            {
                if (features.Contains(prop.Name))
                {
                    string original = prop.GetValue(customer, null).ToString();
                    string encryptedProp = junHoCryptography.encrypt(KEY, original);
                    prop.SetValue(customer, encryptedProp);
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
        public void CustomerSalesTransactionPrivacy(SAprioriOrder order, string[] features)
        {
            foreach (var prop in order.GetType().GetProperties())
            {
                if (features.Contains(prop.Name))
                {
                    string original = prop.GetValue(order, null).ToString();
                    string encryptedProp = junHoCryptography.encrypt(KEY, original);
                    prop.SetValue(order, encryptedProp);
                }
            }
            CustomerSalesTransactionPrivacy(order.OrderDetails, features);
        }
        public void CustomerSalesTransactionPrivacy(List<SAprioriOrderDetail> orderDetails, string[] features)
        {
            foreach (SAprioriOrderDetail orderDetail in orderDetails)
            {
                CustomerSalesTransactionPrivacy(orderDetail, features);
            }
        }
        public void CustomerSalesTransactionPrivacy(SAprioriOrderDetail orderDetail, string[] features)
        {
            foreach (var prop in orderDetail.GetType().GetProperties())
            {
                if (features.Contains(prop.Name))
                {
                    string original = prop.GetValue(orderDetail, null).ToString();
                    string encryptedProp = junHoCryptography.encrypt(KEY, original);
                    prop.SetValue(orderDetail, encryptedProp);
                }
            }
        }
        public void CustomerSalesTransactionPrivacy(SAprioriCustomer customers, string[] features)
        {
            foreach (SAprioriOrder order in customers.Orders)
            {
                CustomerSalesTransactionPrivacy(order, features);
            }
        }
    }
}
