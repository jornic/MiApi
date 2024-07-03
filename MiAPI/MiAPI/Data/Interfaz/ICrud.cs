namespace MiAPI.Data.Interfaz
{
    public interface ICrud<Entity> where Entity : class
    {   
        Task<bool> UpdateAsync(Entity entity,string userid);
        Task<Entity> SelectByName(string name);
        Task<bool> DeleteAsync(int id);
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
    }
}
