# SAprioriSystem
SApriori Engine to Predict the seasonal consumption behavior of consumers based on Object Relational Mapping model and S-Apriori algorithm
# Example with smalldataset - C# code
Download https://github.com/thanhtd32/SAprioriSystem/tree/main/dataset/smalldataset and save it into local file.
This folder has:
*Category.json
*Customer.json
*Employee.json
*Order.json
*OrderDetails.json
*Product.json
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
