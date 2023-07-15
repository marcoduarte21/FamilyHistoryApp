using System.ComponentModel.DataAnnotations;

namespace RegistroDeMatriculaDeCentroEducativo.Model
{
    public enum Sexo
    {
        [Required(ErrorMessage = "El campo Sexo es requerido.")]
        MASCULINO = 1, FEMENINO = 2,
    }
}