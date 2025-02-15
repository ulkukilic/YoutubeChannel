using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace YoutubeChannel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // SQL bağlantısı oluşturuluyor
        SqlConnection baglanti = new SqlConnection("Data Source=elıf;Initial Catalog=UdemyYoutubeChannel;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantı açılıyor
                baglanti.Open();
                // SQL komutu hazırlanıyor
                SqlCommand komut = new SqlCommand("INSERT INTO TBLYOUTUBE (AD, KATEGORI, LINK) VALUES (@P1, @P2, @P3)", baglanti);
                // Parametreler ekleniyor
                komut.Parameters.AddWithValue("@P1", txt_name.Text);
                komut.Parameters.AddWithValue("@P2", txt_category.Text);
                komut.Parameters.AddWithValue("@P3", txt_link.Text);
                // Komut çalıştırılıyor
                komut.ExecuteNonQuery();
                // Bağlantı kapatılıyor
                baglanti.Close();
                // Başarılı mesajı gösteriliyor
                MessageBox.Show("Successfully added.");
                // TextBox'lar temizleniyor
                txt_name.Clear();
                txt_category.Clear();
                txt_link.Clear();
            }
            catch (Exception ex)
            {
                // Hata mesajı gösteriliyor
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void video()
        {
            // Tabloyu çekmek için SQL komutu
            SqlDataAdapter data = new SqlDataAdapter("SELECT * FROM TBLYOUTUBE", baglanti);
            DataTable dataTable = new DataTable();
            // DataTable'a veri dolduruluyor
            data.Fill(dataTable);
            // Veriler DataGridView'a yansıtılıyor
            dataGridView1.DataSource = dataTable;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde veriler gösteriliyor
            video();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Seçilen satırdaki hücreye erişim sağlanıyor
                int choose = e.RowIndex;  // Satır indexi alınıyor
                if (choose >= 0)  // Satır geçerli mi kontrol ediliyor
                {
                    // 'LINK' sütununa karşılık gelen indeks (5) doğru mu?
                    string link = dataGridView1.Rows[choose].Cells[5].Value.ToString();

                    // Link geçerli mi kontrol edelim
                    if (Uri.IsWellFormedUriString(link, UriKind.Absolute))
                    {
                        // WebBrowser'a link yönlendiriliyor
                        webBrowser1.Navigate(link);
                    }
                    else
                    {
                        MessageBox.Show("Invalid URL.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj gösteriliyor
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // Seçilen satırdaki veriye ulaşma
            int choose = dataGridView1.SelectedCells[0].RowIndex;
            // Link değeri alınıyor
            string link = dataGridView1.Rows[choose].Cells[5].Value.ToString();
            // WebBrowser'a link yönlendirmesi yapılıyor
            webBrowser1.Navigate(link);

        }
    }
}
