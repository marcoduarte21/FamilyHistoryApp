using Newtonsoft.Json;
using RegistroDeMatriculaDeCentroEducativo.Model;

namespace RegistroDeMatriculaDeCentroEducativo.Movil;

public partial class VistaMujeresRegistradas : ContentPage
{
	public VistaMujeresRegistradas()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var estudiante = await ObtengaLaLista();

        estudiantesListView.ItemsSource = estudiante;
    }

    private async Task<List<Estudiante>> ObtengaLaLista()
    {
        var httpClient = new HttpClient();

        var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/MujeresRegistradasAPI/GetDetallesMujeresRegistradas");
        string apiResponse = await respuesta.Content.ReadAsStringAsync();

        var estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(apiResponse);


        foreach (var item in estudiantes)
        {
            item.Edad = RetorneLaEdad(item);
        }

        return estudiantes;

       
    }
    public int RetorneLaEdad(Estudiante estudiante)
    {

        int edad = 0;
        if (estudiante.FechaDeNacimiento.Value.Month > DateTime.Today.Month
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