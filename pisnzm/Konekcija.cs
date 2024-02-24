using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pisnzm
{
    internal class Konekcija
    {
        public SqlConnection KreirajKonekciju()
        {
            //pruza jednostavan nacin za kreiranje i upravljanje sadrzajem konekcionog stringa
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            {
                DataSource = @"SC2022090-SN9N\SQLEXPRESS", //naziv lokalnog servera 
                InitialCatalog = "KKCZV", //Baza na lokalnom serveru
                IntegratedSecurity = true
            };
            string con = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija;
        }
    }
}
