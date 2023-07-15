using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RegistroDeMatriculaDeCentroEducativo.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {
            
        }

        public DbSet<Model.Estudiante> Estudiantes { get; set;}


    }


}