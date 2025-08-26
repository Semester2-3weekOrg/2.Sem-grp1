using System.Collections.ObjectModel;

namespace TheMovies.Data.Interfaces
{
    internal interface IBaseRepository<T> where T : class
    {
        ObservableCollection<T> Items { get; }
        List<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void SaveAll();

    }
}
