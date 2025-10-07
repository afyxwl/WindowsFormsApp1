using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1.WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        List<PetClothing> closet = new List<PetClothing>();
             Dictionary<string, List<string>> itemsByObject = new Dictionary<string, List<string>>()
        {
            { "Clothing", new List<string> { "Sweater", "Jacket", "Raincoat" } },
            { "Toys", new List<string> { "Ball", "Rope", "Chew Toy" } },
            { "Spreys", new List<string> { "Flea Spray", "Perfume" } },
            { "Nutrition", new List<string> { "Dry Food", "Wet Food" } },
            { "Vitamins", new List<string> { "Vitamin A", "Vitamin C" } },
            { "Fur", new List<string> { "Comb", "Shampoo" } }
        };

        Dictionary<string, List<string>> brandsByItem = new Dictionary<string, List<string>>()
        {
            { "Sweater", new List<string> { "Zooland", "PetStyle" } },
            { "Jacket", new List<string> { "DogFashion", "HappyPaws" } },
            { "Raincoat", new List<string> { "RainyPet", "ZooWear" } },
            { "Ball", new List<string> { "PlayMax", "ZooToys" } },
            { "Rope", new List<string> { "ChewFun", "PetStrong" } },
            { "Chew Toy", new List<string> { "BiteMe", "ChewyZoo" } },
            { "Flea Spray", new List<string> { "AntiFlea", "SafePet" } },
            { "Perfume", new List<string> { "PetFresh", "DoggyScent" } },
            { "Dry Food", new List<string> { "RoyalCanin", "Pedigree" } },
            { "Wet Food", new List<string> { "Whiskas", "Purina" } },
            { "Vitamin A", new List<string> { "PetVita", "ZooHealth" } },
            { "Vitamin C", new List<string> { "StrongPet", "C-Vital" } },
            { "Comb", new List<string> { "FurCare", "PetBrush" } },
            { "Shampoo", new List<string> { "CleanPaw", "FurWash" } }
        };

        Dictionary<string, decimal> priceByItem = new Dictionary<string, decimal>()
        {
            { "Sweater", 500 },
            { "Jacket", 800 },
            { "Raincoat", 600 },
            { "Ball", 100 },
            { "Rope", 150 },
            { "Chew Toy", 200 },
            { "Flea Spray", 250 },
            { "Perfume", 300 },
            { "Dry Food", 700 },
            { "Wet Food", 500 },
            { "Vitamin A", 400 },
            { "Vitamin C", 450 },
            { "Comb", 120 },
            { "Shampoo", 180 }
        };

        public Form1()
        {
            
                InitializeComponent();
                comboBox1.Items.AddRange(new string[]
                {
                "Clothing", "Toys", "Spreys", "Nutrition", "Vitamins", "Fur"
                });

                comboBox1.SelectedIndexChanged += ComboBoxObject_SelectedIndexChanged;
                comboBox2.SelectedIndexChanged += ComboBoxItem_SelectedIndexChanged;
            }

        private void ComboBoxObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedObject = comboBox1.SelectedItem.ToString();

            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            textBox2.Clear();

            if (itemsByObject.ContainsKey(selectedObject))
                comboBox2.Items.AddRange(itemsByObject[selectedObject].ToArray());

            comboBox4.Visible = (selectedObject == "Clothing");
        }

        private void ComboBoxItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox2.SelectedItem.ToString();

            comboBox3.Items.Clear();
            textBox2.Clear();

            if (brandsByItem.ContainsKey(selectedItem))
                comboBox3.Items.AddRange(brandsByItem[selectedItem].ToArray());

            if (priceByItem.ContainsKey(selectedItem))
                textBox2.Text = priceByItem[selectedItem].ToString();
        }
    


        private void button4_Click(object sender, EventArgs e)
        {
            PetClothing o = new PetClothing("Coat", "Armani", "M", 3500);
            closet.Add(o);
            listBox1.Items.Add("Added by default: " + o.Info());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string element = comboBox1.Text.Trim();
            string brand = comboBox3.Text.Trim();
            string size = comboBox4.Text.Trim();
            double price;
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Fill all of the boxes!", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string rawPrice = textBox2.Text.Replace("€", "").Trim();

            if (!double.TryParse(rawPrice, out price))
            {
                MessageBox.Show("Only numbers allowed in Price!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double discountPercent = 0;
            if (!string.IsNullOrWhiteSpace(textBox5.Text))
            {
                if (!double.TryParse(textBox5.Text.Replace("%", "").Trim(), out discountPercent))
                {
                    MessageBox.Show("Only numbers allowed in Discount!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (discountPercent < 0 || discountPercent > 100)
                {
                    MessageBox.Show("Discount must be between 0 and 100!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                price = price * 100 / discountPercent;
            }

            PetClothing o = new PetClothing(element, brand, size, price);
            closet.Add(o);

            string info = $"Added: {o.Info()}";
            if (discountPercent > 0)
                info += $" (Discount: {discountPercent}%)";
            info += $" | Total: {o.TotalPrice:0.##} €";

            listBox1.Items.Add(info);


            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var o in closet)
            {
                listBox1.Items.Add(o.Info());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            string input = textBox2.Text.Replace("€", "").Trim();

            if (decimal.TryParse(input, out decimal value))
            {
                textBox2.Text = value + " €";
                textBox2.SelectionStart = textBox2.Text.Length; 
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            string input = textBox5.Text.Replace("%", "").Trim();

            if (decimal.TryParse(input, out decimal value))
            {
                textBox5.Text = value + " %";
                textBox5.SelectionStart = textBox5.Text.Length;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox5.Visible= true;
            label5.Visible= true;
        }

        private void button6_Click(object sender, EventArgs e)

        {
            Orders saver = new Orders();
            saver.SaveListBoxToFile(listBox1);
        }

    }

}
