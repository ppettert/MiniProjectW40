/* 
    Weekly Mini Project W40:
    
    Product List program for entering and 
    Each Product has property Category, ProductName, Price
*/

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Console;

public class Product
{
    public string? Category { get; set; }
    public string? Name { get; set; }
    public int Price { get; set; }

    public override string ToString() 
    {
        return Category?.PadRight(20) + Name?.PadRight(20) + Price;
    }

}

public class ProductList 
{

}


public class Program
{

    private static void Main(string[] args)
    {
        bool exitFlag = false;

        List<Product> productList = [];

        // Input Loop
        do
        {
            WriteLine("------------------------------------------------------------");
            WriteLine("To Enter a new product follow the steps or Enter 'Q' to Quit");


            WriteLine("Enter Product Category: ");
            string? inputString = ReadLine();

            if (inputString is not null)
            {
                if (inputString.Trim().ToLower().Equals("q"))
                {
                    exitFlag = true;
                }
                else
                {
                    Product product = new()
                    {
                        Category = inputString.Trim()
                    };

                    WriteLine("Enter Product Name: ");
                    inputString = ReadLine();

                    if (inputString is not null) product.Name = inputString.Trim();

                    WriteLine("Enter Product Price: ");
                    inputString = ReadLine();

                    if (inputString is not null)
                    {

                        if ( int.TryParse(inputString.Trim(), out int price))
                        {
                            product.Price = price; 
                        }

                    }

                    productList.Add(product);

                }

            }

        } while (exitFlag is false);

        WriteLine("------------------------------------------------------------");
        WriteLine( "Category".PadRight(20) + "Product".PadRight(20) + "Price" );
        foreach( Product product in productList )
        {
            WriteLine( product.ToString() );
        }
        WriteLine("------------------------------------------------------------");
    }
}