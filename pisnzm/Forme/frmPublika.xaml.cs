﻿using System;
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
    /// Interaction logic for frmPublika.xaml
    /// </summary>
    public partial class frmPublika : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmPublika()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PadajuceListe();
            txtKoncentracijaPublike.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        public frmPublika(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PadajuceListe();
            txtKoncentracijaPublike.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        public void PadajuceListe()
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

                cmd.Parameters.Add("@konc", System.Data.SqlDbType.Int).Value = txtKoncentracijaPublike.Text;
                cmd.Parameters.Add("@KKID", System.Data.SqlDbType.Int).Value = cmbKK.SelectedValue;

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update Publika
                                        Set koncentracijaPublike = @konc, KKID = @KKID
                                        where PublikaID = @id";
                    this.pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Publika(koncentracijaPublike, KKID) values(@konc,@KKID)";   
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

