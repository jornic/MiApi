namespace MiAPI.Data.Interfaz
{
    public interface ICrud<Entity> where Entity : class
    {   
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(int id);
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
    }
}
