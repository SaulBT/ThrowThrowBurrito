using Cliente.ServiciosGestionUsuarios;
using System;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Cliente.Ventanas.Perfil
{
    public partial class VntCambiarContrasenia : Page
    {
        private Jugador jugador = new Jugador();
        private ServicioCambiarContraseniaClient servicio;
        private bool operacionFueExitosa = false;
        public VntCambiarContrasenia(Jugador jugador)
        {
            InitializeComponent();
            servicio = new ServicioCambiarContraseniaClient();
            this.jugador = jugador;
        }

        private void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validarDatos())
                {
                    string contraseniaHash = encriptar(pwbContraseniaNueva.Password);
                    jugador.contrasenia = contraseniaHash;
                    if (servicio.CambiarContrasenia(contraseniaHash, jugador.claveUsuario))
                    {
                        operacionFueExitosa = true;
                        mostrarAlerta("La contraseña ha sido cambiada exitosamente.");
                    } 
                    else 
                    {
                        mostrarAlerta("Ha ocurrido un error al cambiar la contraseña.");
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
            Console.WriteLine(servicio.State.ToString());
            if (operacionFueExitosa)
            {
                NavigationService.GoBack();
            }
        }

        private void mostrarAlerta(string mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }

        private bool validarDatos()
        {
            bool contraseniaActualCoincide = ContraseniaActualCoincide(pwbContraseniaActual.Password);
            bool contraseniaNuevaEsValida = ContraseniaEsValida(pwbContraseniaNueva.Password);
            bool contraseniasSonIguales = ContraseniasSonIguales(pwbContraseniaActual.Password, pwbContraseniaNueva.Password);

            if (contraseniaActualCoincide && contraseniaNuevaEsValida && !contraseniasSonIguales)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContraseniasSonIguales(string contraseniaActual, string contraseniaNueva)
        {
            if (contraseniaActual == contraseniaNueva)
            {
                mostrarAlerta("La nueva contraseña no puede ser idéntica a la actual, ingrese una diferente.");
                return true;
            } 
            else
            {
                return false;
            }
        }
        public bool ContraseniaActualCoincide(string contrasenia)
        {
            if (jugador.contrasenia == encriptar(contrasenia))
            {
                tbcErrorContraseniaActual.Visibility = Visibility.Hidden;
                return true;
            } 
            else
            {
                tbcErrorContraseniaActual.Visibility = Visibility.Visible;
                return false;
            }
        }

        public bool ContraseniaEsValida(string contrasenia)
        {
            if ((contrasenia.Length >= 8) && (contrasenia.Length <= 100))
            {
                int contNumero = 0, contMayuscula = 0, contMinuscula = 0;
                foreach (char c in contrasenia)
                {
                    if (char.IsNumber(c))
                    {
                        contNumero++;
                    }
                    else if (char.IsUpper(c))
                    {
                        contMayuscula++;
                    }
                    else if (char.IsLower(c))
                    {
                        contMinuscula++;
                    }
                }
                if (contNumero > 0 && contMayuscula > 0 && contMinuscula > 0)
                {
                    tbcErrorContraseniaNueva.Visibility = Visibility.Hidden;
                    return true;
                }
                else
                {
                    tbcErrorContraseniaNueva.Visibility = Visibility.Visible;
                    return false;
                }
            }
            else
            {
                tbcErrorContraseniaNueva.Visibility = Visibility.Visible;
                return false;
            }
        }
        private string encriptar(string contrasenia)
        {
            SHA256Managed sHA256Managed = new SHA256Managed();
            string contraHash = String.Empty;
            byte[] contraByte = sHA256Managed.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));

            foreach (byte b in contraByte)
            {
                contraHash += b.ToString("x2");
            }

            return contraHash;
        }
    }
}
