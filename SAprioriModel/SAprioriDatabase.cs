using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SAprioriModel
{
    /// <summary>
    /// The SAprioriDatabase class is the main Structurer
    /// All data set can be loaded by this class
    /// </summary>
    public class SAprioriDatabase
    {
        public List<SAprioriCustomer> Customers { get; set; }
        public List<SAprioriOrder> Orders { get; set; }
        public List<SAprioriOrder> FilteringOrders { get; set; }
        public List<SAprioriOrderDetail> OrderDetails { get; set; }
        public List<SAprioriProduct> Products { get; set; }
        public List<SAprioriCategory> Categories { get; set; }
        public List<SAprioriEmployee> Employees { get; set; }

        public Dictionary<SAprioriSeason, List<int>> Seasons { get; set; }

        public Dictionary<int,SAprioriProduct> DicProducts { get; set; }

        private string datasetFolder;
        private string customerJsonFile;
        private string orderJsonFile;
        private string oderDetailJsonFile;
        private string productJsonFile;
        private string categoryJsonFile;
        private string employeeJsonFile;
        /// <summary>
        /// Constructor SAprioriDatabase
        /// it initializes all of the list
        /// </summary>
        public SAprioriDatabase()
        {
            Customers = new List<SAprioriCustomer>();
            Orders = new List<SAprioriOrder>();
            OrderDetails = new List<SAprioriOrderDetail>();
            Products = new List<SAprioriProduct>();
            Categories = new List<SAprioriCategory>();
            Employees = new List<SAprioriEmployee>();
            Seasons = new Dictionary<SAprioriSeason, List<int>>();
        }
        /// <summary>
        /// This method will load all data from dataset folder
        /// call LoadDatabase(datasetFolder)
        /// the datasetFolder folder has to store the json files which name:
        /// <para/>Customer.json, Order.json, OrderDetails.json, Product.json, Category.json, Employee.json
        /// and the structurer is the same in the class:
        /// <para/>Customer.json->SAprioriCustomer class
        /// <para/>Order.json->SAprioriOrder class 
        /// <para/>OrderDetails.json-> SAprioriOrderDetail class
        /// <para/>Product.json -> SAprioriProduct class
        /// <para/>Category.json -> SAprioriCategory class
        /// <para/>Employee.json -> SAprioriEmployee class 
        /// </summary>
        /// <para/><u>Example:</u>
        /// <example>
        /// <para/>LoadDatabase("largedataset")
        /// </example>
        /// <param name="datasetFolder">
        /// folder contains all the json data
        /// </param>
        /// <returns>SAprioriDatabase</returns>
        public SAprioriDatabase LoadDatabase(string datasetFolder)
        {
            this.datasetFolder = datasetFolder;
            customerJsonFile= datasetFolder+ "/Customer.json";
            orderJsonFile = datasetFolder + "/Order.json";
            oderDetailJsonFile = datasetFolder + "/OrderDetails.json";
            productJsonFile = datasetFolder + "/Product.json";
            categoryJsonFile = datasetFolder + "/Category.json";
            employeeJsonFile = datasetFolder + "/Employee.json";
            LoadCustomers(customerJsonFile);
            LoadOrders(orderJsonFile);
            LoadOrderDetails(oderDetailJsonFile);
            LoadProducts(productJsonFile);
            LoadCategories(categoryJsonFile);
            LoadEmployee(employeeJsonFile);
            return this;
        }
       
        private string getJsonOnCloud(string link)
        {
            WebRequest request = HttpWebRequest.Create(link);

            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string responseText = reader.ReadToEnd();
            return responseText;
        }
        /// <summary>
        /// linkFolderDataset=https://raw.githubusercontent.com/thanhtd32/SAprioriSystem/main/dataset/smalldataset/
        /// </summary>
        /// <param name="linkFolderDataset">Link of folder dataset</param>
        /// <returns></returns>
        public void LoadDatabaseOnCloud(string linkFolderDataset)
          {
            datasetFolder = linkFolderDataset;
            customerJsonFile = datasetFolder + "/Customer.json";
            orderJsonFile = datasetFolder + "/Order.json";
            oderDetailJsonFile = datasetFolder + "/OrderDetails.json";
            productJsonFile = datasetFolder + "/Product.json";
            categoryJsonFile = datasetFolder + "/Category.json";
            employeeJsonFile = datasetFolder + "/Employee.json";
            
            string json= getJsonOnCloud(customerJsonFile); 
            Customers = JsonConvert.DeserializeObject<List<SAprioriCustomer>>(json);
            json = getJsonOnCloud(orderJsonFile);
            Orders = JsonConvert.DeserializeObject<List<SAprioriOrder>>(json);
            json = getJsonOnCloud(oderDetailJsonFile);
            OrderDetails = JsonConvert.DeserializeObject<List<SAprioriOrderDetail>>(json);
            json = getJsonOnCloud(productJsonFile);
            Products = JsonConvert.DeserializeObject<List<SAprioriProduct>>(json);
            json = getJsonOnCloud(categoryJsonFile);
            Categories = JsonConvert.DeserializeObject<List<SAprioriCategory>>(json);
            json = getJsonOnCloud(employeeJsonFile);
            Employees = JsonConvert.DeserializeObject<List<SAprioriEmployee>>(json);
        }
        /// <summary>
        /// Reload dataset
        /// This method must be called after the LoadDatabase
        /// </summary>
        /// <returns>SAprioriDatabase</returns>
        public SAprioriDatabase ReloadDatabase()
        {
            LoadCustomers(customerJsonFile);
            LoadOrders(orderJsonFile);
            LoadOrderDetails(oderDetailJsonFile);
            LoadProducts(productJsonFile);
            LoadCategories(categoryJsonFile);
            LoadEmployee(employeeJsonFile);
            return this;
        }
        /// <summary>
        /// This method use to load customer
        /// <para/>LoadCustomers(customerJsonFile)
        /// <para/>customerJsonFile->Customer.json
        /// <para/>It returns list of Customers
        /// <para/><u>Example:</u>
        /// <example>
        /// <para/>LoadCustomers("largedataset/Customer.json")
        /// </example>
        /// </summary>
        /// <param name="customerJsonFile">file of json for Customers</param>
        /// <returns>List of SAprioriCustomer </returns>
        public List<SAprioriCustomer> LoadCustomers(string customerJsonFile)
        {
            if(File.Exists(customerJsonFile)==false)
            {
                Customers.Clear();
                return Customers;
            }
            this.customerJsonFile = customerJsonFile;
            string json = ParseJson(customerJsonFile);
            Customers = JsonConvert.DeserializeObject<List<SAprioriCustomer>>(json);
            return Customers;
        }
        /// <summary>
        /// This method reloads the customers
        /// <para/>it has to call after the LoadCustomers(customerJsonFile) method
        /// </summary>
        /// <returns>List of SAprioriCustomer</returns>
        public List<SAprioriCustomer> ReloadCustomers()
        {
            if (File.Exists(customerJsonFile) == false)
            {
                Customers.Clear();
                return Customers;
            }
            string json = ParseJson(customerJsonFile);
            Customers = JsonConvert.DeserializeObject<List<SAprioriCustomer>>(json);
            return Customers;
        }
        /// <summary>
        /// <para/>This method use to load Orders
        /// <para/>LoadOrders(orderJsonFile)
        /// <para/>Order.json->SAprioriOrder class
        /// <para/>It returns list of Orders
        /// <para/><u>Example:</u>
        /// <example>
        /// <para/>LoadOrders("largedataset/Order.json")
        /// </example>
        /// </summary>
        /// <param name="orderJsonFile">file of json for Orders</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> LoadOrders(string orderJsonFile)
        {
            if (File.Exists(orderJsonFile) == false)
            {
                Orders.Clear();
                return Orders;
            }
            this.orderJsonFile = orderJsonFile;
            string json = ParseJson(orderJsonFile);
            Orders = JsonConvert.DeserializeObject<List<SAprioriOrder>>(json);
            return Orders;
        }
        /// <summary>
        /// <para/>This method reloads the Orders
        /// <para/>it has to call after the LoadOrders(orderJsonFile) method
        /// </summary>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> ReloadOrders()
        {
            if (File.Exists(orderJsonFile) == false)
            {
                Orders.Clear();
                return Orders;
            }
            string json = ParseJson(orderJsonFile);
            Orders = JsonConvert.DeserializeObject<List<SAprioriOrder>>(json);
            return Orders;
        }
        /// <summary>
        /// <para/>This method use to load OrderDetails
        /// <para/>LoadOrderDetails(oderDetailJsonFile)
        /// <para/>OrderDetails.json->SAprioriOrderDetail class
        /// <para/>It returns list of Orders
        /// <para/><u>Example:</u>
        /// <example>
        /// <para/>LoadOrderDetails("largedataset/OrderDetails.json")
        /// </example>
        /// </summary>
        /// <param name="oderDetailJsonFile">file of json for OrderDetail</param>
        /// <returns>List of SAprioriOrderDetail</returns>
        public List<SAprioriOrderDetail> LoadOrderDetails(string oderDetailJsonFile)
        {
            if (File.Exists(oderDetailJsonFile) == false)
            {
                OrderDetails.Clear();
                return OrderDetails;
            }
            this.oderDetailJsonFile = oderDetailJsonFile;
            string json = ParseJson(oderDetailJsonFile);
            OrderDetails = JsonConvert.DeserializeObject<List<SAprioriOrderDetail>>(json);
            return OrderDetails;
        }
        /// <summary>
        /// <para/>This method reloads the OrderDetails
        /// <para/>it has to call after the LoadOrderDetails(oderDetailJsonFile) method
        /// </summary>
        /// <returns>List of SAprioriOrderDetail</returns>
        public List<SAprioriOrderDetail> ReloadOrderDetails()
        {
            if (File.Exists(oderDetailJsonFile) == false)
            {
                OrderDetails.Clear();
                return OrderDetails;
            }
            string json = ParseJson(oderDetailJsonFile);
            OrderDetails = JsonConvert.DeserializeObject<List<SAprioriOrderDetail>>(json);
            return OrderDetails;
        }
        /// <summary>
        /// <para/>This method use to load Product
        /// <para/>LoadProducts(productJsonFile)
        /// <para/>Product.json->SAprioriProduct class
        /// <para/>It returns list of Product
        /// <para/><u>Example:</u>
        /// <example>
        /// <para/>LoadProducts("largedataset/Product.json")
        /// </example>
        /// </summary>
        /// <param name="productJsonFile">file of json for Product</param>
        /// <returns>List of SAprioriProduct</returns>
        public List<SAprioriProduct> LoadProducts(string productJsonFile)
        {
            if (File.Exists(productJsonFile) == false)
            {
                Products.Clear();
                return Products;
            }
            this.productJsonFile = productJsonFile;
            string json = ParseJson(productJsonFile);
            Products = JsonConvert.DeserializeObject<List<SAprioriProduct>>(json);
            return Products;
        }
        /// <summary>
        /// <para/>This method reloads the Product
        /// <para/>it has to call after the LoadProducts(productJsonFile) method
        /// </summary>
        /// <returns>List of SAprioriProduct</returns>
        public List<SAprioriProduct> ReloadProducts()
        {
            if (File.Exists(productJsonFile) == false)
            {
                Products.Clear();
                return Products;
            }
            string json = ParseJson(productJsonFile);
            Products = JsonConvert.DeserializeObject<List<SAprioriProduct>>(json);
            return Products;
        }
        /// <summary>
        /// <para/>This method use to load Category
        /// <para/>LoadCategories(categoryJsonFile)
        /// <para/>Category.json->SAprioriCategory class
        /// <para/>It returns list of Category
        /// <para/><u>Example:</u>
        /// <example>
        /// <para/>LoadCategories("largedataset/Category.json")
        /// </example>
        /// </summary>
        /// <param name="categoryJsonFile">file of json for Category</param>
        /// <returns>List of Category</returns>
        public List<SAprioriCategory> LoadCategories(string categoryJsonFile)
        {
            if (File.Exists(categoryJsonFile) == false)
            {
                Categories.Clear();
                return Categories;
            }
            this.categoryJsonFile = categoryJsonFile;
            string json = ParseJson(categoryJsonFile);
            Categories = JsonConvert.DeserializeObject<List<SAprioriCategory>>(json);
            return Categories;
        }
        /// <summary>
        /// <para/>This method reloads the Categories
        /// <para/>it has to call after the LoadCategories(categoryJsonFile) method
        /// </summary>
        /// <returns>List of SAprioriCategory</returns>
        /// <returns></returns>
        public List<SAprioriCategory> ReloadCategories()
        {
            if (File.Exists(categoryJsonFile) == false)
            {
                Categories.Clear();
                return Categories;
            }
            string json = ParseJson(categoryJsonFile);
            Categories = JsonConvert.DeserializeObject<List<SAprioriCategory>>(json);
            return Categories;
        }
        /// <summary>
        /// <para/>This method use to load Employee
        /// <para/>LoadEmployee(employeeJsonFile)
        /// <para/>Employee.json->SAprioriEmployee class
        /// <para/>It returns list of Employee
        /// <para/><u>Example:</u>
        /// <example>
        /// <para/>LoadEmployee("largedataset/Employee.json")
        /// </example>
        /// </summary>
        /// <param name="employeeJsonFile"></param>
        /// <returns></returns>
        public List<SAprioriEmployee> LoadEmployee(string employeeJsonFile)
        {
            if (File.Exists(employeeJsonFile) == false)
            {
                Employees.Clear();
                return Employees;
            }
            this.employeeJsonFile = employeeJsonFile;
            string json = ParseJson(employeeJsonFile);
            Employees = JsonConvert.DeserializeObject<List<SAprioriEmployee>>(json);
            return Employees;
        }
        /// <summary>
        /// <para/>This method reloads the Employee
        /// <para/>it has to call after the Employee(employeeJsonFile) method
        /// </summary>
        /// <returns>List of SAprioriEmployee</returns>
        public List<SAprioriEmployee> ReloadEmployee()
        {
            if (File.Exists(employeeJsonFile) == false)
            {
                Employees.Clear();
                return Employees;
            }
            string json = ParseJson(employeeJsonFile);
            Employees = JsonConvert.DeserializeObject<List<SAprioriEmployee>>(json);
            return Employees;
        }
        /// <summary>
        /// This function will read json content in the  json file
        /// </summary>
        /// <param name="jsonFile">file with json content</param>
        /// <returns>string of json format</returns>
        public string ParseJson(string jsonFile)
        {
            StreamReader sr = new StreamReader(jsonFile, Encoding.UTF8);
            string s = sr.ReadToEnd();
            sr.Close();
            return s;
        }     
        /// <summary>
        /// use this function to add season
        /// </summary>
        /// <param name="season">season</param>
        /// <param name="months">months of season</param>
        public void addSeason(SAprioriSeason season,List<int>months)
        {
            Seasons.Add(season, months);
        }
        /// <summary>
        /// Filter Orders between datetime 
        /// </summary>
        /// <param name="from">Start date to filter</param>
        /// <param name="to">End date to filter</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee
        /// </param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrders(DateTime from, DateTime to,bool buildTree=true)
        {
            FilteringOrders = Orders.Where(x => x.OrderDate >= from && x.OrderDate <= to).ToList();
            if(buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
        /// <summary>
        /// Build FilteringOrders Tree
        /// This is imporant to run SApriori
        /// </summary>
        private void BuildFilteringOrdersTree()
        {
            if(OrderDetails.Count==0)
            {
                ReloadOrderDetails();
                ReloadProducts();
                ReloadCustomers();
                ReloadEmployee();
                ReloadCategories();                
            }
            DicProducts = new Dictionary<int, SAprioriProduct>();
            foreach (SAprioriOrder order in FilteringOrders)
            {
                order.OrderDetails= OrderDetails.Where(od => od.OrderID == order.OrderID).ToList();
                foreach(SAprioriOrderDetail od in order.OrderDetails)
                {
                    od.Order = order;
                    od.Product = Products.FirstOrDefault(p => p.ProductID == od.ProductID);
                    if (DicProducts.ContainsKey(od.ProductID) == false)
                        DicProducts.Add(od.ProductID, od.Product);
                }
                order.Customer = Customers.FirstOrDefault(c => c.CustomerID == order.CustomerID);
                order.Employee = Employees.FirstOrDefault(s => s.EmployeeID == order.EmployeeID);
            }            
        }

        /// <summary>
        /// Filter Orders on period 
        /// </summary>
        /// <param name="period">the date to filter</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrders(DateTime period, bool buildTree = true)
        {
            FilteringOrders = Orders.Where(x => x.OrderDate.Date.CompareTo(period.Date)==0).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
        /// <summary>
        /// Filter Orders between index
        /// </summary>
        /// <param name="from">start index</param>
        /// <param name="to">end index</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns></returns>
        public List<SAprioriOrder> FilterOrders(int from, int to, bool buildTree = true)
        {
            FilteringOrders = Orders.Skip(from).Take(to-from).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
        /// <summary>
        /// Filter Orders n elements
        /// </summary>
        /// <param name="n">number of elements to filter</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrders(int n, bool buildTree = true)
        {
            FilteringOrders = Orders.Take(n).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
        /// <summary>
        /// Filter Orders by season
        /// </summary>
        /// <param name="season">season to filter</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrders(SAprioriSeason season, bool buildTree = true)
        {
            List<int> months = Seasons[season];
            FilteringOrders = Orders.Where(x => months.Contains(x.OrderDate.Month)).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
        /// <summary>
        /// Filter Orders by Specical Month And Day
        /// </summary>
        /// <param name="month">month to filter</param>
        /// <param name="day"></param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrdersSpecicalMonthAndDay(int month, int day, bool buildTree = true)
        {
            FilteringOrders = Orders.Where(x => x.OrderDate.Date.Month == month && x.OrderDate.Date.Date.Day == day).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
        /// <summary>
        ///  Filter Orders by Specical Year And Month
        /// </summary>
        /// <param name="year">year to filter</param>
        /// <param name="month">month to filter</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrdersSpecicalYearAndMonth(int year, int month, bool buildTree = true)
        {
            FilteringOrders = Orders.Where(x => x.OrderDate.Date.Year == year && x.OrderDate.Date.Date.Month == month).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
        /// <summary>
        /// Filter Orders by Specical Year 
        /// </summary>
        /// <param name="year">Year to filter</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrdersSpecicalYear(int year, bool buildTree = true)
        {
            FilteringOrders = Orders.Where(x => x.OrderDate.Date.Year == year).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }

        /// <summary>
        /// Filter Orders by season
        /// </summary>
        /// <param name="season">season to filter</param>
        /// <param name="year">year to filter</param>
        /// <param name="buildTree">default true to build tree structurer for order,
        /// it means show order details, product, customer and employee</param>
        /// <returns>List of SAprioriOrder</returns>
        public List<SAprioriOrder> FilterOrders(SAprioriSeason season, int year, bool buildTree = true)
        {
            List<int> months = Seasons[season];
            FilteringOrders = Orders.Where(x => months.Contains(x.OrderDate.Month) && x.OrderDate.Year == year).ToList();
            if (buildTree)
                BuildFilteringOrdersTree();
            return FilteringOrders;
        }
    }
}
