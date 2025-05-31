using NuGet.Protocol.Plugins;
using RegistroDeMatriculaDeCentroEducativo.BL.interfaces;
using RegistroDeMatriculaDeCentroEducativo.DA;
using RegistroDeMatriculaDeCentroEducativo.Model.Entities;
using RegistroDeMatriculaDeCentroEducativo.BL.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroDeMatriculaDeCentroEducativo.Exceptions;

namespace RegistroDeMatriculaDeCentroEducativo.BL.services
{
    public class FamilyHistoryService : IFamilyHistory
    {
        private DBContexto Connection;
        private StudentService? StudentService;

        public FamilyHistoryService(DBContexto connection) {
            Connection = connection;
        }
        public async Task<List<Estudiante>> GetChildren(string cedula)
        {
            Estudiante estudiantePadre = await StudentService.GetByCedulaAsync(cedula);
            if(estudiantePadre == null)
            {
                throw new CustomException("Student not Found", 404);
            }

            if (estudiantePadre.Sexo == Sexo.MASCULINO)
            {
                var InformacionDeLosHijos = from estudiante in Connection.Estudiantes
                                            where estudiante.CedulaPadre == estudiantePadre.Cedula
                                            select estudiante;


                return await InformacionDeLosHijos.ToListAsync();

            }
            else if (estudiantePadre.Sexo == Sexo.FEMENINO)
            {
                var InformacionDeLosHijos = from estudiante in Connection.Estudiantes
                                            where estudiante.CedulaMadre == estudiantePadre.Cedula
                                            select estudiante;


                return await InformacionDeLosHijos.ToListAsync();

            }

            return null;
        }

        public async Task<List<Estudiante>> GetCousins(string cedula)
        {
            var listaDeTios = await GetUncles(cedula);
            var lista = new List<Estudiante>();

            if (listaDeTios != null)
            {
                foreach (var tio in listaDeTios)
                {
                    foreach (var primo in Connection.Estudiantes)
                    {
                        if (tio.Cedula == primo.CedulaPadre || tio.Cedula == primo.CedulaMadre)
                        {
                            lista.Add(primo);
                        }
                    }
                }

                return lista.ToList();
            }

            return null;
        }

        public async Task<List<Estudiante>> GetGrandParents(string cedula)
        {
            Estudiante estudianteHijo;
            estudianteHijo = await StudentService.GetByCedulaAsync(cedula);
            string cedulaDelPadre = estudianteHijo.CedulaPadre;
            string cedulaDeLaMadre = estudianteHijo.CedulaMadre;

            Estudiante estudiantePadre = await StudentService.GetByCedulaAsync(cedulaDelPadre);
            Estudiante MadreDelEstudiante = await StudentService.GetByCedulaAsync(cedulaDeLaMadre);

            if (estudianteHijo == null)
            {
                throw new CustomException("student not found", 404);
            }
            if (estudiantePadre == null && MadreDelEstudiante == null)
            {
                throw new CustomException("parents not found.", 404);
            }

            if(estudiantePadre == null)
            {
                throw new CustomException("father not found.", 404);
            }

            if(MadreDelEstudiante == null)
            {
                throw new CustomException("mother not found.", 404);

            }


            if (MadreDelEstudiante != null && estudiantePadre == null)
            {
                var InformacionDeLosAbuelos = from estudiante in Connection.Estudiantes
                                              where estudiante.Cedula == MadreDelEstudiante.CedulaPadre ||
                                              estudiante.Cedula == MadreDelEstudiante.CedulaMadre
                                              select estudiante;
                return (List<Estudiante>)InformacionDeLosAbuelos.ToList();
            }

            if (estudiantePadre != null && MadreDelEstudiante == null)
            {

                var InformacionDeLosAbuelos = from estudiante in Connection.Estudiantes
                                              where estudiante.Cedula == estudiantePadre.CedulaPadre ||
                                              estudiante.Cedula == estudiantePadre.CedulaMadre
                                              select estudiante;
                return (List<Estudiante>)InformacionDeLosAbuelos.ToList();
            }

            if (estudiantePadre != null && MadreDelEstudiante != null)
            {
                var InformacionDeLosAbuelos = from estudiante in Connection.Estudiantes
                                              where estudiante.Cedula == MadreDelEstudiante.CedulaPadre ||
                                              estudiante.Cedula == MadreDelEstudiante.CedulaMadre ||
                                              estudiante.Cedula == estudiantePadre.CedulaPadre ||
                                              estudiante.Cedula == estudiantePadre.CedulaMadre
                                              select estudiante;
                return await InformacionDeLosAbuelos.ToListAsync() as List<Estudiante>;
            }

            return null;
        }

        public async Task<List<Estudiante>> GetParents(string cedula)
        {
            Estudiante estudianteHijo;
            estudianteHijo = await StudentService.GetByCedulaAsync(cedula);

            if (estudianteHijo != null)
            {
                throw new CustomException("Student not found.", 404);
            }

            var InformacionDelPadre = from estudiante in Connection.Estudiantes
                                      where estudiante.Cedula == estudianteHijo.CedulaPadre
                                      || estudiante.Cedula == estudianteHijo.CedulaMadre
                                      select estudiante;

            if (InformacionDelPadre.Count() > 0)
            {
                return (List<Estudiante>)InformacionDelPadre.ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Estudiante>> GetSiblings(string cedula)
        {
            Estudiante estudianteAConsultar;
            estudianteAConsultar = await StudentService.GetByCedulaAsync(cedula);
            if(estudianteAConsultar == null)
            {
                throw new CustomException("Student not found.", 404);
            }
            string cedulaDelPadre = estudianteAConsultar.CedulaPadre;
            string cedulaDeLaMadre = estudianteAConsultar.CedulaMadre;

            var listaDeHermanos = from estudiante in Connection.Estudiantes
                                  where estudiante.CedulaPadre == cedulaDelPadre && estudiante.CedulaMadre == cedulaDeLaMadre
                                  && estudiante.Cedula != cedula || estudiante.CedulaPadre == cedulaDelPadre && estudiante.Cedula != cedula
                                  || estudiante.CedulaMadre == cedulaDeLaMadre && estudiante.Cedula != cedula
                                  select estudiante;

            return await listaDeHermanos.ToListAsync();
        }

        public async Task<List<Estudiante>> GetUncles(string cedula)
        {
            Estudiante estudianteAConsultar;
            estudianteAConsultar = await StudentService.GetByCedulaAsync(cedula);
            string cedulaDelPadre = estudianteAConsultar.CedulaPadre;
            string cedulaDeLaMadre = estudianteAConsultar.CedulaMadre;

            Estudiante estudiantePadre;
            estudiantePadre = await StudentService.GetByCedulaAsync(cedulaDelPadre);
            string cedulaDelAbueloPaterno = null;
            if (estudiantePadre != null)
            {
                cedulaDelAbueloPaterno = estudiantePadre.CedulaPadre;
            }

            Estudiante MadreDelEstudiante;
            MadreDelEstudiante = await StudentService.GetByCedulaAsync(cedulaDeLaMadre);
            string cedulaDelAbueloMaterno = null;
            if (MadreDelEstudiante != null)
            {
                cedulaDelAbueloMaterno = MadreDelEstudiante.CedulaPadre;
            }

            if (cedulaDelAbueloPaterno != null && cedulaDelAbueloMaterno != null)
            {
                var listaDeTios = from estudiante in Connection.Estudiantes
                                  where estudiante.CedulaPadre == cedulaDelAbueloPaterno && estudiante.Cedula != cedulaDelPadre
                                  || estudiante.CedulaPadre == cedulaDelAbueloMaterno && estudiante.Cedula != cedulaDeLaMadre
                                  select estudiante;

                return (List<Estudiante>)listaDeTios.ToList();
            }

            if (cedulaDelAbueloPaterno != null && cedulaDelAbueloMaterno == null)
            {
                var listaDeTios = from estudiante in Connection.Estudiantes
                                  where estudiante.CedulaPadre == cedulaDelAbueloPaterno && estudiante.Cedula != cedulaDelPadre

                                  select estudiante;

                return (List<Estudiante>)listaDeTios.ToList();
            }

            if (cedulaDelAbueloPaterno == null && cedulaDelAbueloMaterno != null)
            {
                var listaDeTios = from estudiante in Connection.Estudiantes
                                  where estudiante.CedulaPadre == cedulaDelAbueloMaterno && estudiante.Cedula != cedulaDeLaMadre
                                  select estudiante;

                return (List<Estudiante>)listaDeTios.ToList();
            }
            return null;
        }
    }
}
