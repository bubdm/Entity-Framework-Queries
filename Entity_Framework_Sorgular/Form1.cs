using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entity_Framework_Sorgular
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NORTHWNDEntities db = new NORTHWNDEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.ToList();
         /* dataGridView1.DataSource = db.Products.Select(x => new
            {
                UrunAdi = x.ProductName,
                Fiyat = x.UnitPrice,
                Stok = x.UnitsInStock,
                Kategorisi = x.Category.CategoryName
            }).ToList(); */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.OrderBy(x => x.UnitPrice).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock < 10).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Customers.Where(x => x.CompanyName.Contains("Restaurant")).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice >= 20 && x.UnitPrice <= 50).ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text); // Linq içerisinde Convert olmaz. O yüzden ayrı bir yerde tanımladık.
            dataGridView1.DataSource = db.Products.Where(x => x.ProductID == id).ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Çalışanların adını, soyadını, doğum tarihini ve yaşını getiren sorgu:
            dataGridView1.DataSource = db.Employees.Select(x => new {
                AdiSoyadi = x.FirstName + " " + x.LastName,
                DogumTarihi = x.BirthDate,
                // Yas = DateTime.Now.Year - (x.BirthDate.HasValue ? x.BirthDate.Value.Year : DateTime.Now.Year)
                Yas = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now) // Yukardaki kodla aynı işi yapar. SqlFunctions sınıfının gelmesi için System.Data.Entity.SqlServer eklemek gerekir.
            }).ToList();
        }
    }
}
// İlgili tablodan tek bir adet veri gelecekse:
// 1. Yol:
// Product bulunanUrun = db.Products.Where(x => x.ProductID == 2).First();
// Product bulunanUrun = db.Products.Where(x => x.ProductID == 2).FirstOrDefault();
// Product bulunanUrun = db.Products.Where(x => x.ProductID == 2).Single();
// Product bulunanUrun = db.Products.Where(x => x.ProductID == 2).SingleOrDefault();
// 2. Yol:
// Product bulunanUrun = db.Products.FirstOrDefault(x => x.ProductID == 2);
// 3. Yol:
// Find(): Parametresine verilen değeri ilgili tablonun primary key kolonunda arar.
// Product bulunanUrun = db.Products.Find(3);