using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL;

public class MoveContext : DbContext
{
    private static string connectionString = "Server=localhost;Database=born2move;Uid=born2move_user;Pwd=IRo4HiMfdl!x2VWN;";

    public DbSet<Move> Move { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}

