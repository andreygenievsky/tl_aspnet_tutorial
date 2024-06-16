namespace TodoRestApi.Data;

using Microsoft.EntityFrameworkCore;
using TodoRestApi.Models;

public class PersistenceContext : DbContext
{
    public PersistenceContext(DbContextOptions<PersistenceContext> opts) : base (opts)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}