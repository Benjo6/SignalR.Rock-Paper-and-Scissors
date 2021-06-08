using Microsoft.EntityFrameworkCore;
using RPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) 
        {

        }
        public DbSet<Game> Games { get; set; }
    }
}
