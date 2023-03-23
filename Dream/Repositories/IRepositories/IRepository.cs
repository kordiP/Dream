namespace Dream.Repositories.IRepositories
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
     /* --- ensureing that all repositories --- */
     /* --- implement the needed methods. --- */
      /* --- Its generic implementation --- */
       /* --- reduces code redundancy --- */

    public interface IRepository<T> where T : class
    {
        void Add(T model);
        void Delete(T model);
        void Update(T model);
        List<T> GetAll();
        T Get(int id);
        bool Exists(int id);
        void Save();

    }
}
