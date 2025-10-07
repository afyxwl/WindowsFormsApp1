using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public class Orders
    {
        public void SaveListBoxToFile(ListBox listBox)
        {
            if (listBox.Items.Count == 0)
            {
                MessageBox.Show("Save failed — the list is empty!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = $"Orders_{DateTime.Now:yyyyMMdd_HHmm}.txt";
            string filePath = Path.Combine(Application.StartupPath, fileName);

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var item in listBox.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }

            MessageBox.Show($"File saved successfully:\n{fileName}", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
