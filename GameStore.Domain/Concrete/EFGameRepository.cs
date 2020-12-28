using System.Collections.Generic;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Concrete
{
    public class EFGameRepository : IGameRepository
    {
        EFDbContext _context = new EFDbContext();

        public IEnumerable<Game> Games
        {
            get { return _context.Games; }
        }
        public void SaveGame(Game game)
        {
            if (game.GameId == 0)
                _context.Games.Add(game);
            else
            {
                Game dbEntry = _context.Games.Find(game.GameId);
                if (dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.Description = game.Description;
                    dbEntry.Price = game.Price;
                    dbEntry.Category = game.Category;
                }
            }
            _context.SaveChanges();
        }
        public Game DeleteGame(int gameId)
        {
            Game dbEntry = _context.Games.Find(gameId);
            if (dbEntry != null)
            {
                _context.Games.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
