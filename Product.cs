
namespace MiniProjectW40
{
    public class Product
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public Product(string category, string name, int price)
        {
            Category = category;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return Category?.PadRight(20) + Name?.PadRight(20) + Price;
        }

    }

}