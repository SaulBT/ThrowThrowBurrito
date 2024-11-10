using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Cliente.Ventanas
{
    public partial class VntConfiguracion : Page
    {
        //private Configuraciones configuraciones = new Configuraciones();

        public VntConfiguracion()
        {
            InitializeComponent();
            configurarControlesWPF();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionGeneral.VolumenGeneral = sldVolumen.Value;
            ConfiguracionGeneral.Musica = sldMusica.Value;
            ConfiguracionGeneral.Sonidos = sldSonido.Value;
            ConfiguracionGeneral.Sensibilidad = sldSensibilidad.Value;
            ConfiguracionGeneral.Brillo = sldBrillo.Value;
            if (rdbEspaniol.IsChecked == true)
            {
                ConfiguracionGeneral.Idioma = rdbEspaniol.Content.ToString();
            }
            else if (rdbEnglish.IsChecked == true)
            {
                ConfiguracionGeneral.Idioma = rdbEnglish.Content.ToString();
            }
            guardarConfiguraciones();
            NavigationService.GoBack();
        }

        private void guardarConfiguraciones()
        {
            using (StreamWriter writer = new StreamWriter(ConfiguracionGeneral.nombreArchivoConfiguraciones))
            {
                writer.WriteLine("Volumen:" + ConfiguracionGeneral.VolumenGeneral);
                writer.WriteLine("Musica:" + ConfiguracionGeneral.Musica);
                writer.WriteLine("Sonidos:" + ConfiguracionGeneral.Sonidos);
                writer.WriteLine("Sensibilidad:" + ConfiguracionGeneral.Sensibilidad);
                writer.WriteLine("Brillo:" + ConfiguracionGeneral.Brillo);
                writer.WriteLine("Idioma:" + ConfiguracionGeneral.Idioma);
            }
        }


        private void configurarControlesWPF()
        {
            sldVolumen.Value = ConfiguracionGeneral.VolumenGeneral;
            sldMusica.Value = ConfiguracionGeneral.Musica;
            sldSonido.Value = ConfiguracionGeneral.Sonidos;
            sldSensibilidad.Value = ConfiguracionGeneral.Sensibilidad;
            sldBrillo.Value = ConfiguracionGeneral.Brillo;
            if (ConfiguracionGeneral.Idioma == "Español")
            {
                rdbEspaniol.IsChecked = true;
            }
            else if (ConfiguracionGeneral.Idioma == "English")
            {
                rdbEnglish.IsChecked = true;
            }
        }

        
    }
}
