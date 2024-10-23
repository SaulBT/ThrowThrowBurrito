using Cliente.ServicioLogin;
using Cliente.ServicioPersonalizarPerfil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Ventanas.Perfil
{
    /// <summary>
    /// Lógica de interacción para PersonalizarPerfil.xaml
    /// </summary>
    public partial class VntPersonalizarPerfil : Page
    {
        private Jugador jugador;
        private ServicioPersonalizarPerfil.Perfil perfil = new ServicioPersonalizarPerfil.Perfil();
        private ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient proxy = new ServicioPersonalizarPerfilClient();

        public VntPersonalizarPerfil(Jugador jugador)
        {
            InitializeComponent();
            this.jugador = jugador;
            cambiarPefilJugador();
            txbDescripcion.Text = jugador.descripcion;
            txbNombreUsuario.Text = jugador.nombreUsuario;
            if (jugador.fotoPerfil != null)
                imgFotoPerfil.Source = ConvertirByteAImagen(jugador.fotoPerfil);

        }

        //POR LO MIENTRAS
        private void cambiarPefilJugador()
        {
            perfil.NombreUsuario = jugador.nombreUsuario;
            if (jugador.descripcion != null)
                perfil.Descripcion = "";
        }

        private void cambiarJugadorPerfil()
        {
            jugador.descripcion = perfil.Descripcion;
            jugador.nombreUsuario = perfil.NombreUsuario;
            jugador.fotoPerfil = perfil.Foto;
        }

        // Implementación de botones

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            proxy.Close();
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txbNombreUsuario.Text))
                {
                    perfil.Descripcion = txbDescripcion.Text;
                    perfil.NombreUsuario = txbNombreUsuario.Text;
                    string claveUsuario = jugador.claveUsuario;

                    if (FotoEsValida(perfil.Foto))
                    {
                        if (proxy.GuardarCambios(perfil, claveUsuario))
                        {
                            cambiarJugadorPerfil();
                            mostrarAlerta("Cambios guardados con éxito");
                            VntPerfil verPerfil = new VntPerfil(jugador);
                            NavigationService.Navigate(verPerfil);
                        }
                        else
                        {
                            mostrarAlerta("no se puedo guardar el cambio");
                        }
                    }
                    else
                    {
                        mostrarAlerta("la foto no es valida, es muy grande");
                    } 
                }
                else
                {
                    mostrarAlerta("Nombre de usuario vacío");
                }
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                proxy.Abort();
            } 
        }

        private void btnSubirFoto_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                imgFotoPerfil.Source = bitmap;
                perfil.Foto = convertirImagenAByte(bitmap);
            }
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
            Console.WriteLine(proxy.State.ToString());
            if (proxy.State == CommunicationState.Closed)
            {
                NavigationService.GoBack();
            }
        }

        // Funciones locales

        public bool FotoEsValida(byte[] foto)
        {
            const int mb = 1024 * 1024;
            if (foto == null)
            {
                return true;
            }

            if (foto.Length < 5 * mb)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private byte[] convertirImagenAByte(BitmapSource bitmapSource)
        {
            byte[] data;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
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

        private void mostrarAlerta(string mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }
    }
}
