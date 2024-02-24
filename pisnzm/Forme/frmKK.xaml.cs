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
    /// Interaction logic for frmKK.xaml
    /// </summary>
    public partial class frmKK : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmKK()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtImeKluba.Focus();
        }

        public frmKK(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtImeKluba.Focus();
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

                cmd.Parameters.Add("@imeKluba", System.Data.SqlDbType.NVarChar).Value = txtImeKluba.Text;
                cmd.Parameters.Add("@godOs", System.Data.SqlDbType.Int).Value = txtGodinaOsnivanja.Text;
                cmd.Parameters.Add("@drzavaKluba", System.Data.SqlDbType.NVarChar).Value = txtDrzavaKluba.Text;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update KK
                                        Set imeKluba = @imeKluba, godinaOsnivanja = @godOs, drzavaKluba = @drzavaKluba
                                        where KKID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into KK(imeKluba,godinaOsnivanja,drzavaKluba) values(@imeKluba,@godOs,@drzavaKluba)";
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
