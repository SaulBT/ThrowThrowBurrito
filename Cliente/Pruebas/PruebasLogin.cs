using Cliente.Logica;
using Cliente.ServiciosGestionUsuarios;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Pruebas
{
    [TestFixture]
    internal class PruebasLogin
    {
        private LogicaLogin logica;
        [SetUp]
        public void Setup()
        {
            logica = new LogicaLogin();
        }

        [Test]
        public void ProbarLoginCorrecto()
        {
            string nombreUsuario = "CuentaPrueba10";
            string contrasenia = "ContraPrueba1";

            Jugador resultado = logica.IniciarSesion(nombreUsuario, contrasenia);
            //Console.WriteLine(resultado.nombreUsuario);

            Assert.That(nombreUsuario, Is.EqualTo(resultado.nombreUsuario));
        }

        [Test]
        public void ProbarLoginIncorrecto()
        {
            string nombreUsuario = "UsuarioNuevo10";
            string contrasenia = "passwordTst23";

            Jugador resultado = logica.IniciarSesion(nombreUsuario, contrasenia);

            Assert.That(null, Is.EqualTo(resultado));
        }

        [Test]
        public void ProbarExcepcion()
        {
            string nombreUsuario = "CuentaPrueba10";
            string contrasenia = "ContraPrueba1";
            logica.servicio.Abort();
            Assert.Throws<CommunicationObjectAbortedException>(() => logica.IniciarSesion(nombreUsuario, contrasenia));
        }
    }
}
