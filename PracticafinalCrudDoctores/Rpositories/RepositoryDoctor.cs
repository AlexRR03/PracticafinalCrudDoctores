using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Numerics;
using Microsoft.Data.SqlClient;
using PracticafinalCrudDoctores.Models;
namespace PracticafinalCrudDoctores.Rpositories
{
    public class RepositoryDoctor
    {
        private DataTable tablaDr;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        #region SqlQuerys
            //        create procedure SP_INSERT_DOCTOR
            //(@idHospital int, @idDoctor int, @apellido nvarchar(50), @especialidad nvarchar(50), @salario int)

            //as
            //	declare @nextId int
            //    select @nextId= max(DOCTOR_NO)+1 from DOCTOR

            //    insert into DOCTOR values(@idHospital, @nextId, @apellido, @especialidad, @salario)
            //go
        #endregion
        public RepositoryDoctor()
        {
            string cnString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";
            string sql = "select D.HOSPITAL_COD,D.DOCTOR_NO,D.APELLIDO,D.ESPECIALIDAD,D.SALARIO,H.NOMBRE from DOCTOR D\r\nleft join HOSPITAL H\r\non H.HOSPITAL_COD = D.HOSPITAL_COD";
            SqlDataAdapter adapter = new SqlDataAdapter(sql,cnString);
            this.tablaDr = new DataTable();
            adapter.Fill(this.tablaDr);
            this.cn = new SqlConnection(cnString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;     
        }

        public List<Doctor> GetDoctores()
        { 
            var consulta = from datos in this.tablaDr.AsEnumerable() select
                           datos;
            List<Doctor> listaDoctores = new List<Doctor>();
            foreach(var row in consulta)
            {
                Doctor doctor = new Doctor();
                doctor.IdHospital = row.Field<int>("HOSPITAL_COD");
                doctor.IdDoctor = row.Field<int>("DOCTOR_NO");
                doctor.Apellido = row.Field<string>("APELLIDO");
                doctor.Especialidad = row.Field<string>("ESPECIALIDAD");
                doctor.Salario = row.Field<int>("SALARIO");
                doctor.NombreHospital = row.Field<string>("NOMBRE");
                listaDoctores.Add(doctor);
            }
            return listaDoctores;
        }
        public Doctor FindDoctor(int idDoctor)
        {
            var consulta = from datos in this.tablaDr.AsEnumerable() where datos.Field<int>("DOCTOR_NO") == idDoctor select datos;
            var row = consulta.First();
            Doctor doctor = new Doctor();
            doctor.IdHospital = row.Field<int>("HOSPITAL_COD");
            doctor.IdDoctor = row.Field<int>("DOCTOR_NO");
            doctor.Apellido = row.Field<string>("APELLIDO");
            doctor.Especialidad = row.Field<string>("ESPECIALIDAD");
            doctor.Salario = row.Field<int>("SALARIO");
            doctor.NombreHospital = row.Field<string>("NOMBRE");
            return doctor;
        }
        public async Task InsertDoctorAsync(int idHospital,string apellido,string especialidad, int salario)
        {
            string sql = "SP_INSERT_DOCTOR";
            this.com.Parameters.AddWithValue("@idhospital", idHospital);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        
    }
}
