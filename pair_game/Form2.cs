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
        
        public static string kullaniciadi;
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            kullaniciadi = Convert.ToString(textBox1.Text); //Form2'de bulunan kullanıcı adı bir değişkene atılır.
            Form1.kullanıcıadi1 = kullaniciadi; //Form2'deki değişken Form1'deki değişkene atılır.

            Form1 frm1 = new Form1();    //Form2 arayüzünden Form1 arayüzüne geçmek için tanımlanan değişkendir.
            Form2 frm2 = new Form2();
            //this.Close(); //Form2'den Form1'e geçiş yapıldığında, Form 2 gizlenir.
            frm2.Close();
            frm1.ShowDialog(); //Form 2' den Form 1'e geçiş yapılır.
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            // Oyun başlangıcında oyuncunun bilgileri Form 2'deki score listboxuna aktarılır.
            string path = @"D:\staj\ege\PairGame\EşleştirmeOyunu\bin\Debug\log.txt";
            var str = File.ReadAllText(path);
            textBox2.Text = str;
        }
        public void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
