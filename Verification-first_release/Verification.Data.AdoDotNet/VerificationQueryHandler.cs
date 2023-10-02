using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Verification.Data.AdoDotNet
{
    public class VerificationQueryHandler
    {
        readonly string connectionString;
        public VerificationQueryHandler(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Tuple<bool, dynamic> GetData(List<string> Qry)
        {
            DataSet ds = null;
            DataTable tbl = null;
            StringBuilder stringBuilder = null;
            try
            {
                if (Qry.Count > 1)
                {
                    ds = new DataSet();
                    stringBuilder = new StringBuilder();

                    foreach (string str in Qry)
                        stringBuilder.Append(str + ";");
                }
                else
                {
                    tbl = new DataTable();
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (tbl != null)
                        new SqlDataAdapter(Qry[0], con).Fill(tbl);
                    else if (ds != null)
                        new SqlDataAdapter(stringBuilder.ToString(), con).Fill(ds);
                }
                if (Qry.Count == 1)
                    return new Tuple<bool, dynamic>(true, tbl);
                else
                    return new Tuple<bool, dynamic>(true, ds);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, dynamic>(false, ex);
            }
        }

        public Tuple<bool, dynamic> Reader(string Qry, Dictionary<string, dynamic> para = null, int Timeout = 60)
        {
            DataTable tbl = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    tbl = new DataTable();
                    SqlCommand cmd = new SqlCommand(Qry, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Timeout;
                    if (para != null) foreach (var aa in para) cmd.Parameters.AddWithValue(aa.Key, aa.Value);
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    tbl.Load(reader);
                    return new Tuple<bool, dynamic>(true, tbl);
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, dynamic>(false, ex);
            }
        }
        public Tuple<bool, dynamic> NonQuery(string Qry, Dictionary<string, dynamic> para = null, int Timeout = 60)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();
                    SqlTransaction sqlTransaction = con.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(Qry, con, sqlTransaction); ;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Timeout;
                    if (para != null) foreach (var aa in para) cmd.Parameters.AddWithValue(aa.Key, aa.Value);
                    try
                    {
                        int exec = cmd.ExecuteNonQuery();
                        sqlTransaction.Commit();
                        return new Tuple<bool, dynamic>(true, exec);
                    }
                    catch (SqlException ex)
                    {
                        sqlTransaction.Rollback();
                        return new Tuple<bool, dynamic>(false, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, dynamic>(false, ex);
            }
        }
        public Tuple<bool, dynamic> Scaller(string Qry, Dictionary<string, dynamic> para = null, int Timeout = 60)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(Qry, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = Timeout;
                    if (para != null) foreach (var aa in para) cmd.Parameters.AddWithValue(aa.Key, aa.Value);
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open();
                    return new Tuple<bool, dynamic>(true, cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, dynamic>(false, ex);
            }
        }
    }
}
