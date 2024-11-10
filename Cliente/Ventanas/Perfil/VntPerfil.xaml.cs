using Cliente.ServiciosGestionUsuarios;
using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Cliente.Ventanas.Perfil
{
    /// <summary>
    /// Lógica de interacción para VerPerfil.xaml
    /// </summary>
    public partial class VntPerfil : Page
    {
        private Jugador jugador;

        public VntPerfil(Jugador jugador)
        {
            InitializeComponent();
            this.jugador = jugador;
            tbcDescripcion.Text = jugador.descripcion;
            tbcNombreUsuario.Text = jugador.nombreUsuario;
            if (jugador.fotoPerfil != null)
                imgFotoPerfil.Source = ConvertirByteAImagen(jugador.fotoPerfil);
        }

        private BitmapImage ConvertirByteAImagen(byte[] imageData)
        {
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            return bitmap;
        }

        private void btnPersonalizarPerfil_Click(object sender, RoutedEventArgs e)
        {
            VntPersonalizarPerfil personalizarPerfil = new VntPersonalizarPerfil(jugador);
            NavigationService.Navigate(personalizarPerfil);
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnCambiarContrasenia_Click(object sender, RoutedEventArgs e)
        {
            VntCambiarContrasenia vntCambiarContrasenia = new VntCambiarContrasenia(jugador);
            NavigationService.Navigate(vntCambiarContrasenia);
        }
    }
}
