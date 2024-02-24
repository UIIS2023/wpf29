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
    /// Interaction logic for frmTrening.xaml
    /// </summary>
    public partial class frmTrening : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmTrening()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            padajucaLista();
            txtTipTreninga.Focus();
        }

        public frmTrening(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            padajucaLista();
            txtTipTreninga.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        public void padajucaLista()
        {
            try
            {
                konekcija.Open();
                string vrTrening = @"select trenerID, imeTrenera from Trener";
                DataTable dtTrening = new DataTable();
                SqlDataAdapter daTrening = new SqlDataAdapter(vrTrening, konekcija);
                daTrening.Fill(dtTrening);
                cmbTrener.ItemsSource = dtTrening.DefaultView;
                cmbTrener.DisplayMemberPath = "imeTrenera";
                dtTrening.Dispose();
                daTrening.Dispose();
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
                cmd.Parameters.Add("@tip", System.Data.SqlDbType.NVarChar).Value = txtTipTreninga.Text;
                cmd.Parameters.Add("@trajanje", System.Data.SqlDbType.Int).Value = txtTrajanjeTreninga.Text;
                cmd.Parameters.Add("@TrenerID", System.Data.SqlDbType.Int).Value = cmbTrener.SelectedValue;
               
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Trening
                                        Set tipTreninga = @tip, trajanjeTreninga = @trajanje, trenerID = @TrenerID
                                        where TreningID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Trening(tipTreninga,trajanjeTreninga,trenerID) values(@tip,@trajanje,@TrenerID)";
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
