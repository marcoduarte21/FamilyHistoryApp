namespace RegistroDeMatriculaDeCentroEducativo.Movil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new VistaEstudiantes();
        }
    }
}