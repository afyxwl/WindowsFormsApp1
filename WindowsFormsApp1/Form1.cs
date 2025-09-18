using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
        List<Outerwear> closet = new List<Outerwear>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Outerwear o = new Outerwear("Coat", "Armani", "M", 3500);
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
            string brand = comboBox4.Text.Trim();
            string size = comboBox5.Text.Trim();
            double price;
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "")
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

            Outerwear o = new Outerwear(element, brand, size, price);
            closet.Add(o);

            string info = $"Added: {o.Info()}";
            if (discountPercent > 0)
                info += $" (Discount: {discountPercent}%)";
            info += $" | Total: {o.TotalPrice(1):0.##} €";

            listBox1.Items.Add(info);


            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
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

        private void button5_Click(object sender, EventArgs e)
        {
            textBox5.Visible= true;
            label5.Visible= true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
                    }
    }

    
    
}
