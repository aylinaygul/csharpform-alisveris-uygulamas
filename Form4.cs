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
using System.Net;
using System.Net.Mail;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
        
    {

        public static string path = System.IO.Directory.GetCurrentDirectory();

         //public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");
        public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\aylna\\OneDrive\\Masaüstü\\WindowsFormsApp1\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");


        public Form4()
        {
            InitializeComponent();
        }
        static int kod;

        
        private void ekle()
        {
            
            baglan.Open();
            //veritabanına insert into ilegirilen bilgiler ekleniyor
            SqlCommand komut = new SqlCommand("Insert into urun(urunkod,urunad,fiyat,tip,kumaş,beden,renk,sayı,cinsiyet,[diger ozellik],resim,resim1) values('" + kod + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + comboBox1.Text + "','" + textBox8.Text + "','" + textBox17.Text + "','" + textBox18.Text + "')", baglan);
            komut.ExecuteNonQuery();
            MessageBox.Show("başarıyla kaydedildi!");
            baglan.Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            ekle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ad = kod.ToString() + "-2";
            openFileDialog1.ShowDialog();
            pictureBox2.ImageLocation = openFileDialog1.FileName;
            string[] xx = openFileDialog1.SafeFileName.Split('.');
            File.Copy(openFileDialog1.FileName.ToString(), path + "\\" + openFileDialog1.SafeFileName.ToString());
            FileInfo info = new FileInfo(path + "\\" + openFileDialog1.SafeFileName);
            //eklenen resimlerin sürekli kullanılabilmesi için bin\debug klasörüne kodları adları olacak şekilde kopyalanıyor

            string yeni = path + "\\" + ad+"."+xx[1];
            MessageBox.Show(yeni);
            info.MoveTo(yeni);
            textBox18.Text = yeni;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kod = kodyap();
            string ad = kod.ToString() + "-1";
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            string[] xx = openFileDialog1.SafeFileName.Split('.');
            if (openFileDialog1.FileName.ToString() != "")
            {
                File.Copy(openFileDialog1.FileName.ToString(), path + "\\" + openFileDialog1.SafeFileName.ToString());
                FileInfo info = new FileInfo(path + "\\" + openFileDialog1.SafeFileName);

                string yeni = path + "\\" + ad + "." + xx[1];
                MessageBox.Show(yeni);
                info.MoveTo(yeni);
                textBox17.Text = yeni;
            }
        }
        private void ürünEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void ürünDüzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            tabControl1.SelectTab(1);
            comboBox3.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *from urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku["urunkod"]);

            }
            baglan.Close();
        }
        private int kodyap()
        {
            //her ürün için rastgele kod yapılıp aynı koddan başkası olup olmadığı kontrol ediliyor
            baglan.Open();
            Random rand = new Random();
            int kod = rand.Next(111111, 999999);
            SqlCommand komut = new SqlCommand("Select *From urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                if (kod == Convert.ToInt32(oku["urunkod"]))
                {
                    kodyap();
                }

            }

            baglan.Close();
            return kod;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglan.Open();
            //combobox ta seçilen ürün değiştiğinde seçilen ürünün bilgileri yerlerine konuyor
            SqlCommand komut = new SqlCommand("Select *From urun where urunkod like'%" + comboBox3.SelectedItem.ToString() + "%'", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                textBox16.Text = oku["fiyat"].ToString();
                textBox15.Text = oku["tip"].ToString();
                textBox14.Text = oku["kumaş"].ToString();
                textBox13.Text = oku["beden"].ToString();
                textBox12.Text = oku["renk"].ToString();
                textBox11.Text = oku["sayı"].ToString();
                comboBox2.Text = oku["cinsiyet"].ToString();
                textBox10.Text = oku["diger ozellik"].ToString();
                textBox9.Text = oku["resim"].ToString();
                pictureBox4.ImageLocation = textBox9.Text;
                textBox19.Text = oku["resim1"].ToString();
                pictureBox3.ImageLocation = textBox19.Text;
            }
            baglan.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut2 = new SqlCommand("Update urun set fiyat ='" + textBox16.Text + "',tip ='" + textBox15.Text.ToString() + "',kumaş ='" + textBox14.Text.ToString() + "' where urunad ='" + comboBox3.SelectedItem.ToString() + "'", baglan);
            komut2.ExecuteNonQuery();
            baglan.Close();
        }

        private void aktifindirimlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            listView1.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Ürün Adı", 150);
            listView1.Columns.Add("İndirim Oranı", 150);
            listView1.Columns.Add("Başlangıç Tarihi", 150);
            listView1.Columns.Add("Bitiş Tarihi", 150);
         
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (oku["baslangıc"] != DBNull.Value && oku["bitis"] != DBNull.Value)
                {//indirimin var olup olmadığı kontrol edilip listview de görüntüleniyor
                    if (DateTime.Now > Convert.ToDateTime(oku["baslangıc"]) && DateTime.Now < Convert.ToDateTime(oku["bitis"]))
                    {
                        string[] bilgiler = { oku["urunad"].ToString(), oku["indirim"].ToString(), oku["baslangıc"].ToString(), oku["bitis"].ToString() };
                        listView1.Items.Add(new ListViewItem(bilgiler));

                    }
                }

                
            }
            baglan.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Update urun set indirim ='" + DBNull.Value + "',baslangıc ='" + DBNull.Value + "',bitis ='" + DBNull.Value + "' where urunad ='" + listView1.SelectedItems[0].SubItems[0].Text + "'", baglan);
            komut.ExecuteNonQuery();
            foreach (ListViewItem bilgi in listView1.SelectedItems)
            {
                bilgi.Remove();
            }
            baglan.Close();
        }

        private void kodogrenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                comboBox7.Items.Add(oku["urunad"]);


            }
            baglan.Close();
        }

        private void indirimduzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            comboBox4.Items.Clear();
            baglan.Open();

            SqlCommand komut = new SqlCommand("Select *From urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                comboBox4.Items.Add(oku["urunkod"]);

            }
            baglan.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Update urun set indirim =" + textBox20.Text + ",baslangıc ='" + textBox21.Text + "',bitis ='" + textBox22.Text + "' where urunkod ='" + comboBox4.SelectedItem.ToString() + "'", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
        }



        private void button5_Click_1(object sender, EventArgs e)
        {
            baglan.Open();
            bool kk = false;
            SqlCommand komut = new SqlCommand("Select *From urun where urunad like '%" + comboBox7.SelectedItem.ToString() + "%' ", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                if (textBox24.Text == oku["tip"].ToString() && textBox25.Text == oku["kumaş"].ToString() && textBox26.Text == oku["beden"].ToString() && textBox27.Text == oku["renk"].ToString() )
                {
                    MessageBox.Show(oku["urunkod"].ToString());
                }
                else 
                {
                    kk = true;
                }
            }
            baglan.Close();
            if (kk)
            {
                MessageBox.Show("böyle bir ürün bulunamadı");
            }
        }

        private void satışlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            tabControl1.SelectTab(5);
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *from urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox6.Items.Add(oku["urunkod"]);

            }
            baglan.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            foreach (var series in chart1.Series)
            {
                //grafiği önceki verilerden temizliyoruz
                series.Points.Clear();
            }

            label30.Text = "";
            int bir= 0;
            int iki=0;
            int uc = 0;
            int tplm=0;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From satıs where urunkod like '%"+comboBox6.SelectedItem.ToString()+"%'  ", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                    tplm++;
                    string xx = oku["tarih"].ToString();
                    string[] trh = (xx.Split('.'));
                    int ay = Convert.ToInt32(trh[1]);
                    if (ay == DateTime.Now.Month)
                    {
                    //şuan bulunduğumuz ve önceki 2 ayın satışlarını kontrol ediyoruz ve grafiğe aktarıyoruz
                        bir++;
                    
                    }
                    if (ay == (DateTime.Now.Month - 1))
                    {
                        iki++;
                    }
                    if (ay == (DateTime.Now.Month - 2))
                    {
                        uc++;
                    }
            }
            label30.Text ="Toplam yapılan satış:"+ tplm.ToString();
            chart1.Series["Series1"].Points.Add(uc);
            chart1.Series["Series1"].Points.Add(iki);
            chart1.Series["Series1"].Points.Add(bir);
            chart1.Series["Series1"].Points[0].AxisLabel = "2 ay önce";
            chart1.Series["Series1"].Points[1].AxisLabel = "1 ay önce";
            chart1.Series["Series1"].Points[2].AxisLabel = "bu ay";
            baglan.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            tabControl1.SelectTab(6);
            kod = kodyap();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            if (MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?", "Çıkış Yap", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                form1.Show();
            }
        }

       
    }
    }

