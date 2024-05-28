using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Data.OleDb;

namespace Rehber
{
    public partial class shareUserData : Form
    {
        public shareUserData(string mongoID)
        {
            InitializeComponent();
            if (mongoID == null){
                MessageBox.Show("ID boş geçilemez!");
                this.Close();
            }
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM rehber", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (oku["ID"].ToString() == mongoID)
                {
                textBox1.Text = oku["name"].ToString() + " " + oku["surname"].ToString() + " Adlı Kişinin Bilgilerini Paylaşmak İçin Lütfen Aşağıdaki Formu Doldurunuz.";
                gizliMongoUserID.Text = mongoID;
                this.Text = oku["name"].ToString() + " " + oku["surname"].ToString() + " - Paylaş";
                }
            }
        }

        private void editUserData_Load(object sender, EventArgs e)
        {
            
        }

        private void label18_Click(object sender, EventArgs e)
        {
            var index = new index();
            index.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=rehber.accdb");
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("SELECT * FROM rehber", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (oku["ID"].ToString() == gizliMongoUserID.Text.ToString()){
                    string mail = textBox5.Text;
                    string subject = "Rehber - " + oku["name"].ToString() + " " + oku["surname"].ToString() + " kişisini aramak istediğiniz için size bir mesaj gönderdim.";
                    string body = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"table-layout:fixed;background-color:#f9f9f9\" id=\"bodyTable\"><tbody>	<tr>		<td style=\"padding-right:10px;padding-left:10px;\" align=\"center\" valign=\"top\" id=\"bodyCell\">			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"wrapperWebview\" style=\"max-width:600px\"><tbody><tr><td align=\"center\" valign=\"top\">	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">		<tbody></tbody>	</table></td></tr></tbody>			</table>			<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"wrapperBody\" style=\"max-width:600px\"><tbody><tr><td align=\"center\" valign=\"top\">	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"tableCard\" style=\"background-color:#fff;border-color:#e5e5e5;border-style:solid;border-width:0 1px 1px 1px;\">		<tbody>			<tr><td style=\"background-color:#00d2f4;font-size:1px;line-height:3px\" class=\"topBorder\" height=\"3\">&nbsp;</td>			</tr>			<tr><td style=\"padding-bottom: 20px;\" align=\"center\" valign=\"top\" class=\"imgHero\"><a href=\"#\" style=\"text-decoration:none\" target=\"_blank\"><img alt=\"\" border=\"0\" src=\"http://email.aumfusion.com/vespro/img/hero-img/blue/heroGradient/user-account.png\" style=\"width:100%;max-width:600px;height:auto;display:block;color: #f9f9f9;\" width=\"600\"></a></td>			</tr>			<tr><td style=\"padding-bottom: 5px; padding-left: 20px; padding-right: 20px;\" align=\"center\" valign=\"top\" class=\"mainTitle\"><h2 class=\"text\" style=\"color:#000;font-family:Poppins,Helvetica,Arial,sans-serif;font-size:28px;font-weight:500;font-style:normal;letter-spacing:normal;line-height:36px;text-transform:none;text-align:center;padding:0;margin:0\">Merhaba</h2></td>			</tr>			<tr><td style=\"padding-left:20px;padding-right:20px\" align=\"center\" valign=\"top\" class=\"containtTable ui-sortable\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"tableDescription\" style=\"\"><tbody>	<tr>		<td style=\"padding-bottom: 20px;\" align=\"center\" valign=\"top\" class=\"description\">			<p class=\"text\" style=\"color:#666;font-family:'Open Sans',Helvetica,Arial,sans-serif;font-size:14px;font-weight:400;font-style:normal;letter-spacing:normal;line-height:22px;text-transform:none;text-align:center;padding:0;margin:0\">";
                    body += oku["name"].ToString();
                    body += " " + oku["surname"].ToString();
                    body += " Adlı Kişinin Bilgilerini İletmek İçin Buradayım! <br> Adı: ";
                    body += oku["name"].ToString();
                    body += "<br> SoyAdı: " + oku["surname"].ToString() + "<br> Telefon Numarası: <a href=\"tel:+\"" + oku["phone_number"].ToString() + "\">" + oku["phone_number"].ToString() + "</a>";
                    body += "</p>		</td>	</tr></tbody></table></td>			</tr>			<tr><td style=\"font-size:1px;line-height:1px\" height=\"20\">&nbsp;</td>			</tr></tbody>	</table>	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" class=\"space\">		<tbody>			<tr><td style=\"font-size:1px;line-height:1px\" height=\"30\">&nbsp;</td>			</tr>		</tbody>	</table></td></tr></tbody>			</table>		</td>	</tr></tbody></table>";
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(textBox5.Text);
                    mailMessage.To.Add(mail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new System.Net.NetworkCredential("xxxxxx@gmail.com", "xxxx");
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                    MessageBox.Show("Mesajınız gönderildi.");
                }
            }
        }
    }
}
