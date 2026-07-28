using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApiClinica.Data
{
    public class ConexionBase
    {

        public DataSet CreateDataSetBench(SqlCommand sqlCmd, string _CadenaSQL)
        {
            SqlConnection ConClinica;
            ConClinica = new SqlConnection(_CadenaSQL);
            ConClinica.Open();
            sqlCmd.Connection = ConClinica;
            sqlCmd.CommandTimeout = 3600;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = sqlCmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            ConClinica.Close();
            return ds;
        }

        public void ExecuteNonQuery(SqlCommand sqlCmd, string _CadenaSQL)
        {
            SqlConnection ConClinica;
            ConClinica = new SqlConnection(_CadenaSQL);
            ConClinica.Open();
            sqlCmd.Connection = ConClinica;
            sqlCmd.CommandTimeout = 3600;
            sqlCmd.ExecuteNonQuery();
            ConClinica.Close();
        }
    }
}
