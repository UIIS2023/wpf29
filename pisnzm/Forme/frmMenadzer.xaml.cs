using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pisnzm.Frame
{
    /// <summary>
    /// Interaction logic for frmMenadzer.xaml
    /// </summary>
    public partial class frmMenadzer : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmMenadzer()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtPrezimeMenadzera.Focus();
        }

        public frmMenadzer(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtPrezimeMenadzera.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };

                cmd.Parameters.Add("@prezime", System.Data.SqlDbType.NVarChar).Value = txtPrezimeMenadzera.Text;
                cmd.Parameters.Add("@ime", System.Data.SqlDbType.NVarChar).Value = txtImeMenadzera.Text;
                cmd.Parameters.Add("@starost", System.Data.SqlDbType.Int).Value = Convert.ToInt32(txtStarostMenadzera);

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Menadzer
                                        Set prezimeMenadzera = @prezime, imeMenadzera = @ime, starostMenadzera = @starost
                                        where MenadzerID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Menadzer(prezimeMenadzera, imeMenadzera, starostMenadzera) values(@prezime,@ime,@starost)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Greska prilikom konverzije podataka", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
