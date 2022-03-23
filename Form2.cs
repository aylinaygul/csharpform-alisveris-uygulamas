using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        // SqlConnection baglan = Form1.baglan;
        public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\aylna\\OneDrive\\Masaüstü\\WindowsFormsApp1\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");

        public Form2()
        {
           
            InitializeComponent();
        }
        char? pc = null;
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "kullanıcı adı")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "kullanıcı adı";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "şifre")
            {
                textBox2.Text = "";
            }
            textBox2.PasswordChar = '*';
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.PasswordChar = Convert.ToChar(pc);
                textBox2.Text = "şifre";
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "tekrar şifre")
            {
                textBox3.Text = "";
            }
            textBox3.PasswordChar = '*';
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.PasswordChar = Convert.ToChar(pc);
                textBox3.Text = "tekrar şifre";
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "e mail")
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "e mail";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        static Random random = new Random();
        int rand = random.Next(1257, 9999);
        private void button1_Click(object sender, EventArgs e)
        {
           
                if (textBox2.Text == textBox3.Text)
                {
                    mailsend(textBox4.Text, rand);
                    MessageBox.Show("mail adresinize doğrulama kodu gönderildi!");
                    textBox6.Visible = true;
                    label1.Visible = true;
                    button3.Visible = true;

                }
                else
                {
                    MessageBox.Show("Şifreler eşleşmiyor");
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            
           
            baglan.Close();
        }
        
        
        public void mailsend(string xx,int rand)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.live.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("aylinaygul@hotmail.com", "aylinn1234");
            MailMessage message = new MailMessage();
            message.From = new MailAddress("aylinaygul@hotmail.com", "shopshopshop");
            message.To.Add(xx);
            message.Subject = "Doğrulama Kodu";
            message.Body = "Doğrulama Kodunuz:"+rand;
            sc.Send(message);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text== Convert.ToString(rand))
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("Insert into giris(id,sifre,email) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "')", baglan);
                komut.ExecuteNonQuery();
                MessageBox.Show("kayıt başarılı!");
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();
            }
            else
            {
                MessageBox.Show("girdiğiniz kod yanlış");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
