using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Rehber
{
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
        }

        private void index_Load(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM rehber", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            if (oku.HasRows == false)
            {
                listBox1.Items.Add("Rehberde hiç kayıt yok.");
            }
            OleDbCommand komut2 = new OleDbCommand("SELECT Count(phone_number) FROM rehber", baglanti);
            textBox1.Text = komut2.ExecuteScalar().ToString()  + " kişi arasında ara";
            while (oku.Read())
            {
                listBox1.Items.Add(oku["name"].ToString() + " " + oku["surname"].ToString() + " - " + oku["phone_number"].ToString());
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM rehber", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            if (listBox1.SelectedItem == null) return;
            if (listBox1.SelectedItem.ToString() == "Rehberde hiç kayıt yok.") return;
            string selectedItem = listBox1.SelectedItem.ToString();
            string[] selectedItemArray = selectedItem.Split('-');
            string phone_number = selectedItemArray[1].Trim();
            string[] selectedItemArray2 = selectedItem.Split(' ');
            string name = selectedItemArray2[0].Trim();
            string surname = selectedItemArray2[1].Trim();
            string id = "";
            while (oku.Read())
            {
                if (oku["phone_number"].ToString().ToLower() == phone_number.ToLower()) //Not: Numarayı Kayıt eder iken boşluk bıraktı ise çalışmaya bilir.
                {
                    id = oku["ID"].ToString();
                }
            }
            var detaylar = new detaylar(id);
            detaylar.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM rehber", baglanti);
            OleDbCommand komut2 = new OleDbCommand("SELECT Count(phone_number) FROM rehber", baglanti);
            var kontrol = komut2.ExecuteScalar().ToString() + " kişi arasında ara";
            OleDbDataReader oku = komut.ExecuteReader();
            if (textBox1.Text == kontrol) return;
            if (textBox1.Text == "") return;
            listBox1.Items.Clear();
            while (oku.Read())
            {
                if (oku["name"].ToString().ToLower().Contains(textBox1.Text.ToLower()) || oku["surname"].ToString().ToLower().Contains(textBox1.Text.ToLower()) || oku["phone_number"].ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    textBox1.ForeColor = Color.Black;
                    listBox1.Items.Add(oku["name"].ToString() + " " + oku["surname"].ToString() + " - " + oku["phone_number"].ToString());
                }
                if (listBox1.Items.Count < 0)
                {
                    listBox1.Items.Add("Sonuç bulunamadı");
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            var addNewUser = new addNewUser();
            addNewUser.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM rehber", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            int lenght = oku.RecordsAffected;
            if(lenght < 0)
            {
                MessageBox.Show("Rehberde hiç kayıt yok.");
                return;
            }
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Cells[1, "A"] = "Ad";
            worksheet.Cells[1, "B"] = "Soyad";
            worksheet.Cells[1, "C"] = "Telefon";
            worksheet.Cells[1, "D"] = "E-mail";
            worksheet.Cells[1, "E"] = "Adres";
            worksheet.Cells[1, "F"] = "İş Yeri";
            worksheet.Cells[1, "G"] = "Notlar";
            worksheet.Cells[1, "H"] = "Doğum Tarihi";
            int row = 2;
            while (oku.Read())
            {
                worksheet.Cells[row, "A"] = oku["name"].ToString();
                worksheet.Cells[row, "B"] = oku["surname"].ToString();
                var phone_number = oku["phone_number"];
                string phone_number_format = phone_number.ToString();
                if (phone_number_format.Length == 10)
                {
                    phone_number = phone_number_format.Insert(3, " ");
                    phone_number = phone_number_format.Insert(7, " ");
                    worksheet.Cells[row, "C"] = phone_number;
                }
                else if (phone_number_format.Length == 11)
                {
                    phone_number = phone_number_format.Insert(3, " ");
                    phone_number = phone_number_format.Insert(8, " ");
                    worksheet.Cells[row, "C"] = phone_number;
                }
                else if (phone_number_format.Length == 12)
                {
                    phone_number = phone_number_format.Insert(3, " ");
                    phone_number = phone_number_format.Insert(9, " ");
                    worksheet.Cells[row, "C"] = phone_number;
                }
                worksheet.Cells[row, "D"] = oku["mail"].ToString();
                worksheet.Cells[row, "E"] = oku["adress"].ToString();
                worksheet.Cells[row, "F"] = oku["workplace"].ToString();
                worksheet.Cells[row, "G"] = oku["user_note"].ToString();
                worksheet.Cells[row, "H"] = oku["birthday"].ToString();
                row++;
            }
            worksheet.Columns.AutoFit();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files|*.xlsx";
            saveFileDialog1.Title = "Save an Excel File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            excel.Quit();
            MessageBox.Show("Başarıyla Dışa Aktarıldı");
        }
    }
}
