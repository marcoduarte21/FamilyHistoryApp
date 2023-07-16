namespace RegistroDeMatriculaDeCentroEducativo.Movil;

public partial class VistaEstudiantes : ContentPage
{
	public VistaEstudiantes()
	{
		InitializeComponent();
	}

    private void OnButton1Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VistaEstudiantesRegistrados());
    }

    private void OnButton2Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VistaHombresHombresRegistrados());
    }

    private void OnButton3Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VistaMujeresRegistradas());
    }
}