# SAprioriSystem
SApriori Engine to Predict the seasonal consumption behavior of consumers based on Object Relational Mapping model and S-Apriori algorithm
# Example with smalldataset
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
