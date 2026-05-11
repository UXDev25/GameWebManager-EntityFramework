using Microsoft.EntityFrameworkCore;
using VideoGameManager.Models;

namespace VideoGameManagerEF.Data;

public class GameStoreContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Developer> Developers { get; set; }

    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options) { }
}
