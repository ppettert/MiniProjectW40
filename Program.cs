/* 
    Weekly Mini Project W40:
    
    Product List program for entering and listing products

    Each Product has property Category, ProductName, Price
*/

using static System.Console;
using System.Text.Json;

namespace MiniProjectW40
{
    public class Program
    {

        /*
            Main loop helper function to search from user input in ProductList and print it
            in:  productList    a ProductList to search within
        */
        private static void Search( ProductList productList )
        {
            Write("Enter a Product Name to search for: ");

            // string? trimmedInputString = Console.ReadLine()?.Trim(); 
            string? inputString = ReadLine();

            if( inputString is not null )
            {
                String trimmedInputString = inputString.Trim();
                List<Product> foundProducts = productList.FilteredByName( trimmedInputString );

                // TODO: Change .Count calls to Any() ?
                if( foundProducts.Count == 0 )
                {
                    WriteLine("Not found.");
                }
                else
                {
                    List<Product> sortedProductList = productList.OrderBy( product => product.Price ).ToList();
                    
                    WriteLine("".PadRight(50,'-'));
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
                    ResetColor();
                    
                    foreach( Product product in sortedProductList )
                    {
                        if( product.Name.Equals( trimmedInputString ) )
                        {
                            ForegroundColor = ConsoleColor.Magenta;
                            WriteLine( product.ToString() );
                            ResetColor();
                        }
                        else
                        {
                            WriteLine( product.ToString() );
                        }
                    }
                    WriteLine("".PadRight(50,'-'));
                }

            }

        }

        /*
            Main loop helper function for input and adding of new Product
            in:         productList    ProductList to add product to
            returns:    bool           true if user enter Q for Quit, otherwise false
        */
        private static bool AddProduct( ProductList productList )
        {
            string productCategory = "";
            string productName = "";
            int price = 0;

            WriteLine("".PadRight(60,'-'));
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("To enter a new product follow the steps or enter 'Q' to Quit");
            ResetColor();

            bool done = false;
            while( !done )
            {
                Write( "Enter a Product Category: " );
                string? inputString = ReadLine();
                if( inputString is not null )
                {
                    string trimmedInputString = inputString.Trim();

                    if( trimmedInputString.ToUpper().Equals("Q") )
                    {
                        // User wants to quit
                        return true;  
                    }
                    else if( !trimmedInputString.Equals("") )
                    {
                        productCategory = inputString.Trim();
                        done = true;
                    }
                }
            }

            done = false;
            while( !done )
            {
                Write( "Enter a Product Name: ");
                string? inputString = ReadLine();
                if( inputString is not null )
                {
                    string trimmedInputString = inputString.Trim();

                    if( trimmedInputString.ToUpper().Equals("Q") )
                    {
                        // User wants to quit
                        return true;  
                    }
                    else if( !inputString.Trim().Equals("") )
                    {
                        productName = inputString.Trim();
                        done = true;
                    }
                }
            }

            done = false;
            while( !done )
            {
                Write( "Enter a Price: ");
                string? inputString = ReadLine();

                if( inputString is not null )
                {
                    string trimmedInputString = inputString.Trim();

                    if (trimmedInputString.ToUpper().Equals("Q"))
                    {
                        // User wants to quit
                        return true;
                    }
                    else if (int.TryParse(inputString, out price))
                    {
                        done = true;
                    }
                    else
                    {
                        WriteLine("Price incorrectly entered");
                    }
                }
            }

            productList.Add( new Product( productCategory, productName, price ) );
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Product successfully added!");
            ResetColor();
            return false;

        }    


        /*
            Main loop helper function for printing product list and total price
            in:         productList    ProductList to print
        */
        private static void PrintSummary( ProductList productList )
        {
            if( productList.Count == 0 )
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Empty list!");
                ResetColor();
                return;
            } 

            WriteLine("".PadRight( 50, '-' ) );
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
            ResetColor();
            
            List<Product> sortedProductList = productList.OrderBy( product => product.Price ).ToList();

            foreach( Product product in sortedProductList )
            {
                WriteLine( product.ToString() );
            }

            WriteLine();
            WriteLine("".PadRight(20) + "Total Amount: ".PadRight(20) + productList.Sum( product => product.Price ) );
            WriteLine("".PadRight( 50, '-' ) );

        }

        /*
            Main Program entry point
        */
        private static void Main( string[] args )
        {
            ProductList? productList = [];
            
            bool done = false;
            
            // Initial product input Loop
            while( !done )
            {
                done = AddProduct( productList );
            }

            if( productList.Count > 0 ) 
            {
                PrintSummary( productList );
            }    

            done = false; 


            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            
            // Main Input Loop
            while( !done ) 
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("P to enter new product, S to search product, Q to quit: ");
                ResetColor();

                string? inputString = ReadLine();

                if( inputString is not null )
                {
                    string inputCommand = inputString.Trim().ToUpper();

                    if( inputCommand.Equals("Q") )
                    {
                        done = true;
                    }
                    else if( inputCommand.Equals("S") )
                    {
                        Search(productList);
                    }
                    else if( inputCommand.Equals("P") )
                    {
                        while( !done )
                        {
                            done = AddProduct( productList );
                        }

                        if( productList.Count > 0 ) 
                        {
                            PrintSummary( productList );
                        }    

                        done = false;
                    }
                    else if( inputCommand.Equals("I"))
                    {
                        // Undocumented feature, just for testing ;) 
                        string jsonString = """
                                            [
                                                {"Category": "Electronics", "Name": "Computer",     "Price": 19900},   
                                                {"Category": "Electronics", "Name": "Radio",        "Price": 199},
                                                {"Category": "Electronics", "Name": "TV",           "Price": 6990},
                                                {"Category": "Toy",         "Name": "Teddybear",    "Price": 99},
                                                {"Category": "Tool",        "Name": "Powerdrill",   "Price": 599},
                                                {"Category": "Electronics", "Name": "TV",           "Price": 26990}
                                            ]
                                            """;
                        ProductList? tempProductList = JsonSerializer.Deserialize<ProductList>( jsonString, jsonOptions );
                        if( tempProductList is not null )
                        {
                            productList = tempProductList;
                        }

                    }
                    else if( inputCommand.Equals("J") )
                    {
                        // Dump as JSON
                        WriteLine( JsonSerializer.Serialize( productList, jsonOptions ) );
                    }
                    else
                    {
                        WriteLine("Unknown command");
                    }

                }

                if( done && productList.Count > 0 )
                {                                   
                    PrintSummary( productList );
                }

            } // while


        }
    }

}