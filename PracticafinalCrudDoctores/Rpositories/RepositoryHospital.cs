using System.Data;
using Microsoft.Data.SqlClient;
using PracticafinalCrudDoctores.Models;

namespace PracticafinalCrudDoctores.Rpositories
{
    public class RepositoryHospital
    {
        private DataTable tablaHosp;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryHospital()
        {
            string cnString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITALES;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from HOSPITAL";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, cnString);
            this.tablaHosp = new DataTable();
            adapter.Fill(this.tablaHosp);   
            this.cn = new SqlConnection(cnString);  
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Hospital> GetHospitales()
        {
            var consulta = from datos in this.tablaHosp.AsEnumerable() select datos;
            List<Hospital> listaHospitales = new List<Hospital>();
            foreach(var row in consulta)
            {
                Hospital hospital = new Hospital();
                hospital.IdHospital = row.Field<int>("HOSPITAL_COD");
                hospital.Nombre = row.Field<string>("NOMBRE");
                hospital.Direccion = row.Field<string>("DIRECCION");
                hospital.Telefono = row.Field<string>("TELEFONO");
                hospital.NumCamas = row.Field<int>("NUM_CAMA");
                listaHospitales.Add(hospital);
            }


            return listaHospitales;


        }
    }
}
