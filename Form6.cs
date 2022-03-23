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
    public partial class Form6 : Form
    {
        
       // public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");
        public static SqlConnection baglan = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=C:\\Users\\aylna\\OneDrive\\Masaüstü\\WindowsFormsApp1\\Database1.mdf;Integrated Security = True; Connect Timeout = 30");


        public Form6()
        {
            InitializeComponent();
        }
        double toplam = 0;
        private void Form6_Load(object sender, EventArgs e)
        {
            baglan.Open();
            Form3 form3 = new Form3();
            listView1.View = View.Details;
            listView1.Columns.Add("Ürün kodu", 150);
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Ürün adı", 150);
            listView1.Columns.Add("Ürün rengi", 150);
            listView1.Columns.Add("Ürün bedeni", 150);
            listView1.Columns.Add("Fiyat", 150);
            listView1.Columns.Add("İndirim", 150);
            toplam=0;
                SqlCommand komut = new SqlCommand("Select *From urun", baglan);
                SqlDataReader oku = komut.ExecuteReader();
            
                while (oku.Read())
                {
                for (int j = 0; j < form3.spt.Length; j++)
                {
                    if (oku["urunkod"].ToString() == form3.spt[j])
                    {
                        toplam += Convert.ToDouble( oku["fiyat"]);
                        string[] bilgiler = { oku["urunkod"].ToString(),oku["urunad"].ToString(), oku["renk"].ToString(), oku["beden"].ToString(), oku["fiyat"].ToString() };
                        listView1.Items.Add(new ListViewItem(bilgiler));
                    }
                }
                }
            baglan.Close();
            label1.Text ="Ödenecek Tutar:  " +toplam.ToString();
        }

        Form3 form3 = new Form3();
        private void button1_Click(object sender, EventArgs e)
        {
            
            int[] x = new int[form3.spt.Length];
            Form1 form1 = new Form1();
            baglan.Open();
            SqlCommand komut2 = new SqlCommand("Select *From urun", baglan);
            SqlDataReader oku = komut2.ExecuteReader();
            int[] sayi = new int[form3.spt.Length]; ;
            while (oku.Read())
            {
                for (int j = 0; j < form3.spt.Length; j++)
                {
                    if (oku["urunkod"].ToString() == form3.spt[j])
                    {
                        x[j] = j;
                        if(oku["sayı"]!=DBNull.Value  && Convert.ToInt32(oku["sayı"]) != 0)
                        {
                            sayi[j] = Convert.ToInt32(oku["sayı"]);
                        }
                        
                        else
                        {
                            MessageBox.Show(oku["urunad"].ToString() + " adlı üründe stok yok!");
                        }
                    }
                }
            }
            MessageBox.Show("satış başarılı!");
            baglan.Close();
            for (int j = 0; j < form3.spt.Length; j++)
            {
                ekle(form3, form1, x[j]);
                int xxx = sayi[j];
                eksi(sayi, j, x[j],form3);
                
                
            }
        }
        private void ekle(Form3 form3, Form1 form1,int j)
        {
            //form3 ten sepete eklenenler ve form1 en giriş yapanın id si alınarak satış kayıtları veritabanında tutuluyor
            baglan.Open();
            SqlCommand komut = new SqlCommand("Insert into satıs(id,urunkod,tarih,fiyat) values('" + form1.idd + "','" + form3.spt[j] + "','" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "','" + form3.fyt[j] + "')", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            
        }
        private void eksi(int[] sayi,int j,int x,Form3 form3)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Update urun set sayı = '"+(sayi[j]-1)+"' where urunkod ='"+ form3.spt[x] +"'", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listView1.SelectedItems[0].SubItems[0].Text);
            for (int j = 0; j < form3.spt.Length; j++)
            {
                if (listView1.SelectedItems[0].SubItems[0].Text == form3.spt[j])
                {
                    toplam -= form3.fyt[j];
                    form3.spt[j] = "";
                }
            }
                    foreach (ListViewItem bilgi in listView1.SelectedItems)
            {
                bilgi.Remove();
            }
            
            label1.Text = "Ödenecek Tutar:  " + toplam.ToString();

        }
    }
}
