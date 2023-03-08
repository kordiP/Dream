using Dream.Data.Models;

namespace Data.Models
{
    public class GameDeveloper
    {
        public int GameId { get; set; }
        public int DeveloperId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Developer Developer { get; set; }
    }
}
