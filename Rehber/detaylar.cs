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
    public partial class detaylar : Form
    {
        public detaylar(string mongoID)
        {
            InitializeComponent();
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM rehber", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (oku["ID"].ToString() == mongoID)
                {
                    this.Text = oku["name"].ToString() + " " + oku["surname"].ToString() + " - Detaylar";
                    label1.Text = oku["name"].ToString();
                    label2.Text = oku["surname"].ToString();
                    label3.Text = oku["workplace"].ToString();
                    label4.Text = oku["adress"].ToString();
                    label13.Text = oku["phone_number"].ToString();
                    label14.Text = oku["mail"].ToString();
                    label15.Text = oku["birthday"].ToString();
                    textBox1.Text = oku["user_note"].ToString();
                    textBox2.Text = oku["name"].ToString();
                    textBox3.Text = oku["surname"].ToString();
                    textBox4.Text = oku["workplace"].ToString();
                    textBox5.Text = oku["adress"].ToString();
                    textBox6.Text = oku["phone_number"].ToString();
                    textBox7.Text = oku["mail"].ToString();
                    dateTimePicker1.Text = oku["birthday"].ToString();

                    gizliMongoUserID.Text = oku["ID"].ToString();
                }
            }

        }

        private void label17_Click(object sender, EventArgs e)
        {
            var index = new index();
            index.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox1.ForeColor = Color.Black;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
            dateTimePicker1.Visible = true;

            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;

            button1.Visible = false;
            button2.Visible = true;

            label18.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            if(textBox2.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            if(textBox3.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            if(textBox4.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            if(textBox5.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            if(textBox6.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            if(textBox7.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            if(dateTimePicker1.Text == ""){
                MessageBox.Show("Lütfen boş alanları doldurunuz.");
                return;
            }
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            OleDbCommand komut = new OleDbCommand();  
            komut.Connection = baglanti;        
            komut.CommandText = "update rehber set name=@namee, surname=@surnamee, mail=@maile, adress=@adresss, birthday=@birthdayy, workplace=@workplacee, phone_number=@telefon, user_note=@not where ID=@IDD";
            komut.Parameters.AddWithValue("@namee", textBox2.Text);
            komut.Parameters.AddWithValue("@surnamee", textBox3.Text);
            komut.Parameters.AddWithValue("@maile", textBox7.Text);
            komut.Parameters.AddWithValue("@adresss", textBox5.Text);
            komut.Parameters.AddWithValue("@birthdayy", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@workplacee", textBox4.Text);
            komut.Parameters.AddWithValue("@telefon", textBox6.Text);
            komut.Parameters.AddWithValue("@not", textBox1.Text);
            komut.Parameters.AddWithValue("@IDd", gizliMongoUserID.Text);
            baglanti.Open();      
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme Başarılı");
            var detaylar = new detaylar(gizliMongoUserID.Text);
            detaylar.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            var index = new index();
            index.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            var mongoID = gizliMongoUserID.Text;
            var shareUserData = new shareUserData(mongoID);
            shareUserData.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = "delete from rehber where ID=@IDD";
            komut.Parameters.AddWithValue("@IDD", gizliMongoUserID.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme Başarılı");
            var index = new index();
            index.Show();
            this.Hide();
        }

        private void detaylar_Load(object sender, EventArgs e)
        {

        }
    }
}
