using RegistroDeMatriculaDeCentroEducativo.Model;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;

namespace RegistroDeMatriculaDeCentroEducativo.BL
{
    public class GestorDeLaMatricula : IGestorDeLaMatricula
    {

        private DA.DBContexto Connection;

        public GestorDeLaMatricula(DA.DBContexto connection)
        {
            Connection = connection;
        }

        public void Edite(int id, string cedula, string nombre, string primerApellido, string segundoApellido, Sexo sexo, DateTime fechaDeNacimiento, string cedulaMadre, string cedulaPadre)
        {
            Model.Estudiante EstudianteAModificar;
            EstudianteAModificar = RetorneElEstudiantePorId(id);

            EstudianteAModificar.Cedula = cedula;
            EstudianteAModificar.Nombre = nombre;
            EstudianteAModificar.PrimerApellido = primerApellido;
            EstudianteAModificar.SegundoApellido = segundoApellido;
            EstudianteAModificar.Sexo = sexo;
            EstudianteAModificar.FechaDeNacimiento = fechaDeNacimiento;
            EstudianteAModificar.Edad = RetorneLaEdad(EstudianteAModificar);
            EstudianteAModificar.CedulaPadre = cedulaPadre;
            EstudianteAModificar.CedulaMadre = cedulaMadre;
            Connection.Estudiantes.Update(EstudianteAModificar);
            Connection.SaveChanges();

        }

        public List<Estudiante> ListeLasMujeresRegistradas()
        {
           var listaDeMujeresRegistradas = from estudiante in Connection.Estudiantes
                                           where estudiante.Sexo == Sexo.FEMENINO
                                           select estudiante;

            return (List<Model.Estudiante>)listaDeMujeresRegistradas.ToList();

        }

        
        public List<Estudiante> ListeLosAbuelos(string cedula)
        {
            Model.Estudiante estudianteHijo;
            estudianteHijo = RetorneElEstudiantePorIdentificacion(cedula);
            string cedulaDelPadre = estudianteHijo.CedulaPadre;
            string cedulaDeLaMadre = estudianteHijo.CedulaMadre;

            Model.Estudiante estudiantePadre = RetorneElEstudiantePorIdentificacion(cedulaDelPadre);
            Model.Estudiante MadreDelEstudiante = RetorneElEstudiantePorIdentificacion(cedulaDeLaMadre);



            if (MadreDelEstudiante != null && estudiantePadre == null)
            {
                var InformacionDeLosAbuelos = from estudiante in Connection.Estudiantes
                                              where estudiante.Cedula == MadreDelEstudiante.CedulaPadre ||
                                              estudiante.Cedula == MadreDelEstudiante.CedulaMadre
                                              select estudiante;
                return (List<Model.Estudiante>)InformacionDeLosAbuelos.ToList();
            }
            
            if(estudiantePadre != null && MadreDelEstudiante == null){

                var InformacionDeLosAbuelos = from estudiante in Connection.Estudiantes
                                              where estudiante.Cedula == estudiantePadre.CedulaPadre ||
                                              estudiante.Cedula == estudiantePadre.CedulaMadre
                                              select estudiante;
                return (List<Model.Estudiante>)InformacionDeLosAbuelos.ToList();
            }

            if(estudiantePadre != null && MadreDelEstudiante != null)
            {
                var InformacionDeLosAbuelos = from estudiante in Connection.Estudiantes
                                              where estudiante.Cedula == MadreDelEstudiante.CedulaPadre ||
                                              estudiante.Cedula == MadreDelEstudiante.CedulaMadre ||
                                              estudiante.Cedula == estudiantePadre.CedulaPadre ||
                                              estudiante.Cedula == estudiantePadre.CedulaMadre
                                              select estudiante;
                return (List<Model.Estudiante>)InformacionDeLosAbuelos.ToList();
            }



            return null;
                    
        }

        public List<Estudiante> ListeLosEstudiantes()
        {
            
            return Connection.Estudiantes.ToList();
        }

        public List<Estudiante> ListeLosHermanos(string cedula)
        {
            Model.Estudiante estudianteAConsultar;
            estudianteAConsultar = RetorneElEstudiantePorIdentificacion(cedula);
            string cedulaDelPadre = estudianteAConsultar.CedulaPadre;
            string cedulaDeLaMadre = estudianteAConsultar.CedulaMadre;

            var listaDeHermanos = from estudiante in Connection.Estudiantes
                              where estudiante.CedulaPadre == cedulaDelPadre && estudiante.CedulaMadre == cedulaDeLaMadre 
                              && estudiante.Cedula != cedula || estudiante.CedulaPadre == cedulaDelPadre && estudiante.Cedula != cedula
                              || estudiante.CedulaMadre == cedulaDeLaMadre && estudiante.Cedula != cedula
                                  select estudiante;

            return (List<Model.Estudiante>)listaDeHermanos.ToList();
        }

        public List<Estudiante> ListeLosHijos(string cedula)
        {
            Model.Estudiante estudiantePadre = RetorneElEstudiantePorIdentificacion(cedula);
            if (estudiantePadre.Sexo == Sexo.MASCULINO)
            {
                var InformacionDeLosHijos = from estudiante in Connection.Estudiantes
                                            where estudiante.CedulaPadre == estudiantePadre.Cedula
                                            select estudiante;
                

                    return (List<Model.Estudiante>)InformacionDeLosHijos.ToList();
                
            }
            else if(estudiantePadre.Sexo == Sexo.FEMENINO)
            {
                var InformacionDeLosHijos = from estudiante in Connection.Estudiantes
                                            where estudiante.CedulaMadre == estudiantePadre.Cedula
                                            select estudiante;

                
                    return (List<Model.Estudiante>)InformacionDeLosHijos.ToList();
                
            }

            return null;
        }

            public List<Estudiante> ListeLosHombresRegistrados()
        {
            var listaHombresRegistrados = from estudiante in Connection.Estudiantes
                                            where estudiante.Sexo == Sexo.MASCULINO
                                            select estudiante;

            return (List<Model.Estudiante>)listaHombresRegistrados.ToList();

        }

        public List<Estudiante> ListeLosPadres(string cedula)
        {
            Model.Estudiante estudianteHijo;
            estudianteHijo = RetorneElEstudiantePorIdentificacion(cedula);
           var InformacionDelPadre = from estudiante in Connection.Estudiantes
                                  where estudiante.Cedula == estudianteHijo.CedulaPadre
                                  || estudiante.Cedula == estudianteHijo.CedulaMadre
                                  select estudiante;

            if (InformacionDelPadre.Count() > 0)
            {
                return (List<Model.Estudiante>)InformacionDelPadre.ToList();
            }
            else
            {
                return null;
            }
        }

        

        public List<Model.Estudiante> ListeLosPrimos(string cedula)
        {
            var listaDeTios = ListeLosTios(cedula);
            var lista = new List<Model.Estudiante>();

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
        public List<Estudiante> ListeLosTios(string cedula)
        {
            Model.Estudiante estudianteAConsultar;
            estudianteAConsultar = RetorneElEstudiantePorIdentificacion(cedula);
            string cedulaDelPadre = estudianteAConsultar.CedulaPadre;
            string cedulaDeLaMadre = estudianteAConsultar.CedulaMadre;

            Model.Estudiante estudiantePadre;
            estudiantePadre = RetorneElEstudiantePorIdentificacion(cedulaDelPadre);
            string cedulaDelAbueloPaterno = null;
            if (estudiantePadre != null)
            {
                cedulaDelAbueloPaterno = estudiantePadre.CedulaPadre;
            }

            Model.Estudiante MadreDelEstudiante;
            MadreDelEstudiante = RetorneElEstudiantePorIdentificacion(cedulaDeLaMadre);
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

                return (List<Model.Estudiante>)listaDeTios.ToList();
            }

            if (cedulaDelAbueloPaterno != null && cedulaDelAbueloMaterno == null)
            {
                var listaDeTios = from estudiante in Connection.Estudiantes
                                  where estudiante.CedulaPadre == cedulaDelAbueloPaterno && estudiante.Cedula != cedulaDelPadre
                                 
                                  select estudiante;

                return (List<Model.Estudiante>)listaDeTios.ToList();
            }

            if (cedulaDelAbueloPaterno == null && cedulaDelAbueloMaterno != null)
            {
                var listaDeTios = from estudiante in Connection.Estudiantes
                                  where estudiante.CedulaPadre == cedulaDelAbueloMaterno && estudiante.Cedula != cedulaDeLaMadre
                                  select estudiante;

                return (List<Model.Estudiante>)listaDeTios.ToList();
            }
            return null;
            }

        public void Registre(EstudianteParaIE estudiante)
        {
                    Model.Estudiante estudiante1 = new Estudiante();
                    estudiante1.Cedula = estudiante.Cedula;
                    estudiante1.CedulaMadre = estudiante.CedulaMadre;
                    estudiante1.CedulaPadre = estudiante.CedulaPadre;
            estudiante1.FechaDeNacimiento = estudiante.FechaDeNacimiento;
            estudiante1.Sexo = estudiante.Sexo;
            estudiante1.Nombre = estudiante.Nombre;
            estudiante1.PrimerApellido = estudiante.PrimerApellido;
            estudiante1.SegundoApellido = estudiante.SegundoApellido;


                    Connection.Estudiantes.Add(estudiante1);
                    Connection.SaveChanges();
                
        }

        public Estudiante RetorneElEstudiantePorIdentificacion(string identificacion)
        {
            foreach(var estudiante in Connection.Estudiantes)
            {
                if(estudiante.Cedula == identificacion)
                {
                    return estudiante;
                }
            }
            return null;
        }

        public Estudiante RetorneElEstudiantePorId(int id)
        {
            foreach (var estudiante in Connection.Estudiantes)
            {
                if (estudiante.Id == id)
                {
                    return estudiante;
                }
            }
            return null;
        }

        public int RetorneLaEdad(Estudiante estudiante)
        {

            int edad = 0;
            if(estudiante.FechaDeNacimiento.Value.Month > DateTime.Today.Month
          || estudiante.FechaDeNacimiento.Value.Month == DateTime.Today.Month &&
          estudiante.FechaDeNacimiento.Value.Day < DateTime.Today.Day)
            {
                edad = DateTime.Today.Year - estudiante.FechaDeNacimiento.Value.Year - 1;
                

            }
            else
            {
                edad = DateTime.Today.Year - estudiante.FechaDeNacimiento.Value.Year;
               
            }

            return edad;
        }

    }
}