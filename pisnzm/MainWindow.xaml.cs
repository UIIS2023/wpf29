using pisnzm.Frame;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pisnzm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string ucitanaTabela;
        Konekcija kon = new Konekcija();
        bool azuriraj;
        SqlConnection konekcija = new SqlConnection();

        #region Select upiti 

        static string igracSelect = @"select IgracID as ID, imeIgraca as Ime, prezimeIgraca as Prezime, starostIgraca as Starost, tezinaIgraca as Tezina, visinaIgraca as Visina, pozicijaIgraca as Pozicija, imeKluba as 'Ime kluba', imeMenadzera as 'Ime menadzera'
                                        from Igrac join KK on Igrac.KKID = KK.KKID
                                                   join Menadzer on Igrac.MenadzerID = Menadzer.MenadzerID";
        static string halaSelect = @"select HalaID as ID, sirinaHale as Sirina, duzinaHale as Duzina, kapacitetHale as Kapacitet, nazivHale as Naziv, imeKluba as 'Ime kluba'
                                        from Hala join KK on Hala.KKID = KK.KKID";

        static string loptaSelect = @"select LoptaID as ID, velicinaLopte as Velicina, materijalLopte as Materijal, brendLopte as Brend, imeIgraca as 'Ime igraca'
                                        from Lopta join Igrac on Lopta.IgracID = Igrac.IgracID";

        static string KKSelect = @"select KKID as ID, imeKluba as Ime, godinaOsnivanja as godOs, drzavaKluba as Drzava from KK";

        static string trenerSelect = @"select TrenerID as ID, starostTrenera as Starost, imeTrenera as Ime, prezimeTrenera as Prezime, tipTrenera as Tip from Trener";

        static string treningSelect = @"select TreningID as ID, tipTreninga as Tip, trajanjeTreninga as Trajanje, imeTrenera as 'Ime trenera'
                                        from Trening join Trener on Trening.TrenerID = Trener.TrenerID";

        static string publikaSelect = @"select PublikaID as ID, koncentracijaPublike as Koncentracija, imeKluba as 'Ime kluba'
                                        from Publika join KK on Publika.KKID = KK.KKID";

        static string menadzerSelect = @"select MenadzerID as ID, imeMenadzera as Ime, prezimeMenadzera as Prezime, starostMenadzera as Starost from Menadzer";

        #endregion

        #region Select sa uslovom

        string selectUslovIgrac = @"select * from Igrac where IgracID=";
        string selectUslovHala = @"select * from Hala where HalaID=";
        string selectUslovLopta = @"select * from Lopta where LoptaID=";
        string selectUslovKK = @"select * from KK where KKID=";
        string selectUslovTrener = @"select * from Trener where TrenerID=";
        string selectUslovTrening = @"select * from Trening where TreningID=";
        string selectUslovPublika = @"select * from Publika where PublikaID=";
        string selectUslovMenadzer = @"select * from Menadzer where MenadzerID=";

        #endregion

        #region Delete sa uslovom

        string IgracDelete = @"Delete from Igrac where IgracID=";
        string HalaDelete = @"Delete from Hala where HalaID=";
        string LoptaDelete = @"Delete from Lopta where LoptaID=";
        string KKDelete = @"Delete from KK where KKID=";
        string TrenerDelete = @"Delete from Trener where TrenerID=";
        string TreningDelete = @"Delete from Trening where TreningID=";
        string PublikaDelete = @"Delete from Publika where PublikaID=";
        string MenadzerDelete = @"Delete from Menadzer where MenadzerID=";

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(dataGridCentralni, igracSelect);
        }

        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                if(grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }

                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }

            catch(SqlException)
            {
                MessageBox.Show("Neuspesno ucitani podaci", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                if ( konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(igracSelect))
                    {
                        frmIgrac prozorIgrac = new frmIgrac(azuriraj, red);
                        prozorIgrac.txtImeIgraca.Text = citac["ImeIgraca"].ToString();
                        prozorIgrac.txtPrezimeIgraca.Text = citac["PrezimeIgraca"].ToString();
                        prozorIgrac.txtStarostIgraca.Text = citac["StarostIgraca"].ToString();
                        prozorIgrac.txtVisinaIgraca.Text = citac["VisinaIgraca"].ToString();
                        prozorIgrac.txtTezinaIgraca.Text = citac["TezinaIgraca"].ToString();
                        prozorIgrac.txtPozicijaIgraca.Text = citac["PozicijaIgraca"].ToString();
                        prozorIgrac.cmbKK.SelectedValuePath = citac["KKID"].ToString();
                        prozorIgrac.cmbMenadzer.SelectedValuePath = citac["MenadzerID"].ToString();
                        prozorIgrac.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(halaSelect))
                    {
                        frmHala prozorHala = new frmHala(azuriraj, red);
                        prozorHala.txtSirinaHale.Text = citac["SirinaHale"].ToString();
                        prozorHala.txtDuzinaHale.Text = citac["DuzinaHale"].ToString();
                        prozorHala.txtKapacitetHale.Text = citac["KapacitetHale"].ToString();
                        prozorHala.txtNazivHale.Text = citac["NazivHale"].ToString();
                        prozorHala.cmbKK.SelectedValuePath = citac["KKID"].ToString();
                        prozorHala.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(loptaSelect))
                    {
                        frmLopta prozorLopta = new frmLopta(azuriraj, red);
                        prozorLopta.txtMaterijalLopte.Text = citac["MaterijalLopte"].ToString();
                        prozorLopta.txtBrendLopte.Text = citac["BrendLopte"].ToString();
                        prozorLopta.txtVelicinaLopte.Text = citac["VelicinaLopte"].ToString();
                        prozorLopta.cmbIgrac.SelectedValuePath = citac["IgracID"].ToString();
                        prozorLopta.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(KKSelect))
                    {
                        frmKK prozorKK = new frmKK(azuriraj, red);
                        prozorKK.txtImeKluba.Text = citac["ImeKluba"].ToString();
                        prozorKK.txtGodinaOsnivanja.Text = citac["GodinaOsnivanja"].ToString();
                        prozorKK.txtDrzavaKluba.Text = citac["DrzavaKluba"].ToString();
                        prozorKK.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(trenerSelect))
                    {
                        frmTrener prozorTrener = new frmTrener(azuriraj, red);
                        prozorTrener.txtStarostTrenera.Text = citac["StarostTrenera"].ToString();
                        prozorTrener.txtImeTrenera.Text = citac["ImeTrenera"].ToString();
                        prozorTrener.txtPrezimeTrenera.Text = citac["PrezimeTrenera"].ToString();
                        prozorTrener.txtTipTrenera.Text = citac["TipTrenera"].ToString();
                        prozorTrener.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(treningSelect))
                    {
                        frmTrening prozorTrening = new frmTrening(azuriraj, red);
                        prozorTrening.txtTipTreninga.Text = citac["TipTreninga"].ToString();
                        prozorTrening.txtTrajanjeTreninga.Text = citac["TrajanjeTreninga"].ToString();
                        prozorTrening.cmbTrener.SelectedValuePath = citac["TrenerID"].ToString();
                        prozorTrening.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(publikaSelect))
                    {
                        frmPublika prozorPublika = new frmPublika(azuriraj, red);
                        prozorPublika.txtKoncentracijaPublike.Text = citac["KoncentracijaPublike"].ToString();
                        prozorPublika.cmbKK.SelectedValuePath = citac["KKID"].ToString();
                        prozorPublika.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(menadzerSelect))
                    {
                        frmMenadzer prozorMenadzer = new frmMenadzer(azuriraj, red);
                        prozorMenadzer.txtPrezimeMenadzera.Text = citac["PrezimeMenadzera"].ToString();
                        prozorMenadzer.txtImeMenadzera.Text = citac["ImeMenadzera"].ToString();
                        prozorMenadzer.txtStarostMenadzera.Text = citac["StarostMenadzera"].ToString();
                        prozorMenadzer.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }
            void ObrisiZapis(DataGrid grid, string deleteUpit)
            {
                try
                {
                    konekcija.Open();
                    DataRowView red = (DataRowView)grid.SelectedItems[0];
                    MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni da zelite da obrisete?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rezultat == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand()
                        {
                            Connection = konekcija
                        };
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                        cmd.CommandText = deleteUpit + "@id";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Niste selektovali red", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Postoje povezani podaci u nekim drugim tabelama", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
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
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, igracSelect);
        }

        private void btnTrener_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, trenerSelect);
        }

        private void btnMenadzer_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, menadzerSelect);
        }

        private void btnHala_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, halaSelect);
        }

        private void btnKK_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, KKSelect);
        }

        private void btnTrening_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, treningSelect);
        }

        private void btnLopta_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, loptaSelect);
        }

        private void btnPublika_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, publikaSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(igracSelect))
            {
                prozor = new frmIgrac();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, igracSelect);
            }
            else if (ucitanaTabela.Equals(halaSelect))
            {
                prozor = new frmHala();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, halaSelect);
            }
            else if (ucitanaTabela.Equals(loptaSelect))
            {
                prozor = new frmLopta();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, loptaSelect);
            }
            else if (ucitanaTabela.Equals(KKSelect))
            {
                prozor = new frmKK();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, KKSelect);
            }
            else if (ucitanaTabela.Equals(trenerSelect))
            {
                prozor = new frmTrener();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, trenerSelect);
            }
            else if (ucitanaTabela.Equals(treningSelect))
            {
                prozor = new frmTrening();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, treningSelect);
            }
            else if (ucitanaTabela.Equals(publikaSelect))
            {
                prozor = new frmPublika();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, publikaSelect);
            }
            else if (ucitanaTabela.Equals(menadzerSelect))
            {
                prozor = new frmMenadzer();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, menadzerSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(igracSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovIgrac);
                UcitajPodatke(dataGridCentralni, igracSelect);
            }
            else if (ucitanaTabela.Equals(halaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovHala);
                UcitajPodatke(dataGridCentralni, halaSelect);
            }
            else if (ucitanaTabela.Equals(loptaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovLopta);
                UcitajPodatke(dataGridCentralni, loptaSelect);
            }
            else if (ucitanaTabela.Equals(KKSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovKK);
                UcitajPodatke(dataGridCentralni, KKSelect);
            }
            else if (ucitanaTabela.Equals(trenerSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovTrener);
                UcitajPodatke(dataGridCentralni, trenerSelect);
            }
            else if (ucitanaTabela.Equals(treningSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovTrening);
                UcitajPodatke(dataGridCentralni, treningSelect);
            }
            else if (ucitanaTabela.Equals(publikaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovPublika);
                UcitajPodatke(dataGridCentralni, publikaSelect);
            }
            else if (ucitanaTabela.Equals(menadzerSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovMenadzer);
                UcitajPodatke(dataGridCentralni, menadzerSelect);
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(igracSelect))
            {
                ObrisiZapis(dataGridCentralni, IgracDelete);
                UcitajPodatke(dataGridCentralni, igracSelect);
            }
            else if (ucitanaTabela.Equals(halaSelect))
            {
                ObrisiZapis(dataGridCentralni, HalaDelete);
                UcitajPodatke(dataGridCentralni, halaSelect);
            }
            else if (ucitanaTabela.Equals(loptaSelect))
            {
                ObrisiZapis(dataGridCentralni, LoptaDelete);
                UcitajPodatke(dataGridCentralni, loptaSelect);
            }
            else if (ucitanaTabela.Equals(KKSelect))
            {
                ObrisiZapis(dataGridCentralni, KKDelete);
                UcitajPodatke(dataGridCentralni, KKSelect);
            }
            else if (ucitanaTabela.Equals(trenerSelect))
            {
                ObrisiZapis(dataGridCentralni, TrenerDelete);
                UcitajPodatke(dataGridCentralni, trenerSelect);
            }
            else if (ucitanaTabela.Equals(treningSelect))
            {
                ObrisiZapis(dataGridCentralni, TreningDelete);
                UcitajPodatke(dataGridCentralni, treningSelect);
            }
            else if (ucitanaTabela.Equals(publikaSelect))
            {
                ObrisiZapis(dataGridCentralni, PublikaDelete);
                UcitajPodatke(dataGridCentralni, publikaSelect);
            }
            else if (ucitanaTabela.Equals(menadzerSelect))
            {
                ObrisiZapis(dataGridCentralni, MenadzerDelete);
                UcitajPodatke(dataGridCentralni, menadzerSelect);
            }
        }
    }
}
