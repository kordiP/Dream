using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class LikeRepository : IRepository<Like>
    {
        private DreamContext context;
        public LikeRepository(DreamContext context)
        {
            this.context = context;
        }

        public void Add(Like like)
        {
            context.Likes.Add(like);
        }

        public void Delete(Like like)
        {
            context.Likes.Remove(like);
            Save();
        }

        public bool Exists(int id)
        {
            if (context.Likes.Any(x => x.GameId == id)) return true;
            else if (context.Likes.Any(x => x.UserId == id)) return true;
            else return false;
        }

        public Like Get(int id)
        {
            if (context.Likes.Any(x => x.GameId == id))
                return context.Likes.FirstOrDefault(x => x.GameId == id);
            else if (context.Likes.Any(x => x.UserId == id))
                return context.Likes.FirstOrDefault(x => x.UserId == id);
            else return null;
        }

        public List<Like> GetAll()
        {
            return context.Likes.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Like model)
        {
            context.Update(model).CurrentValues.SetValues(model);
            Save();
        }
    }
}
