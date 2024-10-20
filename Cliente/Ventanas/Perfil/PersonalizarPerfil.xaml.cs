using Cliente.ServicioPersonalizarPerfil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public partial class PersonalizarPerfil : Page
    {
        private string claveUsuario = null;
        private ServicioPersonalizarPerfil.Perfil perfil = null;

        public PersonalizarPerfil(string claveUsuario)
        {
            this.claveUsuario = claveUsuario;
            ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient Proxy = new ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient();
            perfil = Proxy.ObtenerPerfil(claveUsuario);


            InitializeComponent();
            tbDescripcion.Text = perfil.Descripcion;
            tbNombreUsuario.Text = perfil.NombreUsuario;
            if (perfil.Foto != null)
                imgFotoPerfil.Source = ConvertirByteAImagen(perfil.Foto);
        }

        // Implementación de botones

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient Proxy = new ServicioPersonalizarPerfil.ServicioPersonalizarPerfilClient();
            if (!string.IsNullOrEmpty(tbNombreUsuario.Text))
            {
                perfil.Descripcion = tbDescripcion.Text;
                perfil.NombreUsuario = tbNombreUsuario.Text;

                if (FotoEsValida(perfil.Foto))
                {
                    if (Proxy.GuardarCambios(perfil, claveUsuario))
                    {
                        Console.WriteLine("Cambios guardados con éxito");
                        VerPerfil verPerfil = new VerPerfil(claveUsuario);
                        NavigationService.Navigate(verPerfil);
                    }
                    else
                    {
                        Console.WriteLine("no se puedo guardar el cambio");
                    }
                }
                else
                {
                    Console.WriteLine("la foto no es valida, es muy grande");
                }

            }
            else
            {
                Console.WriteLine("Nombre de usuario vacío");
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
    }
}
