using RegistroDeMatriculaDeCentroEducativo.BL.interfaces;
using RegistroDeMatriculaDeCentroEducativo.Model.DTOs;
using RegistroDeMatriculaDeCentroEducativo.Model.Entities;
using RegistroDeMatriculaDeCentroEducativo.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;

namespace RegistroDeMatriculaDeCentroEducativo.BL.services
{
    public class StudentService : IStudentService
    {

        private DA.DBContexto Connection;

        public StudentService(DA.DBContexto connection)
        {
            Connection = connection;
        }


        public async Task<List<Estudiante>> GetAllWomen()
        {
           var listaDeMujeresRegistradas = from estudiante in Connection.Estudiantes
                                           where estudiante.Sexo == Sexo.FEMENINO   
                                           select estudiante;

            return await listaDeMujeresRegistradas.ToListAsync() as List<Estudiante>;

        }

        public async Task<List<Estudiante>> GetAllAsync()
        {
            
            return await Connection.Estudiantes.ToListAsync();
        }

        public async Task<List<Estudiante>> GetAllMen()
        {
            var listaHombresRegistrados =from estudiante in Connection.Estudiantes
                                            where estudiante.Sexo == Sexo.MASCULINO
                                            select estudiante;

            return await listaHombresRegistrados.ToListAsync() as List<Estudiante>;

        }

        public async Task<Estudiante> CreateAsync(EstudianteDTO estudiante)
        {
            Estudiante existingStudent = await GetByCedulaAsync(estudiante.Cedula);

            if (existingStudent != null)
            {
                throw new CustomException("student already exists.", 404);
            }

            Estudiante newStudent = new Estudiante();
                newStudent.Id = estudiante.Id;
                newStudent.Cedula = estudiante.Cedula;
                newStudent.CedulaMadre = estudiante.CedulaMadre;
                newStudent.CedulaPadre = estudiante.CedulaPadre;
                newStudent.FechaDeNacimiento = estudiante.FechaDeNacimiento;
                newStudent.Sexo = estudiante.Sexo;
                newStudent.Nombre = estudiante.Nombre;
                newStudent.PrimerApellido = estudiante.PrimerApellido;
                newStudent.SegundoApellido = estudiante.SegundoApellido;

                await Connection.Estudiantes.AddAsync(newStudent);
                await Connection.SaveChangesAsync();

                return newStudent;
        }

        public async Task<Estudiante> GetByCedulaAsync(string cedula)
        {
            var estudiante = await Connection.Estudiantes.FirstOrDefaultAsync(x => x.Cedula == cedula);
            if (estudiante == null) throw new CustomException("Student Not Found", 404);

            return estudiante;
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            var estudiante = await Connection.Estudiantes.FindAsync(id);

            if(estudiante == null) throw new CustomException("Student Not Found :(.", 404);
            
            return estudiante;
        }

        public async Task<Estudiante> UpdateAsync(EstudianteDTO estudiante)
        {
                Estudiante StudentToUpdate;
                StudentToUpdate = await GetByCedulaAsync(estudiante.Cedula);

            if(StudentToUpdate == null)
            {
                throw new CustomException("student not found to update", 404);
            }

            if (StudentToUpdate != null && StudentToUpdate.Id != estudiante.Id)
            {
                throw new CustomException("cedula already exists", 404);
            }

                StudentToUpdate.Cedula = estudiante.Cedula;
                StudentToUpdate.Nombre = estudiante.Nombre;
                StudentToUpdate.PrimerApellido = estudiante.PrimerApellido;
                StudentToUpdate.SegundoApellido = estudiante.SegundoApellido;
                StudentToUpdate.Sexo = estudiante.Sexo;
                StudentToUpdate.FechaDeNacimiento = estudiante.FechaDeNacimiento;
                StudentToUpdate.CedulaPadre = estudiante.CedulaPadre;
                StudentToUpdate.CedulaMadre = estudiante.CedulaMadre;
                Connection.Estudiantes.Update(StudentToUpdate);
                await Connection.SaveChangesAsync();
                
                return StudentToUpdate;
                
        }

        public Task<Estudiante> DeleteAsync(EstudianteDTO estudiante)
        {
            throw new NotImplementedException();
        }
    }
}