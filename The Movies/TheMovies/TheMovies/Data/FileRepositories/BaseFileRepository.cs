using System.Collections.ObjectModel;
using TheMovies.Data.Interfaces;

namespace TheMovies.Data.FileRepositories
{
    internal abstract class BaseFileRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly Datahandler<T> _dataHandler;
        protected ObservableCollection<T> _items;

        public ObservableCollection<T> Items => _items;

        protected BaseFileRepository(Datahandler<T> dataHandler)
        {
            _dataHandler = dataHandler;
            _items = new ObservableCollection<T>(_dataHandler.Load());
        }
        public void Add(T item)
        {
            _items.Add(item);
            SaveAll();
        }

        public void Delete(T item)
        {
            _items.Remove(item);
            SaveAll();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            _dataHandler.Save(_items);
        }

        public void Update(T item)
        {
            SaveAll();
        }
    }
}
