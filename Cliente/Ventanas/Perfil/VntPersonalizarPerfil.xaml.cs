using Cliente.ServicioLogin;
using Cliente.ServicioPersonalizarPerfil;
using Cliente.ServicioRegistrarUsuario;
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
            perfil.Descripcion = jugador.descripcion;
            if (jugador.fotoPerfil != null)
                perfil.Foto = jugador.fotoPerfil;
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
                if (validarDatos())
                {
                    perfil.Descripcion = txbDescripcion.Text;
                    perfil.NombreUsuario = txbNombreUsuario.Text;
                    string claveUsuario = jugador.claveUsuario;
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
                mostrarAlerta("La imagen supera el tamaño máximo.");
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

        private bool validarDatos()
        {
            if (!string.IsNullOrEmpty(txbNombreUsuario.Text))
            {
                if (NombreEsValido(txbNombreUsuario.Text))
                {
                    if (FotoEsValida(perfil.Foto))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                mostrarAlerta("El nombre de usuario no puede quedarse vacío.");
                return false;
            }
        }

        public bool NombreEsValido(string nombre)
        {
            if (!nombre.Equals(perfil.NombreUsuario))
            {
                if ((nombre.Length >= 3) && (nombre.Length <= 20))
                {
                    foreach (char c in nombre)
                    {
                        if (!char.IsLetterOrDigit(c) && c != '-' && c != '.')
                        {
                            mostrarAlerta("Formato inválido del nombre de usuario.");
                            return false;
                        }
                    }
                    ServicioRegistrarUsuarioClient proxy = new ServicioRegistrarUsuarioClient();
                    if (proxy.ValidarNombreNoRepetido(nombre))
                    {
                        return true;
                    }
                    mostrarAlerta("El nombre de usuario ingresado ya está ocupado.");
                    return false;
                }
                else
                {
                    mostrarAlerta("Formato inválido del nombre de usuario.");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
