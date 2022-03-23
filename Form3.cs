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
using System.Threading;


namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {

        //public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");
        public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\aylna\\OneDrive\\Masaüstü\\WindowsFormsApp1\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");
        public static SqlConnection baglan2 = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\aylna\\OneDrive\\Masaüstü\\WindowsFormsApp1\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");
        //datareader ların karışmaması için 2 bağlantı kullandım.

        public Form3()
        {
            InitializeComponent();
        }

        private void anasayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *from urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                form51.img1 = oku["resim"].ToString();
            }

            baglan.Close();
        }

        public static string[] resimler = new string[9];

        int knt = 0;
        int kntrl = 0;
        private void ekle(string c)
        {
            Form5[] form5 = { form51, form52, form53, form54, form55, form56, form57, form58, form59 };
            goster(form5);
            kntrl++;
            int i = 0;
            if (kntrl % 2 == 1)
            {
                knt = 0;
            }

           
            temizle(form5);
            
            if (baglan2.State.ToString() == "Closed")
            {
                baglan2.Open();
            }
            SqlCommand komut = new SqlCommand("Select *From urun ", baglan2);
            SqlDataReader oku = komut.ExecuteReader();
   
            
            while (oku.Read())
            {
                if (oku["cinsiyet"].ToString() == c)
                {
                    i++;
                    
                    while (i == 10 )//gösterilecek ürünler 9 dan fazla olduğunda
                                    //9 dan sonrasını görüntülemek için butona basılması bekleniyor butona basıldığında 
                                    //usercontroller temizlenip i tekrar 1 e eşitlenerek 10 dan sonrası görüntüleniyor.
                    {
                        
                        Application.DoEvents();//bunun ile button1 a basılana kadar programın burada beklemesini sağlıyorum

                        if (button1.Enabled == false)
                        {
                            
                            knt = 0;
                           
                            temizle(form5);
                            i = 1;
                        }
                        if (knt==1 )
                        {
                            button1.Enabled = true;
                            oku.Close();
                            baglan2.Close();
                            
                        }

                    }

                    switch (i)
                    {
                        case 1:
                            
                            form51.img1 = oku["resim"].ToString();
                            form51.label = oku["urunad"].ToString();
                            form51.labelx = oku["urunkod"].ToString();
                            form51.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 2:
                            form52.img1 = oku["resim"].ToString();
                            form52.label = oku["urunad"].ToString();
                            form52.labelx = oku["urunkod"].ToString();
                            form52.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 3:
                            form53.img1 = oku["resim"].ToString();
                            form53.label = oku["urunad"].ToString();
                            form53.labelx = oku["urunkod"].ToString();
                            form53.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 4:
                            form54.img1 = oku["resim"].ToString();
                            form54.label = oku["urunad"].ToString();
                            form54.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 5:
                            form55.img1 = oku["resim"].ToString();
                            form55.label = oku["urunad"].ToString();
                            form55.labelx = oku["urunkod"].ToString();
                            form55.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 6:
                            form56.img1 = oku["resim"].ToString();
                            form56.label = oku["urunad"].ToString();
                            form56.labelx = oku["urunkod"].ToString();
                            form56.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 7:
                            form57.img1 = oku["resim"].ToString();
                            form57.label = oku["urunad"].ToString();
                            form57.labelx = oku["urunkod"].ToString();
                            form57.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 8:
                            form58.img1 = oku["resim"].ToString();
                            form58.label = oku["urunad"].ToString();
                            form58.labelx = oku["urunkod"].ToString();
                            form58.price = oku["fiyat"].ToString() + "$";
                            break;
                        case 9:
                            form59.img1 = oku["resim"].ToString();
                            form59.label = oku["urunad"].ToString();
                            form59.labelx = oku["urunkod"].ToString();
                            form59.price = oku["fiyat"].ToString() + "$";
                            button1.Enabled = true;
                            break;
                    }
                    

                }

            }
            knt = 0;
            gizle(form5);
            oku.Close();
            baglan2.Close();

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            
            tabControl1.SelectedTab = tabPage3;
            filtre();
        }
        
        private void temizle(Form5[] form5)
        {
            for (int i = 0; i < 9; i++)
            {
                form5[i].img1 = "";
                form5[i].price = "";
                form5[i].label = "";
            }
        }
        private void gizle(Form5[] form5)
        {
            int x = 0;
            for (int i = 0; i < 9; i++)
            {
                if (form5[i].img1 == "" && form5[i].label == "")
                {
                    x++;
                    form5[i].Visible = false;
                }
            }
            if (x == 9)
            {
                if (baglan.State.ToString() == "Opened")
                {
                    baglan.Close();
                }
                label9.Visible = true;
                label9.Text = "Ürün bulunmamaktadır.";
            }
        }
        private void goster(Form5[] form5)
        {
            for (int i = 0; i < 9; i++)
            {
                    form5[i].Visible = true;
                }
            }
        private void kadınToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label9.Visible = false;

            tabControl1.SelectTab(0);
            string c = "kadin";
            cs = c;
            ekle(c);
            knt = 0;
        }
        private void erkekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            knt = 1;
            tabControl1.SelectTab(0);
            string c = "erkek";
            cs = c;
            ekle(c);
            knt = 0;
        }

        private void çocukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            knt = 1;
            tabControl1.SelectTab(0);
            string c = "çocuk";
            cs = c;
            ekle(c);
        }

        private void bebekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            knt = 1;
            tabControl1.SelectTab(0);
            string c = "bebek";
            ekle(c);
        }



        private void iNDİRİMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label9.Visible = false;
            Form5[] form5 = { form51, form52, form53, form54, form55, form56, form57, form58, form59 };
            goster(form5);
            knt = 1;
            tabControl1.SelectTab(0);
            cs = "ndr";
            temizle(form5);
            int i = 0;

            if (baglan.State.ToString() == "Closed")
            {
                baglan.Open();
            }
            SqlCommand komut = new SqlCommand("Select *From urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                if (oku["baslangıc"] != DBNull.Value || oku["bitis"] != DBNull.Value)
                {
                    if (DateTime.Now > Convert.ToDateTime(oku["baslangıc"]) && DateTime.Now < Convert.ToDateTime(oku["bitis"]))
                    {
                        i++;
                        while (i == 10)
                        {
                            Application.DoEvents();
                            if (button1.Enabled == false)
                            {
                                temizle(form5);
                                i = 1;
                                button1.Enabled = true;

                            }
                        }

                        switch (i)
                        {
                            case 1:
                                form51.img1 = oku["resim"].ToString();
                                form51.label = oku["urunad"].ToString();
                                form51.labelx = oku["urunkod"].ToString();
                                form51.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 2:
                                form52.img1 = oku["resim"].ToString();
                                form52.label = oku["urunad"].ToString();
                                form52.labelx = oku["urunkod"].ToString();
                                form52.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 3:
                                form53.img1 = oku["resim"].ToString();
                                form53.label = oku["urunad"].ToString();
                                form53.labelx = oku["urunkod"].ToString();
                                form53.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 4:
                                form54.img1 = oku["resim"].ToString();
                                form54.label = oku["urunad"].ToString();
                                form54.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 5:
                                form55.img1 = oku["resim"].ToString();
                                form55.label = oku["urunad"].ToString();
                                form55.labelx = oku["urunkod"].ToString();
                                form55.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 6:
                                form56.img1 = oku["resim"].ToString();
                                form56.label = oku["urunad"].ToString();
                                form56.labelx = oku["urunkod"].ToString();
                                form56.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 7:
                                form57.img1 = oku["resim"].ToString();
                                form57.label = oku["urunad"].ToString();
                                form57.labelx = oku["urunkod"].ToString();
                                form57.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 8:
                                form58.img1 = oku["resim"].ToString();
                                form58.label = oku["urunad"].ToString();
                                form58.labelx = oku["urunkod"].ToString();
                                form58.price = oku["fiyat"].ToString() + "$";
                                break;
                            case 9:
                                form59.img1 = oku["resim"].ToString();
                                form59.label = oku["urunad"].ToString();
                                form59.labelx = oku["urunkod"].ToString();
                                form59.price = oku["fiyat"].ToString() + "$";
                                break;
                        }


                    }


                }
            }
            gizle(form5);
            baglan.Close();

        }
        private void tıkla(Form5 xx)
        {
            pictureBox1.ImageLocation = xx.img1;
            label1.Text = xx.label;
            label2.Text = xx.price;
            label7.Text = xx.labelx;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From urun where urunkod like '%" + label7.Text + "%'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                label4.Text = "Kumaş Çeşidi:  " + oku["kumaş"].ToString();
                label3.Text = "Ürün Rengi:  " + oku["renk"].ToString();
                label5.Text = oku["diger ozellik"].ToString();

            }
            baglan.Close();
        }
        private void form51_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form51);
        }

        private void form52_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form52);
        }

        private void form53_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form53);
        }

        private void form55_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form55);
        }

        private void form54_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form54);
        }

        private void form56_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form56);
        }

        private void form57_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form57);
        }

        private void form58_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form58);
        }

        private void form59_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            tıkla(form59);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            Form1 form1 = new Form1();
            SqlCommand komut = new SqlCommand("Insert into yorum(id,urunkod,yorum) values ('" + form1.idd + "','" + label7.Text + "','" + textBox1.Text + "')", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            listView1.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Kullanıcı Adı", 150);
            listView1.Columns.Add("Yorum", 450);
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From yorum", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                string[] bilgiler = { oku["id"].ToString(), oku["yorum"].ToString() };
                listView1.Items.Add(new ListViewItem(bilgiler));
            }
            baglan.Close();
        }
        private static string[] sepet = new string[0];
        private static int[] fiyat = new int[0];
        private void button3_Click(object sender, EventArgs e)
        {
            Array.Resize(ref sepet, sepet.Length + 1);
            Array.Resize(ref fiyat, sepet.Length + 1);
            sepet[sepet.Length - 1] = label7.Text;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From urun where urunkod like '%" + label7.Text + "%'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                fiyat[sepet.Length - 1] = Convert.ToInt32(oku["fiyat"]);
            }
            baglan.Close();

        }

        public string[] spt
        {
            get
            {
                return sepet;
            }

            set
            {
                sepet = value;
            }
        }
        public int[] fyt
        {
            get
            {
                return fiyat;
            }

            set
            {
                fiyat = value;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Show();
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            ekle("kadin");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            knt = 1;
            tabControl1.SelectTab(0);
           ekle("erkek");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            knt = 1;
            tabControl1.SelectTab(0);
            ekle("çocuk");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            knt = 1;
            tabControl1.SelectTab(0);
           ekle("bebek");
        }
        
        private void filtre()
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From urun", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["renk"]);
                comboBox2.Items.Add(oku["kumaş"]);

            }
            baglan.Close();

        }
        string cs;
        private void button7_Click(object sender, EventArgs e)
        {
            Form5[] form5 = { form51, form52, form53, form54, form55, form56, form57, form58, form59 };
            goster(form5);
            temizle(form5);
            int i = 0;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *From urun ", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                if (oku["cinsiyet"].ToString() == cs || cs == "ndr")//indirime tıklandığında filtreler değiştirilip aynı işlemler tekrarlanıyor
                {
                    if ((oku["renk"].ToString() == comboBox1.SelectedItem.ToString() || comboBox1.Text.ToString() == "hepsi") && (oku["kumaş"].ToString() == comboBox2.SelectedItem.ToString() || comboBox2.Text.ToString() == "hepsi"))
                    {
                        if (Convert.ToInt32(oku["fiyat"]) >= Convert.ToInt32(comboBox3.SelectedItem) && Convert.ToInt32(oku["fiyat"]) <= Convert.ToInt32(comboBox4.SelectedItem))
                        {
                            i++;
                            while (i == 10)
                            {
                                Application.DoEvents();

                                if (button1.Enabled == false)
                                {
                                    temizle(form5);
                                    i = 1;
                                }
                            }

                            switch (i)
                            {
                                case 1:
                                    form51.img1 = oku["resim"].ToString();
                                    form51.label = oku["urunad"].ToString();
                                    form51.labelx = oku["urunkod"].ToString();
                                    form51.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 2:
                                    form52.img1 = oku["resim"].ToString();
                                    form52.label = oku["urunad"].ToString();
                                    form52.labelx = oku["urunkod"].ToString();
                                    form52.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 3:
                                    form53.img1 = oku["resim"].ToString();
                                    form53.label = oku["urunad"].ToString();
                                    form53.labelx = oku["urunkod"].ToString();
                                    form53.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 4:
                                    form54.img1 = oku["resim"].ToString();
                                    form54.label = oku["urunad"].ToString();
                                    form54.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 5:
                                    form55.img1 = oku["resim"].ToString();
                                    form55.label = oku["urunad"].ToString();
                                    form55.labelx = oku["urunkod"].ToString();
                                    form55.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 6:
                                    form56.img1 = oku["resim"].ToString();
                                    form56.label = oku["urunad"].ToString();
                                    form56.labelx = oku["urunkod"].ToString();
                                    form56.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 7:
                                    form57.img1 = oku["resim"].ToString();
                                    form57.label = oku["urunad"].ToString();
                                    form57.labelx = oku["urunkod"].ToString();
                                    form57.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 8:
                                    form58.img1 = oku["resim"].ToString();
                                    form58.label = oku["urunad"].ToString();
                                    form58.labelx = oku["urunkod"].ToString();
                                    form58.price = oku["fiyat"].ToString() + "$";
                                    break;
                                case 9:
                                    form59.img1 = oku["resim"].ToString();
                                    form59.label = oku["urunad"].ToString();
                                    form59.labelx = oku["urunkod"].ToString();
                                    form59.price = oku["fiyat"].ToString() + "$";
                                    button1.Enabled = true;
                                    break;
                            }
                        }
                    }
                }
            }
            gizle(form5);
            baglan.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
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

