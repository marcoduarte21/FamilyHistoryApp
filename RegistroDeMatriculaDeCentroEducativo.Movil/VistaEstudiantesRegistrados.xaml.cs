using Newtonsoft.Json;
using RegistroDeMatriculaDeCentroEducativo.Model;

namespace RegistroDeMatriculaDeCentroEducativo.Movil;

public partial class VistaEstudiantesRegistrados : ContentPage
{
	public VistaEstudiantesRegistrados()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var estudiante = await ObtengaLaLista();

        inventarioListView.ItemsSource = estudiante;
    }

    private async Task<List<Estudiante>> ObtengaLaLista()
    {
        var httpClient = new HttpClient();

        var respuesta = await httpClient.GetAsync("-----------------");
        string apiResponse = await respuesta.Content.ReadAsStringAsync();

        var inventarios = JsonConvert.DeserializeObject<List<Estudiante>>(apiResponse);

        return inventarios;
    }
}