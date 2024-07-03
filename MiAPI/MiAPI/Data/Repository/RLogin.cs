using Clase07.Data.Repository;
using MiAPI.Data.Interfaz;
using MiAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiAPI.Data.Repository
{
    public class RLogin : Generic, ILogInUsers
    {
        public async Task<bool> VerificarUsuario(MLogin login)
        {
            _parameters = new List<SqlParameter>{
                 new SqlParameter("@IdUser", login.User),
                 new SqlParameter("@Pwd",login.Password)
             };
            DataTable table = await SqlRead("sp_login");
            return table.Rows.Count > 0;
        }
    }
}
