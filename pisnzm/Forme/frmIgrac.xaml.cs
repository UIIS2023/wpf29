using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for frmIgrac.xaml
    /// </summary>
    public partial class frmIgrac : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmIgrac()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PadajuceListe();
            txtImeIgraca.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        public frmIgrac(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PadajuceListe();
            txtImeIgraca.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        private void PadajuceListe()
        {
            try 
            {
                konekcija.Open();
                string vrKK = @"select KKID, imeKluba from KK";  
                DataTable dtKK = new DataTable();
                SqlDataAdapter daKK = new SqlDataAdapter(vrKK, konekcija);
                daKK.Fill(dtKK);
                cmbKK.ItemsSource = dtKK.DefaultView;
                cmbKK.DisplayMemberPath = "imeKluba";
                dtKK.Dispose();
                daKK.Dispose();


                string vrMen = @"select MenadzerID, imeMenadzera from Menadzer";
                DataTable dtMen = new DataTable();
                SqlDataAdapter daMen = new SqlDataAdapter(vrMen, konekcija);
                daMen.Fill(dtMen);
                cmbMenadzer.ItemsSource = dtMen.DefaultView;
                cmbMenadzer.DisplayMemberPath = "imeMenadzera";
                daMen.Dispose();
                dtMen.Dispose();

            }

            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
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

                cmd.Parameters.Add("@imeIgraca", System.Data.SqlDbType.NVarChar).Value = txtImeIgraca.Text;
                cmd.Parameters.Add("@prezimeIgraca", System.Data.SqlDbType.NVarChar).Value = txtPrezimeIgraca.Text;
                cmd.Parameters.Add("@starostIgraca", System.Data.SqlDbType.Int).Value = txtStarostIgraca.Text;
                cmd.Parameters.Add("@tezinaIgraca", System.Data.SqlDbType.Int).Value = txtTezinaIgraca.Text;
                cmd.Parameters.Add("@visinaIgraca", System.Data.SqlDbType.Int).Value = txtVisinaIgraca.Text;
                cmd.Parameters.Add("@pozicijaIgraca", System.Data.SqlDbType.NVarChar).Value = txtPozicijaIgraca.Text;
                cmd.Parameters.Add("@KKID", System.Data.SqlDbType.Int).Value = cmbKK.SelectedValue;
                cmd.Parameters.Add("@MenadzerID", System.Data.SqlDbType.Int).Value = cmbMenadzer.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Igrac
                                        Set imeIgraca = @imeIgraca, prezimeIgraca = @prezimeIgraca, starostIgraca = @starostIgraca, tezinaIgraca = @tezinaIgraca, visinaIgraca = @visinaIgraca, pozicijaIgraca = @pozicijaIgraca, KKID = @KKID, MenadzerID = @MenadzerID
                                        where IgracID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Igrac (imeIgraca, prezimeIgraca, starostIgraca, tezinaIgraca, visinaIgraca, pozicijaIgraca, KKID, MenadzerID)
                                    values(@imeIgraca,@prezimeIgraca,@starostIgraca,@tezinaIgraca,@visinaIgraca,@pozicijaIgraca,@KKID,@MenadzerID)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }

            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
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
                azuriraj = false;
            }
        }
        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
