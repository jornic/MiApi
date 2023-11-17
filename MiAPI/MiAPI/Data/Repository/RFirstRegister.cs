using Clase07.Data.Repository;
using MiAPI.Data.Interfaz;
using MiAPI.Models;
using Microsoft.Data.SqlClient;

namespace MiAPI.Data.Repository
{
    public class RFirstRegister :Generic, IInsert<MfirstRegister>
    {
        public async Task<bool> CreateAsync(MfirstRegister entity)
        {
            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@Nombre", entity.Name),
                new SqlParameter("@Apellido", entity.LastName),
                new SqlParameter("@Telefono",entity.PhoneNumber),
                new SqlParameter("@Correo",entity.Email),
                new SqlParameter("@Ciudad",entity.City),
                new SqlParameter("@Direccion",entity.Address),
                new SqlParameter("@User",entity.User),
                new SqlParameter("@Constrasena",entity.Password)
            };

            return await SqlWrite("sp_InsertFirtRegister") > 0;
        }
    }
}
