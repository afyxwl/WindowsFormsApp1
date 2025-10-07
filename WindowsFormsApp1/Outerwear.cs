using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    namespace WindowsFormsApp1
    {
        public interface IProduct
        {
            string Brand { get; set; }
            double Price { get; set; }
            double GetPriceWithDiscount(double discount);
            string GetInfo();
        }

        public abstract class Product : IProduct
        {
            public string Brand { get; set; }
            public double Price { get; set; }

            protected Product()
            {
                Brand = "";
                Price = 0.0;
            }

            protected Product(string brand, double price)
            {
                Brand = brand;
                Price = price;
            }

            public virtual double GetPriceWithDiscount(double discount)
            {
                if (discount < 0 || discount > 1)
                    throw new ArgumentException("Discount must be between 0 and 1.");

                return Price - (Price * discount);
            }

            public abstract string GetInfo();
        }

    
            public class PetClothing
            {
                public string Category { get; set; }     // Наприклад: Clothing, Toys...
                public string Brand { get; set; }        // Наприклад: Armani, PetStyle...
                public string Size { get; set; }         // Наприклад: M, L, XL...
                public double Price { get; set; }        // Ціна
                public double TotalPrice => Price;       // Властивість для відображення (можна буде змінити з урахуванням знижки)

                public PetClothing(string category, string brand, string size, double price)
                {
                    Category = category;
                    Brand = brand;
                    Size = size;
                    Price = price;
                }

                public string Info()
                {
                    return $"{Category} | {Brand} | {Size} | {Price:0.##} €";
                }
            
        


        public void ChangeSize(string newSize)
            {
                Size = newSize;
            }

            public bool IsDiscountAvailable(double discount)
            {
                return discount > 0 && discount <= 0.5;
            }
        }
    }
}