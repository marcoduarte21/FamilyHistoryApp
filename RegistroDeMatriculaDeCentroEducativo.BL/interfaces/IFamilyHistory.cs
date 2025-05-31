using RegistroDeMatriculaDeCentroEducativo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDeMatriculaDeCentroEducativo.BL.interfaces
{
    public interface IFamilyHistory
    {
        Task<List<Estudiante>> GetChildren(string cedula);
        Task<List<Estudiante>> GetParents(string cedula);
        Task<List<Estudiante>> GetSiblings(string cedula);
        Task<List<Estudiante>> GetGrandParents(string cedula);
        Task<List<Estudiante>> GetUncles(string cedula);
        Task<List<Estudiante>> GetCousins(string cedula);
    }
}
