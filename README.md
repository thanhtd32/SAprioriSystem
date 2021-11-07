# SAprioriSystem
SApriori Engine to Predict the seasonal consumption behavior of consumers based on Object Relational Mapping model and S-Apriori algorithm.

Any question, please free to contact me: thanhtd@uel.edu.vn

Blog study coding: https://duythanhcse.wordpress.com/

Group support Student: https://www.facebook.com/groups/communityuni/

```diff
- I will update all source code for this Project soon... 
```

# Install nuget package
```C#
Install-Package SAprioriModel -ProjectName YourProject
```

# Model class diagram for Sales database:

![alt text](https://raw.githubusercontent.com/thanhtd32/SAprioriSystem/main/Images/SalesModelClass.png)

Mapping JSon Data with Model class:
|# | Dataset | Model class |Description| 
|:---:|:---:|:---:|:---:| 
|1 | Customer.json | SAprioriCustomer | List of Customer dataset|
|2 | Employee.json | SAprioriEmployee | List of Employee dataset|
|3 | Category.json | SAprioriCategory | List of Category dataset|
|4 | Product.json | SAprioriProduct | List of Product dataset|
|5 | Order.json | SAprioriOrder | List of Order dataset|
|6 | OrderDetails.json | SAprioriOrderDetail | List of OrderDetails dataset|

Example ORM mapping SAprioriEmployee with Employee.json:
![alt text](https://raw.githubusercontent.com/thanhtd32/SAprioriSystem/main/Images/EmployeORM.png)

# Model class diagram for SApriori engine:
![alt text](https://raw.githubusercontent.com/thanhtd32/SAprioriSystem/main/Images/SAprioriEngine.png)

# Example with smalldataset - C# code
Download https://github.com/thanhtd32/SAprioriSystem/tree/main/dataset/smalldataset and save it into local file.

smalldataset folder has:
- Category.json
- Customer.json
- Employee.json
- Order.json
- OrderDetails.json
- Product.json

|# | Dataset | Number of objects |Description| 
|:---:|:---:|:---:|:---:| 
|1 | Customer.json | 3 | List of Customer data|
|2 | Employee.json | 2 | List of Employee data|
|3 | Category.json | 5 | List of Category data|
|4 | Product.json | 6 | List of Product data|
|5 | Order.json | 5 | List of Order data|
|6 | OrderDetails.json | 16 | List of OrderDetails data|


You call LoadDatabase("smalldataset") like code is shown as below:
```C#
SAprioriDatabase database = new SAprioriDatabase();
database.LoadDatabase("smalldataset");
database.FilterOrders(100, true);
SAprioriEngine sApriori = new SAprioriEngine();
double minSupport = 40;
double minConfident = 50;
SAprioriResult result = sApriori.runSAprioriModel(database, minSupport, minConfident);

foreach (SAprioriRule arule in result.StrongRules)
{
    string s = "[" + arule.X_Results_Description + " --> " + arule.Y_Results_Description + " " + String.Format("{0:0.00}", (arule.Confidence * 100)) + "%] "+"\r\n";
    Console.WriteLine(s);
}
```
Result:
```C#
[Vinamilk --> String Strawberry 100.00%] 
[Vinamilk --> Hamburger 66.67%] 
[Vinamilk --> Hamburger,String Strawberry 66.67%] 
[Vinamilk,Hamburger --> String Strawberry 100.00%] 
[Vinamilk,String Strawberry --> Hamburger 66.67%] 
[Hamburger --> G7 Coffee,Bien Hoa Sugar 50.00%] 
[Hamburger --> Bien Hoa Sugar 50.00%] 
[Hamburger --> G7 Coffee,String Strawberry 50.00%] 
[Hamburger --> String Strawberry 75.00%] 
[Hamburger --> G7 Coffee 75.00%] 
[Hamburger --> Vinamilk,String Strawberry 50.00%] 
[Hamburger --> Vinamilk 50.00%] 
[Hamburger,G7 Coffee --> Bien Hoa Sugar 66.67%] 
[Hamburger,G7 Coffee --> String Strawberry 66.67%] 
[Hamburger,String Strawberry --> G7 Coffee 66.67%] 
[Hamburger,String Strawberry --> Vinamilk 66.67%] 
[Hamburger,Bien Hoa Sugar --> G7 Coffee 100.00%] 
[G7 Coffee --> Hamburger 100.00%] 
[G7 Coffee --> Hamburger,String Strawberry 66.67%] 
[G7 Coffee --> String Strawberry 66.67%] 
[G7 Coffee --> Hamburger,Bien Hoa Sugar 66.67%] 
[G7 Coffee --> Bien Hoa Sugar 66.67%] 
[G7 Coffee,String Strawberry --> Hamburger 100.00%] 
[G7 Coffee,Bien Hoa Sugar --> Hamburger 100.00%] 
[String Strawberry --> Vinamilk 75.00%] 
[String Strawberry --> Vinamilk,Hamburger 50.00%] 
[String Strawberry --> Hamburger 75.00%] 
[String Strawberry --> G7 Coffee 50.00%] 
[String Strawberry --> Hamburger,G7 Coffee 50.00%] 
[Bien Hoa Sugar --> Hamburger 100.00%] 
[Bien Hoa Sugar --> G7 Coffee 100.00%] 
[Bien Hoa Sugar --> Hamburger,G7 Coffee 100.00%] 
```
# Example with largedataset - C# code
Download https://github.com/thanhtd32/SAprioriSystem/tree/main/dataset/largedataset and save it into local file.

Large Dataset AdventureWorks2017, I converted to json

largedataset folder has:
- Category.json
- Customer.json
- Employee.json
- Order.json
- OrderDetails.json
- Product.json

|# | Dataset | Number of objects |Description| 
|:---:|:---:|:---:|:---:| 
|1 | Customer.json | 19119 | List of Customer data|
|2 | Employee.json | 17 | List of Employee data|
|3 | Category.json | 37 | List of Category data|
|4 | Product.json | 504 | List of Product data|
|5 | Order.json | 31465 | List of Order data|
|6 | OrderDetails.json | 121317 | List of OrderDetails data|

You call LoadDatabase("largedataset") like code is shown as below:
```C#
SAprioriDatabase database = new SAprioriDatabase();
database.LoadDatabase("largedataset");
DateTime from = new DateTime(2011, 5, 1);
DateTime to= new DateTime(2011, 5, 31);
database.FilterOrders(from,to, true);
SAprioriEngine sApriori = new SAprioriEngine();
double minSupport = 20;
double minConfident = 80;
SAprioriResult result = sApriori.runSAprioriModel(database, minSupport, minConfident);

foreach (SAprioriRule arule in result.StrongRules)
{
    string s = "[" + arule.X_Results_Description + " --> " + arule.Y_Results_Description + " " + String.Format("{0:0.00}", (arule.Confidence * 100)) + "%] " + "\r\n";
    Console.WriteLine(s);
}
```
Result:
```C#
[Sport-100 Helmet, Red --> Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L 81.82%] 
[Sport-100 Helmet, Red --> Sport-100 Helmet, Black,AWC Logo Cap,Long-Sleeve Logo Jersey, L 81.82%] 
[Sport-100 Helmet, Red --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 90.91%] 
[Sport-100 Helmet, Red --> Sport-100 Helmet, Black,AWC Logo Cap 81.82%] 
[Sport-100 Helmet, Red --> AWC Logo Cap 90.91%] 
[Sport-100 Helmet, Red --> Sport-100 Helmet, Blue,AWC Logo Cap,Long-Sleeve Logo Jersey, L 81.82%] 
[Sport-100 Helmet, Red --> Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L 81.82%] 
[Sport-100 Helmet, Red --> Long-Sleeve Logo Jersey, L 90.91%] 
[Sport-100 Helmet, Red --> Sport-100 Helmet, Blue 81.82%] 
[Sport-100 Helmet, Red --> Sport-100 Helmet, Blue,AWC Logo Cap 81.82%] 
[Sport-100 Helmet, Red --> Sport-100 Helmet, Black 90.91%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Black --> AWC Logo Cap 90.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Black --> Long-Sleeve Logo Jersey, L 90.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Black --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 90.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Black --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 90.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Black,AWC Logo Cap --> Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> AWC Logo Cap 100.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Blue --> AWC Logo Cap 100.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Blue --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Blue --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Blue --> Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Blue,AWC Logo Cap --> Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Red,Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> AWC Logo Cap 100.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap --> Sport-100 Helmet, Blue 90.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap --> Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L 90.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap --> Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L 90.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap --> Sport-100 Helmet, Black 90.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap --> Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L 90.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap --> Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L 90.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap --> Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black 90.00%] 
[Sport-100 Helmet, Red,AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue 90.00%] 
[Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L --> AWC Logo Cap 100.00%] 
[Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue 90.00%] 
[Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black,AWC Logo Cap 90.00%] 
[Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black 90.00%] 
[Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black,AWC Logo Cap 90.00%] 
[Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue,AWC Logo Cap 90.00%] 
[Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue,AWC Logo Cap 90.00%] 
[Sport-100 Helmet, Black --> AWC Logo Cap 84.62%] 
[Sport-100 Helmet, Black --> Sport-100 Helmet, Blue 84.62%] 
[Sport-100 Helmet, Black --> Long-Sleeve Logo Jersey, L 84.62%] 
[Sport-100 Helmet, Black --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 84.62%] 
[Sport-100 Helmet, Black,Sport-100 Helmet, Blue --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 90.91%] 
[Sport-100 Helmet, Black,Sport-100 Helmet, Blue --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 90.91%] 
[Sport-100 Helmet, Black,Sport-100 Helmet, Blue --> AWC Logo Cap 90.91%] 
[Sport-100 Helmet, Black,Sport-100 Helmet, Blue --> Long-Sleeve Logo Jersey, L 90.91%] 
[Sport-100 Helmet, Black,Sport-100 Helmet, Blue,AWC Logo Cap --> Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Black,Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> AWC Logo Cap 100.00%] 
[Sport-100 Helmet, Black,AWC Logo Cap --> Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L 81.82%] 
[Sport-100 Helmet, Black,AWC Logo Cap --> Sport-100 Helmet, Red,Long-Sleeve Logo Jersey, L 81.82%] 
[Sport-100 Helmet, Black,AWC Logo Cap --> Sport-100 Helmet, Blue 90.91%] 
[Sport-100 Helmet, Black,AWC Logo Cap --> Long-Sleeve Logo Jersey, L 100.00%] 
[Sport-100 Helmet, Black,AWC Logo Cap --> Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L 90.91%] 
[Sport-100 Helmet, Black,AWC Logo Cap --> Sport-100 Helmet, Red 81.82%] 
[Sport-100 Helmet, Black,AWC Logo Cap --> Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L 90.91%] 
[Sport-100 Helmet, Black,AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue 90.91%] 
[Sport-100 Helmet, Black,AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red 81.82%] 
[Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red,AWC Logo Cap 81.82%] 
[Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue,AWC Logo Cap 90.91%] 
[Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> AWC Logo Cap 100.00%] 
[Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red 81.82%] 
[Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue 90.91%] 
[Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red,AWC Logo Cap 81.82%] 
[Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue,AWC Logo Cap 90.91%] 
[Sport-100 Helmet, Blue --> Long-Sleeve Logo Jersey, L 84.62%] 
[Sport-100 Helmet, Blue --> Sport-100 Helmet, Black 84.62%] 
[Sport-100 Helmet, Blue --> AWC Logo Cap,Long-Sleeve Logo Jersey, L 84.62%] 
[Sport-100 Helmet, Blue --> AWC Logo Cap 92.31%] 
[Sport-100 Helmet, Blue,AWC Logo Cap --> Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L 83.33%] 
[Sport-100 Helmet, Blue,AWC Logo Cap --> Sport-100 Helmet, Black 83.33%] 
[Sport-100 Helmet, Blue,AWC Logo Cap --> Sport-100 Helmet, Black,Long-Sleeve Logo Jersey, L 83.33%] 
[Sport-100 Helmet, Blue,AWC Logo Cap --> Long-Sleeve Logo Jersey, L 91.67%] 
[Sport-100 Helmet, Blue,AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black 90.91%] 
[Sport-100 Helmet, Blue,AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red 81.82%] 
[Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> AWC Logo Cap 100.00%] 
[Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red,AWC Logo Cap 81.82%] 
[Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red,AWC Logo Cap 81.82%] 
[Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black,AWC Logo Cap 90.91%] 
[Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Red 81.82%] 
[Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black,AWC Logo Cap 90.91%] 
[Sport-100 Helmet, Blue,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black 90.91%] 
[AWC Logo Cap --> Sport-100 Helmet, Blue 85.71%] 
[AWC Logo Cap --> Long-Sleeve Logo Jersey, L 92.86%] 
[AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Black 84.62%] 
[AWC Logo Cap,Long-Sleeve Logo Jersey, L --> Sport-100 Helmet, Blue 84.62%] 
[Long-Sleeve Logo Jersey, L --> AWC Logo Cap 86.67%] 
[LL Road Frame - Red, 60 --> Road-650 Black, 52 90.00%] 
[LL Road Frame - Red, 60 --> Road-650 Red, 60 90.00%] 
[LL Road Frame - Red, 60 --> Road-450 Red, 52 90.00%] 
[LL Road Frame - Red, 60 --> LL Road Frame - Black, 52 90.00%] 
[LL Road Frame - Red, 60 --> Road-450 Red, 52,Road-650 Red, 60 90.00%] 
[LL Road Frame - Red, 60,Road-450 Red, 52 --> Road-650 Red, 60 100.00%] 
[LL Road Frame - Red, 60,Road-650 Red, 60 --> Road-450 Red, 52 100.00%] 
[LL Road Frame - Black, 52 --> Road-450 Red, 52 90.00%] 
[LL Road Frame - Black, 52 --> LL Road Frame - Red, 60 90.00%] 
[LL Road Frame - Black, 52 --> Road-650 Red, 44 90.00%] 
[Road-450 Red, 58 --> Road-650 Black, 52 81.82%] 
[Road-450 Red, 58 --> Road-650 Red, 44 81.82%] 
[Road-450 Red, 58 --> Road-450 Red, 52 81.82%] 
[Road-450 Red, 52,Road-650 Red, 60 --> Road-650 Red, 44 81.82%] 
[Road-450 Red, 52,Road-650 Red, 60 --> Road-650 Red, 44,Road-650 Black, 52 81.82%] 
[Road-450 Red, 52,Road-650 Red, 60 --> Road-650 Red, 44,Road-650 Black, 52 81.82%] 
[Road-450 Red, 52,Road-650 Red, 60 --> Road-650 Black, 52 90.91%] 
[Road-450 Red, 52,Road-650 Red, 60 --> LL Road Frame - Red, 60 81.82%] 
[Road-450 Red, 52,Road-650 Red, 60,Road-650 Red, 44 --> Road-650 Black, 52 100.00%] 
[Road-450 Red, 52,Road-650 Red, 60,Road-650 Black, 52 --> Road-650 Red, 44 90.00%] 
[Road-450 Red, 52,Road-650 Red, 44 --> Road-650 Black, 52 83.33%] 
[Road-450 Red, 52,Road-650 Red, 44,Road-650 Black, 52 --> Road-650 Red, 60 90.00%] 
[Road-450 Red, 52,Road-650 Black, 52 --> Road-650 Red, 60 90.91%] 
[Road-450 Red, 52,Road-650 Black, 52 --> Road-650 Red, 60,Road-650 Red, 44 81.82%] 
[Road-450 Red, 52,Road-650 Black, 52 --> Road-650 Red, 60,Road-650 Red, 44 81.82%] 
[Road-450 Red, 52,Road-650 Black, 52 --> Road-650 Red, 44 90.91%] 
[Road-650 Red, 60 --> Road-650 Black, 52 85.71%] 
[Road-650 Red, 60,Road-650 Red, 44 --> Road-450 Red, 52 100.00%] 
[Road-650 Red, 60,Road-650 Red, 44 --> Road-450 Red, 52,Road-650 Black, 52 100.00%] 
[Road-650 Red, 60,Road-650 Red, 44 --> Road-650 Black, 52 100.00%] 
[Road-650 Red, 60,Road-650 Red, 44 --> Road-450 Red, 52,Road-650 Black, 52 100.00%] 
[Road-650 Red, 60,Road-650 Red, 44,Road-650 Black, 52 --> Road-450 Red, 52 100.00%] 
[Road-650 Red, 60,Road-650 Black, 52 --> Road-450 Red, 52 83.33%] 
[Road-650 Red, 44 --> Road-450 Red, 52 85.71%] 
[Road-650 Red, 44,Road-650 Black, 52 --> Road-450 Red, 52 90.91%] 
[Road-650 Red, 44,Road-650 Black, 52 --> Road-450 Red, 52,Road-650 Red, 60 81.82%] 
[Road-650 Red, 44,Road-650 Black, 52 --> Road-450 Red, 52,Road-650 Red, 60 81.82%] 
[Road-650 Red, 44,Road-650 Black, 52 --> Road-650 Red, 60 81.82%] 
[Road-650 Black, 52 --> Road-650 Red, 60 85.71%] 

```
