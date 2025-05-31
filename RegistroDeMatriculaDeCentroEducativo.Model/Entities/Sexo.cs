using System.ComponentModel.DataAnnotations;

namespace RegistroDeMatriculaDeCentroEducativo.Model.Entities
{
    public enum Sexo
    {
        [Required(ErrorMessage = "El campo Sexo es requerido.")]
        MASCULINO = 1, FEMENINO = 2,
    }
}