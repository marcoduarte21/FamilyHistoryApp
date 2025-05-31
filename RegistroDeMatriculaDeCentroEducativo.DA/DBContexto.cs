using Microsoft.EntityFrameworkCore;
using RegistroDeMatriculaDeCentroEducativo.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace RegistroDeMatriculaDeCentroEducativo.DA
{
    public class DBContexto : DbContext
    {
        public DBContexto(DbContextOptions<DBContexto> options) : base(options)
        {
            
        }

        public DbSet<Estudiante> Estudiantes { get; set;}


    }


}