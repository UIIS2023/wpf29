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
    /// Interaction logic for frmLopta.xaml
    /// </summary>
    public partial class frmLopta : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmLopta()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PadajucaLista();
            txtMaterijalLopte.Focus();
        }
        public frmLopta(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PadajucaLista();
            txtMaterijalLopte.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        public void PadajucaLista()
        {
            try
            {
                konekcija.Open();
                string vrIgrac = @"select IgracID, imeIgraca from Igrac";
                DataTable dtIgrac = new DataTable();
                SqlDataAdapter daIgrac = new SqlDataAdapter(vrIgrac, konekcija);
                daIgrac.Fill(dtIgrac);
                cmbIgrac.ItemsSource = dtIgrac.DefaultView;
                cmbIgrac.DisplayMemberPath = "imeIgraca";
                daIgrac.Dispose();
                dtIgrac.Dispose();
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
                cmd.Parameters.Add("@mat", System.Data.SqlDbType.NVarChar).Value = txtMaterijalLopte.Text;
                cmd.Parameters.Add("@brend", System.Data.SqlDbType.NVarChar).Value = txtBrendLopte.Text;
                cmd.Parameters.Add("@velicina", System.Data.SqlDbType.Int).Value = txtVelicinaLopte.Text;
                cmd.Parameters.Add("@IgracID", System.Data.SqlDbType.Int).Value = cmbIgrac.SelectedValue;
               
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Lopta
                                        Set materijalLopte = @mat, brendLopte = @brend, velicinaLopte = @velicina, IgracID = @IgracID
                                        where LoptaID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Lopta(materijalLopte,brendLopte,velicinaLopte,IgracID) values(@mat,@brend,@velicina,@IgracID)";
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
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
