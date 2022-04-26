namespace OrbitalWitnessAPI.Interfaces
{
    public interface IRepository<T, G>
    {
        IEnumerable<T> GetAll();
        T FindById(G id);
        IEnumerable<T> FindByCondition(Func<T, bool> expression);
        int Create(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
