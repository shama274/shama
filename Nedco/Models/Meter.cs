using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Jedco.Models
{
    public class Meter
    {
        public int? Id { get; set; }
        public int? Customer_id { get; set; }
        public int? Prev_value { get; set; }
        public int? Current_value { get; set; }
        public int? ceil { get; set; }

        public int SaveData()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = new SqlConnection(cstr.con);
                cmd.Connection.Open();
                cmd.CommandText = "SaveMeter";
                if (Id != null) cmd.Parameters.AddWithValue("id", Id);
                if (Customer_id != null) cmd.Parameters.AddWithValue("Customer_id", Customer_id);
                if (Prev_value != null) cmd.Parameters.AddWithValue("Prev_value", Prev_value);
                if (Current_value != null) cmd.Parameters.AddWithValue("Current_value", Current_value);
                if (ceil != null) cmd.Parameters.AddWithValue("ceil", ceil);


                SqlParameter idParam = cmd.Parameters.Add("@id", SqlDbType.Int);
                idParam.Direction = ParameterDirection.InputOutput;

                SqlParameter resultParam = cmd.Parameters.Add("@result", SqlDbType.Int);
                resultParam.Direction = ParameterDirection.InputOutput;

                idParam.Value = this.Id;

                int c = cmd.ExecuteNonQuery();

                this.Id = Convert.ToInt32(idParam.Value);
                int result = Convert.ToInt32(resultParam.Value);
                cmd.Connection.Close();
                return result;
            }
        }
    }
}