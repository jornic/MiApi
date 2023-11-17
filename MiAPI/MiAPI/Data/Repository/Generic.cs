using System.Data;
using Microsoft.Data.SqlClient;

namespace Clase07.Data.Repository
{
    public class Generic : Connection
    {
        protected List<SqlParameter> _parameters;

        protected async Task<DataTable> SqlRead(string sp)
        {
            await using (SqlConnection con = GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter p in _parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                    _parameters.Clear();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        using (DataTable table = new DataTable())
                        {
                            table.Load(reader);
                            return table;
                        }
                    }
                }
            }
        }

        protected async Task<int> SqlWrite(string sp)
        {
            await using (SqlConnection con = GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter p in _parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                    _parameters.Clear();

                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}