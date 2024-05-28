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
    public partial class addNewUser : Form
    {
        public addNewUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""){
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }
            if (textBox2.Text == ""){
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }
            if (textBox3.Text == ""){
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }
            if (textBox4.Text.Length != 12){
                MessageBox.Show("Lütfen geçerli bir telefon numarası giriniz.");
                return;
            }
            if (textBox5.Text == ""){
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }
            if (!textBox6.Text.Contains("@")){
                MessageBox.Show("Lütfen geçerli bir mail adresi giriniz.");
                return;
            }

            if (textBox7.Text == "") {
                MessageBox.Show("Lütfen geçerli bir mail adresi giriniz.");
                return;
            }
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "INSERT INTO rehber (name, surname, workplace, phone_number, mail, user_note, adress, birthday)";
            komut.CommandText += "VALUES (@name, @surname, @workplace, @phone_number, @mail, @user_note, @adress, @birthday)";
            komut.Parameters.AddWithValue("@name", textBox1.Text);
            komut.Parameters.AddWithValue("@surname", textBox2.Text);
            komut.Parameters.AddWithValue("@workplace", textBox3.Text);
            komut.Parameters.AddWithValue("@phone_number", textBox4.Text);
            komut.Parameters.AddWithValue("@mail", textBox6.Text);
            komut.Parameters.AddWithValue("@user_note", textBox5.Text);
            komut.Parameters.AddWithValue("@adress", textBox7.Text);
            komut.Parameters.AddWithValue("@birthday", dateTimePicker1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi eklendi.");
            var index = new index();
            index.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            var index = new index();
            index.Show();
            this.Hide();
        }

        private void addNewUser_Load(object sender, EventArgs e)
        {

        }
    }
}
