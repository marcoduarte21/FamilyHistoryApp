using RegistroDeMatriculaDeCentroEducativo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace RegistroDeMatriculaDeCentroEducativo.BL
{
    internal interface IGestorDeLaMatricula
    {
        public void Registre(Model.Estudiante estudiante);
        public List<Model.Estudiante> ListeLosEstudiantes();
        public Model.Estudiante RetorneElEstudiantePorIdentificacion(string identificacion);
        public Model.Estudiante RetorneElEstudiantePorId(int id);
        public void Edite(int id,string cedula, string nombre, string primerApellido, string segundoApellido,
           Model.Sexo sexo, DateTime fechaDeNacimiento, string cedulaMadre, string cedulaPadre);
        List<Model.Estudiante> ListeLosHijos(string cedula);
        List<Model.Estudiante> ListeLosPadres(string cedula);
        List<Model.Estudiante> ListeLosHermanos(string cedula);
        List<Model.Estudiante> ListeLosAbuelos(string cedula);
        List<Model.Estudiante> ListeLosTios(string cedula);
        List<Model.Estudiante> ListeLosPrimos(string cedula);
        List<Model.Estudiante> ListeLosHombresRegistrados();
        List<Model.Estudiante> ListeLasMujeresRegistradas();
        public int RetorneLaEdad(Estudiante estudiante);
         
       


    }
}
