using System.Collections;

namespace MiniProjectW40
{
    /*
        Class: ProductList 
        Inherts List<Product> to provide a non-generic FilteredByName method
    */
    public class ProductList : List<Product>
    {
        private List<Product> productList = [];

        /*
            Method:     FilteredByName 
            in:         productName     a string containing product name to use as filter
            returns:    the filtered List with Product items matching productName string
        */
        public List<Product> FilteredByName(string productName)
        {
            List<Product> filteredList = productList.Where(product => product.Name.Contains(productName)).ToList();
            
          //  List<Product> filteredList = productList.Where(product => product.Price == 111).ToList();
            return filteredList;
        }

    }

}