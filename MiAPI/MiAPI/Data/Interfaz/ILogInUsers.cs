using MiAPI.Models;

namespace MiAPI.Data.Interfaz
{
    public interface ILogInUsers
    {
        public Task<bool> VerificarUsuario(MLogin login);
    }
}
