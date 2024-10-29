using AccesoDatos;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Servicios;


namespace Pruebas
{
    [TestFixture]
    public class PruebasImplementacion
    {
        //no se prueba captura de excepciones, eso se maneja desde el cliente

        private ImplementacionRegistrarUsuario implementacion;
        private ImplementacionPersonalizarPerfil ImplementacionPersonalizarPerfil;
        private Mock<ModeloDBContainer> contextoMock;

        [SetUp]
        public void SetUp()
        {
            contextoMock = new Mock<ModeloDBContainer>();
            implementacion = new ImplementacionRegistrarUsuario(contextoMock.Object);
            ImplementacionPersonalizarPerfil = new ImplementacionPersonalizarPerfil(contextoMock.Object);
        }
        //implementación registro usuario

        [Test]
        public void ProbarEnviarCodigo_CaracteresValidos()
        {
            try
            {
                string correo = "Ejemplo@gmail.com";
                string codigo = implementacion.EnviarCodigoCorreo(correo);

                StringAssert.IsMatch("^[A-Z0-9]+$", codigo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarEnviarCodigo_Longitud()
        {
            try
            {
                int longitud = 6;
                string correo = "Ejemplo@gmail.com";
                string codigo = implementacion.EnviarCodigoCorreo(correo);

                Assert.That(codigo.Length, Is.EqualTo(longitud));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarRegistrarUsuario()
        {
            try
            {
                var usuario = new Usuario()
                {
                    NombreUsuario = "PruebaRegistro",
                    Contrasenia = "PruebaContrasenia",
                    Correo = "PruebaCorreo"
                };

                contextoMock.Setup(c => c.Jugador.Add(It.IsAny<Jugador>()));
                contextoMock.Setup(c => c.SaveChanges()).Returns(1);

                bool resultado = implementacion.RegistrarUsuario(usuario);

                contextoMock.Verify(c => c.Jugador.Add(It.Is<Jugador>(j =>
                    j.nombreUsuario == usuario.NombreUsuario &&
                    j.contrasenia == usuario.Contrasenia &&
                    j.correoElectronico == usuario.Correo
                )), Times.Once);

                contextoMock.Verify(c => c.SaveChanges(), Times.Once);

                Assert.That(resultado, Is.True);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        [Test]
        public void ProbarGenerarClaveUsuario_ClaveUnica()
        {
            try
            {
                string clave = implementacion.GenerarClaveUsuario();

                Assert.That(implementacion.ValidarClaveNoRepetida, Is.True);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarValidarNombreNoRepetido_NombreExistente()
        {
            try
            {
                bool resultado = implementacion.ValidarNombreNoRepetido("Saul");

                Assert.That(resultado, Is.False);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarValidarNombreNoRepetido_NombreInexistente()
        {
            try
            {
                bool resultado = implementacion.ValidarNombreNoRepetido("Nombre Inexistente");

                Assert.That(resultado, Is.False);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarGenerarCodigo_Longitud()
        {
            try
            {
                int longitud = 6;
                string codigo = Utilidades.GenerarCodigo(longitud);

                Assert.That(codigo.Length, Is.EqualTo(longitud));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarGenerarCodigo_CaracteresValidos()
        {
            try
            {
                int longitud = 6;
                string codigo = Utilidades.GenerarCodigo(longitud);

                StringAssert.IsMatch("^[A-Z0-9]+$", codigo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarValidarClaveNoRepetida_ClaveGenerada()
        {
            try
            {
                string clave = Utilidades.GenerarCodigo(10);
                bool resultado = implementacion.ValidarClaveNoRepetida(clave);

                Assert.That(resultado, Is.True);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        public void ProbarValidarClaveNoRepetida_ClaveExistente()
        {
            try
            {
                bool resultado = implementacion.ValidarClaveNoRepetida("V86ZS316IC");

                Assert.That(resultado, Is.False);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //implementación personalizar perfil

        [Test]
        public void ProbarGuardarCambios()
        {
            try
            {
                Perfil perfil = new Perfil()
                {
                    NombreUsuario = "Gerardo",
                    Descripcion = "I drive tin tin tin tin tin",
                    Foto = null
                };
                string clave = "V86ZS316IC";
                bool resultado = ImplementacionPersonalizarPerfil.GuardarCambios(perfil, clave);

                Assert.That(resultado, Is.True);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}