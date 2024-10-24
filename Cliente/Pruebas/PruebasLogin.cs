using Cliente.ServicioLogin;
using Cliente.Ventanas;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Pruebas
{
    [TestFixture]
    internal class PruebasLogin
    {
        [SetUp]
        public void Setup()
        {
            vntLogin vntLogin = new vntLogin();
        }

        [Test]
        public void ProbarLoginCorrecto()
        {
            string nombreUsuario = "ContraPrueba1";
            string contrasenia = "CuentaPrueba10";

            Jugador resultado = vntLogin
        }
    }
}
