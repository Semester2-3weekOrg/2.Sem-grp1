namespace TheMovies.Data.Interfaces
{
    internal interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void SaveAll();

    }
}
