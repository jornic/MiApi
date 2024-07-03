namespace MiAPI.Data.Interfaz
{
    public interface ISelectByNameUser<Entity> where Entity : class
    {
        Task<Entity> SelectByName(string name);
    }
}
