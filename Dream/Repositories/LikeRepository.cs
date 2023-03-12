using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private DreamContext context;
        public LikeRepository()
        { 
            this.context = new DreamContext(); 
        }

        public void Add(Like like)
        {
            context.Likes.Add(like);
            Save();
        }

        public void Delete(int userId, int gameId)
        {
            Like like = context.Likes.FirstOrDefault(x => x.UserId == userId && x.GameId == gameId);
            context.Likes.Remove(like);
            Save();
        }

        public IEnumerable<Like> GetAll()
        {
            return context.Likes.ToList();
        }

        public IEnumerable<Like> GetByGameId(int gameId)
        {
            return context.Likes.Where(x => x.GameId == gameId).ToList();
        }

        public Like GetById(int userId, int gameId)
        {
            return context.Likes.FirstOrDefault(x => x.UserId == userId && x.GameId == gameId);
        }

        public IEnumerable<Like> GetByUserId(int userId)
        {
            return context.Likes.Where(x => x.UserId == userId).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
