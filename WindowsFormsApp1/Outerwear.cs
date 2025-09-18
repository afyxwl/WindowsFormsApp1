using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    namespace WindowsFormsApp1
    {
        public class Outerwear
        {
            public string Element { get; set; }
            public string Brand { get; set; }
            public string Size { get; set; }
            public double Price { get; set; }

            // Конструктор за замовчуванням
            public Outerwear()
            {
                Element = "";
                Brand = "";
                Size = "";
                Price = 0.0;
            }

            // Конструктор з параметрами
            public Outerwear(string element, string brand, string size, double price)
            {
                Element = element;
                Brand = brand;
                Size = size;
                Price = price;
            }
          

            // Обчислення ціни зі знижкою 10%
            public double PriceWithDiscount(double discount)
            {
                return Price * 100/discount;
            }

            // Обчислення сумарної ціни для кількох одиниць
            public double TotalPrice(int quantity)
            {
                return Price * quantity;
            }

            // Метод виводу інформації
            public string Info()
            {
                return $"Item: {Element}, Brand: {Brand}, Size: {Size}, Price: {Price} €, With Discount: {PriceWithDiscount():0.00} €";
            }
        }
    }

}
