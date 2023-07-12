using ClassLibraryOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethodsDemo
{
    // Static class for extension method
    public static class ProductExtensions
    {
        // Extension Method Of Product Class
        public static double GetDiscount(this Product product)
        {
            return (product.ProductCost * product.DiscountPercentage) / 100;
        }

    }
}
