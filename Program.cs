/* 
    Weekly Mini Project W40:
    
    Product List program for entering and listing products

    Each Product has property Category, ProductName, Price
*/

using System.Diagnostics;
using System.Runtime.CompilerServices;
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

            string? inputString = ReadLine();

            if( inputString is not null )
            {
                List<Product> foundProducts = productList.FilteredByName( inputString.Trim() );

                if( foundProducts.Count == 0 )
                {
                    WriteLine("Not found.");
                }
                else
                {
                    WriteLine("--------------------------------------------------");
                    foreach( Product product in foundProducts )
                    {
                        WriteLine( product.ToString() );
                    }
                    WriteLine("--------------------------------------------------");
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

                if( int.TryParse( inputString, out price ) ) 
                {
                    done = true;
                }
                else
                {
                    WriteLine("Price incorrectly entered");
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
            WriteLine("------------------------------------------------------------");
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
            WriteLine("------------------------------------------------------------");

        }

        /*
            Main Program entry point
        */
        private static void Main( string[] args )
        {
            ProductList? productList = [];
            
            WriteLine("------------------------------------------------------------");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("To enter a new product follow the steps or enter 'Q' to Quit");
            ResetColor();

            bool done = false;
            

            // Initial product input Loop
            while( !done )
            {
                done = AddProduct( productList );
            }

            PrintSummary( productList );

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
                        done = AddProduct(productList);
                    }
                    else if( inputCommand.Equals("I"))
                    {
                        // Undocumented feature, just for testing ;) 
                        string jsonString = """[{"Category": "Electronics","Name": "TV", "Price": 123}]""";
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

                if( done )
                {
                    PrintSummary( productList );
                }

            } // while


        }
    }

}