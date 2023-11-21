#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace Tarea_Inicio_de_Sesión_y_Registro.Models;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
}