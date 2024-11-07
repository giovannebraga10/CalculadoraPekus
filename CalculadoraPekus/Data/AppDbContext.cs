using Microsoft.EntityFrameworkCore;
using CalculadoraPekus.Models;
using System.Collections.Generic;

namespace CalculadoraPekus.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CalculadoraViewModel> Calculadora { get; set; }
    }
}
