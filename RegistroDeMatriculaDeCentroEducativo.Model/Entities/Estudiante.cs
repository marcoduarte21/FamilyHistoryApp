using System.Data.SqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using RegistroDeMatriculaDeCentroEducativo.Exceptions;

namespace RegistroDeMatriculaDeCentroEducativo.Model.Entities
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Cédula es requerido.")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo Primer Apellido es requerido.")]
        [Display(Name = "Primer Apellido")]
        public string? PrimerApellido { get; set; }
        [Required(ErrorMessage = "El campo Segundo Apellido es requerido.")]
        [Display(Name = "Segundo Apellido")]
        public string? SegundoApellido { get; set; }
        [Required(ErrorMessage = "El campo Sexo es requerido.")]
        public Sexo Sexo { get; set; }
        [Required(ErrorMessage = "El campo Fecha De Nacimiento es requerido.")]
        [Display(Name = "Fecha De Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaDeNacimiento { get; set; }
        [Required(ErrorMessage = "El campo Cédula de la Madre es requerido.")]
        [Display(Name = "Cédula de la Madre")]
        public string CedulaMadre { get; set; }
        [Required(ErrorMessage = "El campo Cédula del Padre es requerido.")]
        [Display(Name = "Cédula del Padre")]
        public string CedulaPadre { get; set; }

        [NotMapped]
        public int Edad
        {
            get
            {
                if (!FechaDeNacimiento.HasValue)
                {
                    throw new CustomException("Fecha de nacimiento es null.", 400);
                }

                var today = DateTime.Today;
                var fechaNacimiento = FechaDeNacimiento.Value;
                int edad = today.Year - fechaNacimiento.Year;

                if (fechaNacimiento.Month > today.Month ||
                    fechaNacimiento.Month == today.Month && fechaNacimiento.Day > today.Day)
                {
                    edad--;
                }

                return edad;
            }
        }


    }

}