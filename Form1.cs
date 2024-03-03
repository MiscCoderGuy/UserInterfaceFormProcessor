using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UserInterfaceFormProcessor
{

    public partial class SourceFrontUI : Form
    {
        string firstName = String.Empty,
            lastName = String.Empty,
            addr1 = String.Empty,
            addr2 = String.Empty,
            city = String.Empty,
            state = String.Empty;

        int zip = 0;

        private List<DonorRecord> donors = new List<DonorRecord>();

        public SourceFrontUI()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(textBox1_Enter);
            textBox2.KeyPress += new KeyPressEventHandler(textBox2_Enter);
            textBox3.KeyPress += new KeyPressEventHandler(textBox3_Enter);
            textBox4.KeyPress += new KeyPressEventHandler(textBox4_Enter);
            textBox5.KeyPress += new KeyPressEventHandler(textBox5_Enter);
            textBox6.KeyPress += new KeyPressEventHandler(textBox6_Enter);
            textBox7.KeyPress += new KeyPressEventHandler(textBox7_Enter);
            Continue.KeyPress += new KeyPressEventHandler(textBox8_Enter);
        }

        private void WriteToCsv(List<DonorRecord> donors, string outFile)
        {
            if (donors.Count == 0)
            {
                MessageBox.Show("There are no donors to save.");
                return;
            }
            else
            {
                MessageBox.Show("There are records in List<DonorRecord> donors.");
            }

            // Create a DataTable
            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("FirstName", typeof(string));
            dataTable1.Columns.Add("LastName", typeof(string));
            dataTable1.Columns.Add("Address1", typeof(string));
            dataTable1.Columns.Add("Address2", typeof(string));
            dataTable1.Columns.Add("City", typeof(string));
            dataTable1.Columns.Add("State", typeof(string));
            dataTable1.Columns.Add("ZipCode", typeof(int));

            // Populate DataTable with donor data
            foreach (DonorRecord donorRecord in donors)
            {
                dataTable1.Rows.Add(donorRecord.FirstName, donorRecord.LastName, donorRecord.Address1,
                    donorRecord.Address2, donorRecord.City, donorRecord.State, donorRecord.ZipCode);
            }

            using (StreamWriter writer = new StreamWriter(outFile))
            {
                // Write the column header row
                foreach (DataColumn column in dataTable1.Columns)
                {
                    writer.Write(column.ColumnName);
                    writer.Write(",");
                }
                writer.WriteLine();

                // Write the data rows
                int rec = 0;
                foreach (DataRow row in dataTable1.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        writer.Write(item);
                        writer.Write(",");
                    }
                    rec++;
                    MessageBox.Show("[RECORD]\t" + rec + " written to disk.");
                }
            }
        }

        private void SourceFrontUI_Load(object sender, EventArgs e)
        {
            // When Form Load
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // first name field
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // last name field
        }

        private void textBox1_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;

                textBox2.Select();
            }
        }

        private void textBox2_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                textBox3.Select();
            }
        }

        private void textBox3_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                textBox4.Select();
            }
        }

        private void textBox4_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                textBox7.Select();
            }
        }

        private void textBox5_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void textBox6_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void textBox7_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                Continue.Select();
            }
        }

        private void textBox8_Enter(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Entering 'textBox8_Enter' event method");

            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;

                // Get data from the fields
                string fname = textBox1.Text;
                string lname = textBox2.Text;
                string addr1 = textBox3.Text;
                string addr2 = textBox4.Text;
                string city = textBox5.Text;
                string state = textBox6.Text;
                int zip = int.Parse(textBox7.Text);

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("[ERROR]\tMUST KEY A LAST NAME !!!");
                    textBox2.Select();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("[ERROR]\tMUST KEY AN ADDRESS !!!");
                    textBox3.Select();
                    return;
                }

                if (!int.TryParse(textBox7.Text, out zip) && string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("[ERROR]\tMUST KEY A ZIP CODE !!!");
                    textBox7.Select();
                    return;
                }

                // Create a new Donor object
                DonorRecord donor = new DonorRecord
                {
                    FirstName = fname,
                    LastName = lname,
                    Address1 = addr1,
                    Address2 = addr2,
                    City = city,
                    State = state,
                    ZipCode = zip
                };

                try
                {
                    // Add the Donor object to the list
                    donors.Add(donor);
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = "Failed to add donor record.";
                    MessageBox.Show("Failure to add donor record" + ex.Message);
                }

                // Output data file path
                string outPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string outFile = Path.Combine(outPath, "Data Table Output Data File.csv");

                // Save the donor record to disk
                toolStripStatusLabel1.Text = "[UPDATE]\tWriting record to file...";
                WriteToCsv(donors, outFile);
            }
        }

        private void Continue_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            // Last Name Field
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            // Address 1 Field
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            // Zip Code Field
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // address 1 field
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // address 2 field
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // city field
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // state field
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            // zip code field
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // continue field
        }
    }
}
