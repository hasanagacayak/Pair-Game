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
namespace EşleştirmeOyunu
{
    public partial class Form1 : Form
    {
        # region    
        public static string kullanıcıadi1; //  Form2'de oyuncudan alınan kullanıcı adını Form1'e aktarılırken kullanılan değişken tanımlanır.
        Random random = new Random(); //Oyun esnasında kullanılan ikonların rastgele dağıtılması için kullanılacak değişken tanımlanır.
        #endregion                  //Bu region'da değişkenler tanımlanır.
        List<string> icons = new List<string>() //Oyundaki ikonların tutulacağı liste tanımlanır. İkonlar string olacağu için bu şekilde tanıtıldı.
        {
             "!","!","N","N",",",",","k","k", //Oyunda kullanılacak ikonlar seçildi.
              "b","b","v","v","w","w","z","z"
        };
        Label firstClicked, secondClicked;  // Oyuncu tarafından mouse ile ikon seçerken kullanacağımız değişkenler tanımlanır.
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquare();
        }
        #region  
        int sayac; // Oyun başladıktan sonra kullanıcının skorunu bulmak için oluşturduğumuz süre değişkenidir. 
        public static string[] dataskor = new string[100]; //Excel'den çekilen verileri array'e atarak işleme aldığımız değişkenler tanımlanır.
        public static string[] dataname = new string[100]; //Excel'den çekilen verileri array'e atarak işleme aldığımız değişkenler tanımlanır.
        #endregion    //Değişkenler bu region'da tanımlanır.        
        private void label_Click(object sender, EventArgs e) //Click özelliği olmasa bile herhangi bir tool için mouse'un tıklama özelliği oluşturulması için kullanılan bir eventtir.
        {   //null= Mouse ile seçilen ikonları temsil eder.
            if (firstClicked != null && secondClicked != null) //Oyuncu tarafından hiç bir kutu seçilmemişse bir işlem yapmıyor ve oyuncunun seçimini bekliyoruz.
                return;
            Label clickedLabel = sender as Label;  // ClickedLabel değişkeni label'ın bir elemanı haline getirilir.
            if (clickedLabel == null) //FirstClick ve SecondClick tarafından tutulan ikonları bu değişkene atarız ve işleme devam ederiz. 
                return;
            if (clickedLabel.ForeColor == Color.Black) //Oyuncu tarafından seçilen ikonların rengini koyu griden siyaha dönüştürme işlemi tanıtılır..
                return; 
            if (firstClicked == null) //İlk seçilen ikonun rengini koyu griden siyaha dönüştürür.
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black; //Renk değiştirme işlemi yapılır.
                return;
            }
            secondClicked = clickedLabel;  //İkinci seçilen ikonun rengini koyu griden siyaha dönüştürür.
            secondClicked.ForeColor = Color.Black; //Renk değiştirme işlemi yapılır.
            CheckForWinner(); //Oyuncu tarafından seçilen ikonlar birbiriyle aynıysa iki ikonuda açık tutması için yapılan işlemler tanıtılır.
            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null; 
                secondClicked = null;
            }
            else
                timer1.Start(); //Oyuncu tarafından açılan iki ikon da aynı değilse ikonlar açıldıktan 500ms sonra geri kapatılması için tanıtılan timerdır.
        }
        public void CheckForWinner() //Oyuncunun oyunu bitiridiğinde yapılacak işlemlerin tanıtıldığı eventlerdir.
        {
            #region
            int skor = sayac; //Oyuncunun oyunu bittiğinde sayaçtaki süresini skor değişkenine atarak oyuncunun skorunu belirleyecek değişken tanımlandı.
            Label label; //
            #endregion
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++) //Oyuncu 16 label'ı açarak eşlerini bulmadan oyunun kapanmamasını sağlar.
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                    return;
            }
            //Oyuncunun bilgilerini bilgisayardaki dosyalara yazma işlemleri aşağıda tanımlanır.
            #region
            MessageBox.Show("Tebrikler, kazandınız." + " " + "Skorunuz" + skor + "sn"); //Oyuncunun skor çıktısı oyun sonunda ekrana yazdırılarak oyuncuya gösterilir.
            //Oyuncunun bilgilerini text dosyasına yazmak için kullanılan kodlar.
            File.AppendAllText("log.txt", "Kullanıcı : " + textBox1.Text + "--------->"); //Oyuncunun kullanıcı adı text dosyasına yazılır.
            File.AppendAllText("log.txt", "Skor : " + skor + Environment.NewLine);  //Oyuncunun skoru text dosyasına yazılır.
            //Oyuncunun bilgilerini excel dosyasına yazmak için kullanılan kodlar.
            string path = (@"C:\Users\HasanAgacayakk\OneDrive - gazi.edu.tr\Masaüstü\PairGame\EşleştirmeOyunu\deneme1.csv"); //Oyuncunun bilgilerinin aktarılacağı excel dosyasının konumu tanıtılır.
            if (!File.Exists(path)) //Eğer excel dosyası bilgisayarda bulunmuyorsa...
            { 
                string createText = textBox1.Text +":"+ skor + Environment.NewLine; //Yeni bir excel dosyası oluşturarak oyuncunun bilgileri aktarılır.
                File.WriteAllText(path, createText);
            }
            string appendText = textBox1.Text +":" + skor + Environment.NewLine; //Belirtilen excel dosyasına oyuncunun bilgileri aktarılır.
            File.AppendAllText(path, appendText);
            #endregion
            Close(); //Oyun, ekrana çıkan bilgi ekranını kapatır.

            if(skor<=sayac) //Oyuncunun skoru, oyun başlarken başlatılan sayaç'tan küçükse program oyunun bittiğini anlayarak sayacı durdurarak form dosyasını kapatır. 
            timer2.Stop(); //Sayaç timer'ı durdurulur.
            this.Hide();
            Application.Exit(); //Form sekmesi kapatılır.
        }
        private void timer1_Tick(object sender, EventArgs e) //Timer1= Oyuncunun seçtiği kutular farklıysa kutuların yeniden kapatılması için belirleyen süreyi tanımlayan timerdır.
        {
            timer1.Stop(); //Timer 1 ikonlar kapalıyken durdurulur.
            firstClicked.ForeColor = firstClicked.BackColor; //Birinci seçilen ikonun renginini siyahtan griye çevrilir.
            secondClicked.ForeColor = secondClicked.BackColor;//İkinci seçilen ikonun renginini siyahtan griye çevrilir.
            firstClicked = null; //Birinci seçilen ikon.
            secondClicked = null; //İkinci seçilen ikon.
        }
        private void Form1_Load(object sender, EventArgs e) //Form1 açılırken yapılacak işlemler aşağıda yazılmıştır.
        {
            #region
            textBox1.Text = Form2.kullaniciadi; //Form2'de tanıtılan kullanıcı adını Form1'de bulunan textbox1'e yazarak formlar arası değişken aktarımı sağlanır.
            timer2.Start(); //Form1 arayüzü ekrana geldiği anda timer2 sayacı saymaya başlar.
            string path = (@"C:\Users\HasanAgacayakk\OneDrive - gazi.edu.tr\Masaüstü\PairGame\EşleştirmeOyunu\deneme1.csv"); //Excel'de yazılan oyuncu bilgilerini hem ekrana yazmak hem de işlem yapmak için tekrardan dosya konumu tanımlanır.
            string[] readText = File.ReadAllLines(path); //Excel dosyasındaki veriler içerisinde string öğeler barındıran readText array'ine yazılır.
            int k = 0; //Aşağıdaki for döngüsünde tanıtılan array'lerin indexlerine göre sıtayla yazılmasını sağlayan değişken tanıtırlır.
            string[] parse; //Excel dosyasında kullanıcı adı ve skor bilgileri aynı hücreye yanyana yazıldığı için bu bilgileri ayrı satırlara ayırmamız için kullanılmasını sağlayan değişken tanımlanır..
            #endregion
            dataGridView1.Columns.Add("Kullanıcı Adı:", "Kullanıcı Adı:"); //Data Grid View tool'unun birinci sütununun ismini belirler.
            dataGridView1.Columns.Add("Skor:", "Skor:"); //Data Grid View tool'unun ikinci sütununun ismini belirler.

            for (int i = 0; i < readText.Length; i++) //Excel dosyasındaki kullanıcı sayısı kadar çalışacak for döngüsü tanımlanır...
            {
                parse = readText[i].Split(':'); //Excel dosyasında okunan verilerdeki aynı hücrede bulunan kullanıcı adı ve skor'u ayrı sütunlara ayrılması sağlanır.
                if (i > 0) //i değişkeni için 1'den başlayarak kullanıcı sayısına kadar ayırma işlemi yapılır.
                {
                    dataskor[k] = parse[1]; //Aynı hücrede bulunan verileri ':' işaretinden sonraki verileri ayırarak dataskor array'inin k'nıncı indexine atar.
                    dataname[k] = parse[0]; //Aynı hücrede bulunan verileri ':' işaretinden önceki verileri ayırarak dataname array'inin k'nıncı indexine atar.
                    dataGridView1.Rows.Add(new object[] { dataname[k], dataskor[k] }); //Verileri yukarıda oluşturulan dataskor ve dataname array'lerini iki stun şeklinde Data Grid View'a yazdırılır.
                    k++; //Arraylerin index sayıları her döngüde arttırılırak index sayısı sonraki girdiye ilerlenir.
                }
            }
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending); //Data Grid View'da bulunan skor'ları küçükten büyüğe yazdırarak sıralanmış halini skor tablosunda gösterir.
            textBox1.Hide();//Form 1'de bulunan textbox 1'i gizlenerek. Form 1 daha düzenli gösterilir. 
        }
        private void timer2_Tick(object sender, EventArgs e) //Sayaç timer'ının bulunduğu timer 2'nin özellikleri aşağıda tanımlanır.
        {
            sayac++; //Oyun başladıktan sonra sürenin oyun bitene kadar 1 saniye aralıklarla ilerlenmesi sağlanır.
            this.Text = "Oyun Skorunuz: " + sayac + "sn"; //Oyuncunun o andaki skorunu form dosyasının üst kısmına yazar.
        }
        private void textBox1_TextChanged(object sender, EventArgs e)//İçerisi boştur.
        {
        } 
        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//İçerisi boştur.
        {

        }  
        private void AssignIconsToSquare() //Oyun başında kutuların rastgele dağılmasını sağlayan kısımdır.
        {
            Label label;  //Label değişkeni tanımlanır.
            int randomNumber; //Oyun açıldığında ikonların rastgele dağıtılmasını sağlayan değişken tanımlanır.
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++) // For döngüsü oyundaki kare sayısı kadar döndürülür...
            {
                if (tableLayoutPanel1.Controls[i] is Label) //Sistemin random için seçtiği ikon eğer seçilmemiş ikonsa label'a atılır.
                    label = (Label)tableLayoutPanel1.Controls[i]; 
                else
                    continue;
                randomNumber = random.Next(0, icons.Count); //Kullandığı ikonları sayar.
                label.Text = icons[randomNumber]; //İkonların rastgele bir sayı ile dağıtılması sağlanır.
                icons.RemoveAt(randomNumber); // Yazılan ikonu tekrardan yazmaması için kullanılır.
            }
        }
    }
}