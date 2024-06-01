using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
namespace EşleştirmeOyunu
{
    public partial class Form1 : Form
    {
        public static string kullanıcıadi1;
        Random random = new Random(); //sor!!
        List<string> icons = new List<string>() // sor!!
        {
             "!","!","N","N",",",",","k","k",
              "b","b","v","v","w","w","z","z"
        };
        Label firstClicked, secondClicked;  //Resimleri seçerken kullanacağımız değişkenler tanımlanır.
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquare();
        }
        int sayac;

        public static string[] dataskor = new string[100];
        public static string[] dataname = new string[100];
        private void label_Click(object sender, EventArgs e) //Click özelliği olmasa bile herhangi bir tool için mouse'un tıklama özelliği oluşturulması için kullanılan bir eventtir.
        {
            if (firstClicked != null && secondClicked != null) //Henüz bir kutu seçilmemişse, oyuncu bir kutu seçene kadar bekler.
                return;
            Label clickedLabel = sender as Label; //null= Mouse ile seçilen şekkilleri temsil eder.
            if (clickedLabel == null)
                return;
            if (clickedLabel.ForeColor == Color.Black)
                return; //break ile return farkını sor!
            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }
            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;
            CheckForWinner();
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
                timer1.Start();
        }
        public void CheckForWinner()
        {
            int skor = sayac;

            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }

            MessageBox.Show("Tebrikler, kazandınız." + " " + "Skorunuz" + skor + "sn"); //Oyuncunun skor çıktısı oyun sonunda ekrana yazdırılarak oyuncuya gösterilir.

            File.AppendAllText("log.txt", "****************************" + "Skor : " + skor + "****************************" + "\n");  //Oyuncunun skoru text dosyasına yazılır.
            File.AppendAllText("log.txt", "****************************" + "Kullanıcı : " + textBox1.Text + "****************************");


            //Excel dosyasına yazma işlemi:

            string path = (@"D:\staj\ege\PairGame\EşleştirmeOyunu\deneme1.csv");
            if (!File.Exists(path))
            { 
                string createText = textBox1.Text +":"+ skor + Environment.NewLine;
                File.WriteAllText(path, createText);
            }
            string appendText = textBox1.Text +":" + skor + Environment.NewLine;
            File.AppendAllText(path, appendText);

            ///////////////////////////////////////////////////////////////////////////////
            //var text1 = File.ReadAllText(path);
            


            //for (int i = 0; i < theData.Length; i++)
            //{
            //    dataGridView1.Rows.Add(new object[] { i + 1, theData[i] });
            //}
            

            //string[] theData = dataskor;

            //for (int i = 0; i < theData.Length; i++)
            //{
            //    dataGridView1.Rows.Add(new object[] { i + 1, theData[i] });
            //}
            //int uzunluk = dataskor.Length;

            //for (int i = 0; i == uzunluk; i++)
            //{ dataskor1 = dataskor[i]; }


            Close(); //Oyun ekrana çıkan bilgi ekranını kapatır.

            if(skor<=sayac)
            timer2.Stop();
            //this.Close();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form2.kullaniciadi;
            timer2.Start(); //Form1 arayüzü ekrana geldiği anda timer2 sayacı saymaya başlar.

            string path = (@"D:\staj\ege\PairGame\EşleştirmeOyunu\deneme1.csv");
            string[] readText = File.ReadAllLines(path);
            int k = 0;
            string[] parse;
            //string[] dataskor= new string[100];
            //string[] parse1;
            //string[] dataname = new string[100];
            //string[] dataskor = new string[100];

            dataGridView1.Columns.Add("Index", "Index");
            dataGridView1.Columns.Add("Value", "Dice Value");

            for (int i = 0; i < readText.Length; i++)
            {
                parse = readText[i].Split(':');
                if (i > 0)
                {
                    dataskor[k] = parse[1];
                    dataname[k] = parse[0];
                    dataGridView1.Rows.Add(new object[] { dataname[k], dataskor[k] });
                    k++;
                }
            }

            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);

            textBox1.Hide();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac++;
            this.Text = "Oyun Skorunuz: " + sayac + "sn"; //Oyuncunun o andaki skorunu form dosyasının üst kısmına yazar.
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        } //İçerisi boş.

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AssignIconsToSquare()
        {
            Label label; //Oyun başında kutuların rastgele dağılmasını sağlayan kısımdır.
            int randomNumber;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                    label = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;
                randomNumber = random.Next(0, icons.Count);
                label.Text = icons[randomNumber];
                icons.RemoveAt(randomNumber); // Yazılan iconu tekrardan yazmaması için.
            }
        }
    }
}
