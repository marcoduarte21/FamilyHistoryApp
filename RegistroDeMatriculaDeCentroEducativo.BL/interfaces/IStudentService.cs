using RegistroDeMatriculaDeCentroEducativo.Model.DTOs;
using RegistroDeMatriculaDeCentroEducativo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace RegistroDeMatriculaDeCentroEducativo.BL.interfaces
{
    public interface IStudentService
    {
        Task<List<Estudiante>> GetAllAsync();
        Task<List<Estudiante>> GetAllMen();
        Task<List<Estudiante>> GetAllWomen();
        Task<Estudiante> CreateAsync(EstudianteDTO estudiante);
        Task<Estudiante> UpdateAsync(EstudianteDTO estudiante);
        Task<Estudiante> DeleteAsync(EstudianteDTO estudiante);
        Task<Estudiante> GetByIdAsync (int id);
        Task<Estudiante> GetByCedulaAsync(string cedula);

    }
}
