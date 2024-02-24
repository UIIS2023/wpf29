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
    /// Interaction logic for frmTrener.xaml
    /// </summary>
    public partial class frmTrener : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmTrener()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtStarostTrenera.Focus();
        }

        public frmTrener(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtStarostTrenera.Focus();
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

                cmd.Parameters.Add("@starost", System.Data.SqlDbType.Int).Value = txtStarostTrenera.Text;
                cmd.Parameters.Add("@ime", System.Data.SqlDbType.NVarChar).Value = txtImeTrenera.Text;
                cmd.Parameters.Add("@prezime", System.Data.SqlDbType.NVarChar).Value = txtPrezimeTrenera.Text;
                cmd.Parameters.Add("@tip", System.Data.SqlDbType.NVarChar).Value = txtTipTrenera.Text;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Trener
                                        Set starostTrenera = @starost, imeTrenera = @ime, prezimeTrenera = @prezime, tipTrenera = @tip
                                        where TrenerID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Trener(starostTrenera,imeTrenera,prezimeTrenera,tipTrenera) values(@starost,@ime,@prezime,@tip)";
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
