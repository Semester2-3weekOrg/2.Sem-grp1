using TheMovies.Data.Interfaces;

namespace TheMovies.Data.FileRepositories
{
    internal abstract class BaseFileRepository<T> : IBaseRepository<T> where T : class
    {
        protected BaseFileRepository(string filePath)
        {

        }
        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
