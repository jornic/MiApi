using Clase07.Data.Repository;
using MiAPI.Data.Interfaz;
using MiAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiAPI.Data.Repository
{
    public class RPerson : Generic, ICrud<MPerson>, IInsert<MPerson>
    {
        public Task<bool> CreateAsync(MPerson entity)
        {
            throw new NotImplementedException();
        }
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

        public Task<MPerson> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(MPerson entity)
        {
            throw new NotImplementedException();
        }
    }
}
