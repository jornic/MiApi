namespace MiAPI.Data.Interfaz
{
    public interface IInsert<Entity> where Entity : class
    {
        Task<bool> CreateAsync(Entity entity);
    }
}
