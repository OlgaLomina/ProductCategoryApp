using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;

namespace ProductCategoryApp
{
    public class ProductCategory
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public Subcategor[] Subcategory { get; set; }
    }
    public class Subcategor
    {
        public int subcategID { get; set; }
        public string subcategName { get; set; }
        public DateTime subcategModiffiedDate { get; set; }
        public string subcategRowguid { get; set; }
        public Categor[] Category { get; set; }
    }
    public class Categor
    {
        public int categID { get; set; }
        public string categName { get; set; }
        public DateTime categModiffiedDate { get; set; }
        public string categRowguid { get; set; }
    }
    public class Product_Subcategory
    {
        public ObjectId _id { get; set; }
        public int ProductSubcategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public string rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class Product_Category
    {
        public ObjectId _id { get; set; }
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public string rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class Product_Subcategory_1
    {
        public int ProductSubcategoryID { get; set; }
        public int ProductCategoryID { get; set; }
        public string Name { get; set; }
        public string rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string NameCateg { get; set; }
        public DateTime ModifiedDateCateg { get; set; }
        public string rowguidCateg { get; set; }
    }
    public class Program
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        public static ProductCategory GetProduct()
        {
            Console.WriteLine("Please enter ProductID : ");
            string PId = Console.ReadLine();
            Console.WriteLine("Please enter product Name : ");
            string PNm = Console.ReadLine();
            Console.WriteLine("Please enter ProductNumber : ");
            string PNum = Console.ReadLine();
            Console.WriteLine("Please enter Color : ");
            string color = Console.ReadLine();
            Console.WriteLine("Please enter StandardCost : ");
            string standardCost = Console.ReadLine();
            Console.WriteLine("Please enter ListPrice : ");
            string listPrice = Console.ReadLine();
            Console.WriteLine("Please enter subcategID : ");
            string subcatID = Console.ReadLine();
            Console.WriteLine("Please enter subcategName : ");
            string subcatName = Console.ReadLine();
            //Console.WriteLine("Please enter subcategModiffiedDate : ");
            //string subcatModDate = Console.ReadLine();
            Console.WriteLine("Please enter subcategRowguid : ");
            string subcatRowguid = Console.ReadLine();
            Console.WriteLine("Please enter categID : ");
            string catID = Console.ReadLine();
            Console.WriteLine("Please enter categName : ");
            string catName = Console.ReadLine();
            //Console.WriteLine("Please enter categModiffiedDate : ");
            //string catModDate = Console.ReadLine();
            Console.WriteLine("Please enter categRowguid : ");
            string categRowguid = Console.ReadLine();
            try
            {
                decimal cost = Convert.ToDecimal(standardCost);
            }
            catch (System.FormatException)
            {
                Console.WriteLine("StandardCost is not corrected");
                Console.WriteLine("Please enter StandardCost : ");
                standardCost = Console.ReadLine();

            }
            try
            {
                decimal price = Convert.ToDecimal(listPrice);
            }
            catch (System.FormatException)
            {
                Console.WriteLine("ListPrice is not corrected");
                Console.WriteLine("Please enter ListPrice : ");
                listPrice = Console.ReadLine();
            }

            ProductCategory productcateg = new ProductCategory()
            {

                ProductID = int.Parse(PId),
                Name = PNm,
                ProductNumber = PNum,
                StandardCost = Convert.ToDecimal(standardCost),
                ListPrice = Convert.ToDecimal(listPrice),
                Color = color,
                Subcategory = new Subcategor[1],
            };

            productcateg.Subcategory[0] = new Subcategor()
            {
                subcategID = int.Parse(subcatID),
                subcategName = subcatName,
                subcategModiffiedDate = DateTime.Now,
                subcategRowguid = subcatRowguid,
                Category = new Categor[1],
            };

            productcateg.Subcategory[0].Category[0] = new Categor()
            {
                categID = int.Parse(catID),
                categName = catName,
                categModiffiedDate = DateTime.Now,
                categRowguid = categRowguid,
            };

            return productcateg;
            
 
        }

        public void CRUDwithMongoDb()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("adventureworks");
            var collection = _database.GetCollection<ProductCategory>("ProductProduct");

            Console.WriteLine
                ("Press select your option from the following\n1 - Insert\n2 - Update One Document\n3 - Delete\n4 - Read All\n5 - Join Subcategory&Category\n");
            string userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    //Insert  
                    collection.InsertOneAsync(GetProduct());

                    break;

                case "2":
                    //Update  
                    var obj1 = GetProduct();

                    collection.FindOneAndUpdate<ProductCategory>
                        (Builders<ProductCategory>.Filter.Eq("ProductID", obj1.ProductID),
                            Builders<ProductCategory>.Update.Set("Name", obj1.Name).
                            Set("ProductNumber", obj1.ProductNumber).
                            Set("StandardCost", obj1.StandardCost).Set("ListPrice", obj1.ListPrice).
                            Set("Color", obj1.Color).
                            Set(p => p.Subcategory[0].subcategID, obj1.Subcategory[0].subcategID).
                            Set(p => p.Subcategory[0].subcategName, obj1.Subcategory[0].subcategName).
                            Set(p => p.Subcategory[0].subcategRowguid, obj1.Subcategory[0].subcategRowguid).
                            Set(p => p.Subcategory[0].Category[0].categID, obj1.Subcategory[0].Category[0].categID).
                            Set(p => p.Subcategory[0].Category[0].categName, obj1.Subcategory[0].Category[0].categName).
                            Set(p => p.Subcategory[0].Category[0].categRowguid, obj1.Subcategory[0].Category[0].categRowguid));

                    break;

                case "3":
                    //Find and Delete  
                    Console.WriteLine("Please Enter the ProductID to delete the record(so called document) : ");
                    var deleteProduct = Console.ReadLine();
                    var Deleteone = collection.DeleteOne(s => s.ProductID == int.Parse(deleteProduct));
                    if (Deleteone.IsAcknowledged)
                    {
                        Console.WriteLine("Product with ID {0} was deleted", int.Parse(deleteProduct));
                    }
                    else
                    {
                        Console.WriteLine("Product with ID {0} was NOT deleted", int.Parse(deleteProduct));
                    }
                    break;

                case "4":
                    //Read all existing document

                    var products = collection.AsQueryable<ProductCategory>()
                        /*.Where(p => p.ProductID == 400)*/
                        .OrderBy(x => x.ProductID).ToList();
                    foreach (var y in products)
                    {
                        Console.WriteLine(y._id + "  " + y.ProductID + "\t" + y.Name + "\t" +
                          y.ProductNumber + "\t" + y.StandardCost + "\t" +
                          y.ListPrice + "\t" + y.Color);
                        //Console.WriteLine("Number of Subcategories = {0}", y.Subcategory.Length);

                        for (int i = 0; i < y.Subcategory.Length; ++i)
                        {
                            Console.WriteLine("Subcategory { " + y.Subcategory[i].subcategID + "\t" +
                            y.Subcategory[i].subcategName + "\t" + 
                            y.Subcategory[i].subcategModiffiedDate + "\t" + y.Subcategory[i].subcategRowguid + "\t" +
                            "}");

                            //Console.WriteLine("\t" + "Number of Categories = {0}", y.Subcategory[i].Category.Length);

                            for (int j = 0; j < y.Subcategory[i].Category.Length; ++j)
                            {
                                Console.WriteLine("\t" + "Category { " + y.Subcategory[i].Category[j].categID + "\t" + y.Subcategory[i].Category[j].categName + "\t" +
                                y.Subcategory[i].Category[j].categModiffiedDate + "\t" + y.Subcategory[i].Category[j].categRowguid + "\t" +
                                "}");
                            }
                        }

                        /*
                        var results = _collection.AsQueryable()
                          .Where(p => p.ProductID == y.ProductID && p.Subcategory == y.Subcategory)
                          .SelectMany(p => p.Subcategory, (p, subcategory) => new
                          {
                              ProductID = p.ProductID,
                              subcategID = subcategory.subcategID,
                              Category = subcategory.Category
                          }
                          )
                          .SelectMany(p => p.Category, (p, category) => new
                          {
                              ProductID = p.ProductID,
                              subcategID = p.subcategID,
                              categID = category.categID
                          }
                          )
                          .GroupBy(p => new { ProductID = p.ProductID, subcategID = p.subcategID, categID = p.categID })
                          .GroupBy(p => new { ProductID = p.Key.ProductID, subcategID = p.Key.subcategID },
                            (k, s) => new { Key = k, count = s.Count() }
                          )
                          .GroupBy(p => p.Key.ProductID,
                            (k, s) => new
                            {
                                ProductID = k,
                                subcategCount = s.Count(),
                                categCount = s.Sum(x => x.count)
                            }
                          );                   
                    */
                    }
                    break;

                case "5":
                    IMongoCollection<Product_Subcategory> _collection1 = _database.GetCollection<Product_Subcategory>("ProductSubcategory");
                    IMongoCollection<Product_Category> _collection2 = _database.GetCollection<Product_Category>("ProductCategory");
                    //var collectionConnected = _collection1.AsQueryable().Where(d => d.ProductSubcategoryID == 37);

                    /*var query = from p in _collection1.AsQueryable().OrderBy(p => p.ProductSubcategoryID)
                                join o in _collection2.AsQueryable() on p.ProductCategoryID equals o.ProductCategoryID into category
                                select 
                                new Product_Subcategory_1()
                                {
                                    ProductSubcategoryID = p.ProductSubcategoryID, 
                                    Name = p.Name,
                                    rowguid = p.rowguid,
                                    ModifiedDate = p.ModifiedDate,
                                    ProductCategoryID = p.ProductCategoryID,
                                    NameCateg = o.Name,
                                    ModifiedDateCateg = o.ModifiedDate,
                                    rowguidCateg = o.rowguid
                                };

                    var SubCateg = query.ToList();
                    foreach (var a in SubCateg)
                    {
                        Console.WriteLine(a.ProductSubcategoryID + "  " + a.Name + "\t" +
                          a.ModifiedDate + "\t" + a.rowguid + "\t" + a.ProductCategoryID +
                          "\n\t" + a.NameCateg + "\t" + a.ModifiedDateCateg + "\t" + a.rowguidCateg);

                    }
                    Console.WriteLine("1---------------------------");
                    Console.ReadKey();

                    var sub_cat = _collection1.AsQueryable().OrderBy(x => x.ProductSubcategoryID).ToList();
                    var cat = _collection2.AsQueryable().ToList();

                    var list = (from p in sub_cat
                                join o in cat on p.ProductCategoryID equals o.ProductCategoryID
                                select new
                                {
                                    ProductSubcategoryID = p.ProductSubcategoryID,
                                    Name = p.Name,
                                    rowguid = p.rowguid,
                                    ModifiedDate = p.ModifiedDate,
                                    ProductCategoryID = p.ProductCategoryID,
                                    NameCateg = o.Name,
                                    ModifiedDateCateg = o.ModifiedDate,
                                    rowguidCateg = o.rowguid
                                });

                    foreach (var e in list)
                    {
                        Console.WriteLine(e.ProductSubcategoryID + "  " + e.Name + "\t" +
                          e.ModifiedDate + "\t" + e.rowguid + "\t" + e.ProductCategoryID +
                          "\n\t" + e.NameCateg + "\t" + e.ModifiedDateCateg + "\t" + e.rowguidCateg);
                    }

                    Console.WriteLine("2---------------------------");
                    Console.ReadKey();
                    */

                    var query1 = from p in _collection1.AsQueryable().OrderBy(p => p.ProductSubcategoryID)
                                 join o in _collection2.AsQueryable() on p.ProductCategoryID equals o.ProductCategoryID into category
                                 select new
                                 {
                                     p.ProductSubcategoryID,
                                     p.Name,
                                     p.rowguid,
                                     p.ModifiedDate,
                                     p.ProductCategoryID,
                                     NameCateg = category
                                 };
                    var SubCateg1 = query1.ToList();
                    foreach (var a1 in SubCateg1)
                    {
                        Console.WriteLine(a1.ProductSubcategoryID + "  " + a1.Name + "\t" +
                          a1.ModifiedDate + "\t" + a1.rowguid + "\t" + a1.ProductCategoryID);
                        foreach (var a2 in a1.NameCateg)
                        {
                            Console.WriteLine("\t{" + a2.Name + "\t" + a2.ModifiedDate + "\t" + a2.rowguid + "}");
                        }
                        
                    }
                    //Console.WriteLine("3---------------------------");
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();

                    break;

                default:
                    Console.WriteLine("Please choose a correct option");
                    break;
            }

            //To continue with Program  
            Console.WriteLine("\n--------------------------------------------------------------\nPress Y for continue...\n");
            string userChoice = Console.ReadLine();

            if (userChoice == "Y" || userChoice == "y")
            {
                this.CRUDwithMongoDb();
            }
        }

        public static void Main(string[] args)
        {
            Program p = new Program();
            p.CRUDwithMongoDb();

            //Hold the screen by logic  
            Console.WriteLine("Press any key to teminated the program");
            Console.ReadKey();
        }
    }
}