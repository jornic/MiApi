using Clase07.Data.Repository;
using MiAPI.Data.Interfaz;
using MiAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiAPI.Data.Repository
{
    public class RPerson : Generic, ICrud<MPerson>
    {
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MPerson>> GetAllAsync()
        {
            _parameters = new List<SqlParameter>();
            List<MPerson> person = new List<MPerson>();
            DataTable table = await SqlRead("SelectPersona");

            foreach (DataRow dr in table.Rows)
            {
                person.Add(new MPerson
                {
                    IdPerson = Convert.ToInt32(dr[0]),
                    Name = dr[1].ToString(),
                    LastName = dr[2].ToString(),
                    PhoneNumber = dr[3].ToString(),
                    Email = dr[4].ToString(),
                    City = dr[5].ToString(),
                    Address = dr[6].ToString()
                });
            }

            return person;
        }

        public async Task<MPerson> GetByIdAsync(int id)
        {
             _parameters = new List<SqlParameter>{
                 new SqlParameter("@Id", id)
             };
             MPerson person = null;
            DataTable table = await SqlRead("SelectPersonById");  
            if(table.Rows.Count>0){
                DataRow dr = table.Rows[0];
                person = new MPerson{
                    IdPerson = Convert.ToInt32(dr[0]),
                    Name = dr[1].ToString(),
                    LastName = dr[2].ToString(),
                    PhoneNumber = dr[3].ToString(),
                    Email = dr[4].ToString(),
                    City = dr[5].ToString(),
                    Address = dr[6].ToString()
                };
            }            
            return person;
        }

        public async Task<MPerson> SelectByName(string name)
        {
            _parameters = new List<SqlParameter>{
                 new SqlParameter("@Name", name)
             };
            MPerson person = null;
            DataTable table = await SqlRead("SelectPersonByName");
            if (table.Rows.Count > 0)
            {
                DataRow dr = table.Rows[0];
                person = new MPerson
                {
                    IdPerson = Convert.ToInt32(dr[0]),
                    Name = dr[1].ToString(),
                    LastName = dr[2].ToString(),
                    PhoneNumber = dr[3].ToString(),
                    Email = dr[4].ToString(),
                    City = dr[5].ToString(),
                    Address = dr[6].ToString()
                };
            }
            return person;
        }

        public async Task<bool> UpdateAsync(MPerson entity,string UserLogin)
        {
            _parameters = new List<SqlParameter>
            {
                new SqlParameter("@Login",UserLogin),
                new SqlParameter("@Nombre", entity.Name),
                new SqlParameter("@Apellido", entity.LastName),
                new SqlParameter("@Telefono",entity.PhoneNumber),
                new SqlParameter("@Correo",entity.Email),
                new SqlParameter("@Ciudad",entity.City),
                new SqlParameter("@Direccion",entity.Address)
            };

            return await SqlWrite("sp_UpdatePerson") > 0;
        }
    }
}
