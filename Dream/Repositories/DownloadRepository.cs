using Dream.Data.Models;
using Dream.Repositories.IRepositories;
using System.Data;


namespace Dream.Repositories
{
    public class DownloadRepository : IDownloadRepository
    {
        private DreamContext context;
        public DownloadRepository()
        { 
            this.context = new DreamContext(); 
        }

        public void Add(Download download)
        {
            context.Downloads.Add(download);
        }

        public void Delete(int userId, int gameId)
        {
            Download download = context.Downloads.FirstOrDefault(x => x.UserId == userId && x.GameId == gameId);
            context.Downloads.Remove(download);
            Save();
        }

        public IEnumerable<Download> GetAll()
        {
            return context.Downloads.ToList();
        }

        public IEnumerable<Download> GetByGameId(int gameId)
        {
            return context.Downloads.Where(x => x.GameId == gameId).ToList();
        }

        public Download GetById(int userId, int gameId)
        {
            return context.Downloads.FirstOrDefault(x => x.UserId == userId && x.GameId == gameId);
        }

        public IEnumerable<Download> GetByUserId(int userId)
        {
            return context.Downloads.Where(x => x.UserId == userId).ToList();
        }

        public void Save()
        {
            context.SaveChanges();

        }
    }
}
