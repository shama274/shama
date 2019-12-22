using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Nedco.Models
{
    public class CustomerParameters
    {
        public string Name { get; set; }
        //public string Telephone { get; set; }
        //public string Mobile { get; set; }
        public string Status { get; set; }
        public string CurrentPage { get; set; }
        public string PageLength { get; set; }
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string IBAN { get; set; }
        public int SaveData()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = new SqlConnection(cstr.con);
                cmd.Connection.Open();
                cmd.CommandText = "SaveCustomer";
                if (Name != null) cmd.Parameters.AddWithValue("name", Name);
                if (Address != null) cmd.Parameters.AddWithValue("address", Address);
                if (Mobile != null) cmd.Parameters.AddWithValue("mobile", Mobile);
                if (IBAN != null) cmd.Parameters.AddWithValue("IBAN", IBAN);

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
        public static Customer[] GetCustomers(CustomerParameters parameters, out int rowsCount)
        {
            List<Customer> l = new List<Customer>();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = new SqlConnection(cstr.con);
                cmd.Connection.Open();
                cmd.CommandText = "GetCustomers";

                /*cmd.Parameters.AddWithValue("name", parameters.Name);
                cmd.Parameters.AddWithValue("status", parameters.Status);
                cmd.Parameters.AddWithValue("page", parameters.CurrentPage);
                cmd.Parameters.AddWithValue("pageLength", parameters.PageLength);
                SqlParameter rowsCountParam = cmd.Parameters.Add("rowsCount", SqlDbType.Int);
                rowsCountParam.Direction = ParameterDirection.InputOutput;*/

                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        Customer c = new Customer();
                        if (r["id"] != DBNull.Value) c.Id = Convert.ToInt32(r["id"]);
                        if (r["name"] != DBNull.Value) c.Name = Convert.ToString(r["name"]);
                        if (r["address"] != DBNull.Value) c.Address = Convert.ToString(r["address"]);
                        if (r["mobile"] != DBNull.Value) c.Mobile = Convert.ToString(r["mobile"]);
                        if (r["IBAN"] != DBNull.Value) c.IBAN = Convert.ToString(r["IBAN"]);

                        l.Add(c);
                    }
                }

                r.Close();
                cmd.Connection.Close();
                //rowsCount = Convert.ToInt32(rowsCountParam.Value);
                rowsCount = l.Count;
            }
            return l.ToArray();

        }
    }
}