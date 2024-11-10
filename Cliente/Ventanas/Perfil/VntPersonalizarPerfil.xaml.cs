using System;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Cliente.ServiciosGestionUsuarios;

namespace Cliente.Ventanas.Perfil
{
    public partial class VntPersonalizarPerfil : Page
    {
        private Jugador jugador;
        private ServiciosGestionUsuarios.Perfil perfil = new ServiciosGestionUsuarios.Perfil();
        private ServicioPersonalizarPerfilClient servicio = new ServicioPersonalizarPerfilClient();
        private bool operacionExitosa = false;

        public VntPersonalizarPerfil(Jugador jugador)
        {
            InitializeComponent();
            this.jugador = jugador;
            cambiarPerfilJugador();
            txbDescripcion.Text = jugador.descripcion;
            txbNombreUsuario.Text = jugador.nombreUsuario;
            if (jugador.fotoPerfil != null)
                imgFotoPerfil.Source = ConvertirByteAImagen(jugador.fotoPerfil);
        }

        private void cambiarPerfilJugador()
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
            servicio.Close();
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
                    if (servicio.GuardarCambios(perfil, claveUsuario))
                    {
                        cambiarJugadorPerfil();
                        operacionExitosa = true;
                        mostrarAlerta("Cambios guardados con éxito");
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
                servicio.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
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
            Console.WriteLine(servicio.State.ToString());
            if (servicio.State == CommunicationState.Closed)
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
            if (operacionExitosa)
            {
                VntPerfil verPerfil = new VntPerfil(jugador);
                NavigationService.Navigate(verPerfil);
            }
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
                    ServicioRegistrarUsuarioClient servicio = new ServicioRegistrarUsuarioClient();
                    if (servicio.ValidarNombreNoRepetido(nombre))
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
