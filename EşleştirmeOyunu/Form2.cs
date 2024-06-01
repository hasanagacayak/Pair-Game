using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace EşleştirmeOyunu
{
    public partial class Form2 : Form
    {
        public static string kullaniciadi; //Form2 de kullandığımı kullanıcı adını tutan değişkeni public static şekilde tanıtarak Form1'e aktarılması sağlanır.
        public Form2()
        {
            InitializeComponent();
        } //İçerisi boştur.
        private void button1_Click(object sender, EventArgs e) //Form'de bulunan button1'in eventleri aşağıda tanımlanır.
        {
            kullaniciadi = Convert.ToString(textBox1.Text); //Form2'de bulunan kullanıcı adı bir değişkene atılır.
            Form1.kullanıcıadi1 = kullaniciadi; //Form2'deki değişken Form1'deki değişkene atılır.
            Form1 frm1 = new Form1();    //Form2 arayüzünden Form1 arayüzüne geçmek için tanımlanan değişkendir.
            Form2 frm2 = new Form2(); //Form 2 arayüzündeki işlemler için bir değişken tanımlanır.
            this.Hide();  //Form2'den Form1'e geçiş yapıldığında, Form 2 gizlenir.
            frm1.ShowDialog(); //Form 2' den Form 1'e geçiş yapılır.
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            // Oyun başlangıcında oyuncunun bilgileri text dosyasına yazılması için yapılan işlemler aşağıda verilmiştir.
            string path = @"C:\Users\HasanAgacayakk\OneDrive - gazi.edu.tr\Masaüstü\PairGame\EşleştirmeOyunu\bin\Debug\log.txt"; //Bilgilerin text dosyasına yazılması için txt dosyasına bilgisayardaki konumunu gösteren bir değişken tanımlanır.
            var str = File.ReadAllText(path); //Text dosyasının bilgisayardaki konumu bildirerek bu konumdaki bilgiler str değişkenine aktarılır.
            textBox2.Text = str; //Excel dosyasındaki bilgilerinin kayıtlı olduğu str değişkeni textBox2'ye yazılarak Form2'de bu bilgilerin oyuncuya gösterilmesi sağlanır.
        }
        public void textBox1_TextChanged(object sender, EventArgs e) //İçerisi boştur.
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)//İçerisi boştur.
        {

        }
    }
}