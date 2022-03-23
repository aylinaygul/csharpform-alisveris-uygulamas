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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\aylna\\OneDrive\\Masaüstü\\WindowsFormsApp1\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");
        //veri tabanı bağlantısı kuruluyor.
        //public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");
        public Form1()
        {
            InitializeComponent();
        }
        char? pc = null;
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
        bool kontrol;
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       private static string id;
        private void button1_Click(object sender, EventArgs e)
        {
            id = textBox1.Text;
            string sifre = textBox2.Text;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From giris", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {//giris tablosu okunurken girilen şifrenin ve kullanıcı adının kayıtlı olup olmadığı sorgulanıyor
                if (id == oku["id"].ToString() && sifre == oku["sifre"].ToString())
                {
                    kontrol = true;
                    break;
                }
                else
                {
                    kontrol = false;
                }
            }
            baglan.Close();
            if (kontrol==true)
            {
                this.Hide();
                if (id == "admin")// eğer giriş yapan adminse yönetici sayfası
                {
                    Form4 f4 = new Form4();
                    f4.Show();
                }
                else//değilse müşteri sayfası açılıyor
                {
                    Form3 f3 = new Form3();
                    f3.Show();
                }
                
            }
            else
            {
                MessageBox.Show("giriş başarısız");
            }
        }
        public string idd {//id yi diğer formlarda satış kayıt işlemlerinde kullanmak için get set ediyoruz
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox3.Visible = true;
            button3.Visible = true;
            label1.Visible = true;
            
        }
        
        string mail;
        static Random random = new Random();
        int rand = random.Next(1257, 9999);
        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From giris", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            mail = textBox3.Text;
            bool xx=true;
           while (oku.Read())
            {
               if (mail == oku["email"].ToString())
                {
                    xx = false;
                    Form2 form2 = new Form2();
                    form2.mailsend(mail, rand);
                    MessageBox.Show("mail adresinize doğrulama kodu gönderildi!");
                    textBox3.Clear();
                    label1.Text = "Doğrulama kodunu giriniz.";
                    button3.Visible = false;
                    button4.Visible = true;
                }
           }
            if (xx) {
                MessageBox.Show("böyle bir mail adresi kayıtlı değil!");
            }
            baglan.Close();

        }
        string sfr = "0";
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == rand.ToString())
            {
                textBox3.Clear();
                label1.Text = "Yeni şifrenizi giriniz.";
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = true;
                textBox3.PasswordChar = '*';
                baglan.Open();
                SqlCommand komut = new SqlCommand("Select *From giris where email like '%" + mail + "%'", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                
                while (oku.Read())
                {
                    sfr = oku["sifre"].ToString();
                }

                baglan.Close();
            }
            else
            {
                MessageBox.Show("doğrulama kodu hatalı");
            }
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut2 = new SqlCommand("Update giris set sifre ='" + textBox3.Text.ToString() + "' where sifre ='" + sfr + "'", baglan);
            komut2.ExecuteNonQuery();
            MessageBox.Show("şifreniz başarıyla değiştirildi.Şimdi giriş yapabilirsiniz.");
            baglan.Close();
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
       

       

        
    }

