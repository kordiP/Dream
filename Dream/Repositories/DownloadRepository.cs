using Dream.Data.Models;
using Dream.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Dream.Repositories
{
    public class DownloadRepository : IRepository<Download>
    {
        private DreamContext context;
        public DownloadRepository(DreamContext context)
        { 
            this.context = context; 
        }

        public void Add(Download download)
        {
            context.Downloads.Add(download);
        }

        public void Delete(Download download)
        {
            context.Downloads.Remove(download);
            Save();
        }

        public List<Download> GetAll()
        {
            return context.Downloads.ToList();
        }

        public bool Exists(int id)
        {
            if (context.Downloads.Any(x => x.GameId == id)) return true;
            else if (context.Downloads.Any(x => x.UserId == id)) return true;
            else return false;
        }

        public Download Get(int id)
        {
            if (context.Downloads.Any(x => x.GameId == id))
                return context.Downloads.FirstOrDefault(x => x.GameId == id);
            else if (context.Downloads.Any(x => x.UserId == id))
                return context.Downloads.FirstOrDefault(x => x.UserId == id);
            else return null;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Download model)
        {
            context.Update(model).CurrentValues.SetValues(model);
            Save();
        }
    }
}
