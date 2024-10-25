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
        private Mock<ModeloDBContainer> contextoMock;

        [SetUp]
        public void SetUp()
        {
            implementacion = new ImplementacionRegistrarUsuario();
            contextoMock = new Mock<ModeloDBContainer>();
        }
        /*
        [Test]
        public void ProbarEnviarCodigo_SeEnviaCodigoCorrecto()
        {
            //esta función utiliza número aleatorios, no se puede predecir el código que se genera
        }
        */
        [Test]
        public void ProbarRegistrarUsuario()
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


        [Test]
        public void ProbarGenerarClaveUsuario_ClaveUnica()
        {
            string clave = implementacion.GenerarClaveUsuario();

            Assert.That(implementacion.ValidarClaveNoRepetida, Is.True);
        }

        [Test]
        public void ProbarValidarNombreNoRepetido_NombreExistente()
        {
            bool resultado = implementacion.ValidarNombreNoRepetido("Saul");

            Assert.That(resultado, Is.False);
        }

        [Test]
        public void ProbarValidarNombreNoRepetido_NombreInexistente()
        {
            bool resultado = implementacion.ValidarNombreNoRepetido("Nombre Inexistente");

            Assert.That(resultado, Is.False);
        }

        [Test]
        public void ProbarGenerarCodigo_Longitud()
        {
            int longitud = 6;
            string codigo = implementacion.GenerarCodigo(longitud);

            Assert.That(codigo.Length, Is.EqualTo(longitud));
        }

        [Test]
        public void ProbarGenerarCodigo_CaracteresValidos()
        {
            int longitud = 6;
            string codigo = implementacion.GenerarCodigo(longitud);

            StringAssert.IsMatch("^[A-Z0-9]+$", codigo);
        }

        [Test]
        public void ProbarValidarClaveNoRepetida_ClaveGenerada()
        {
            string clave = implementacion.GenerarCodigo(10);
            bool resultado = implementacion.ValidarClaveNoRepetida(clave);

            Assert.That(resultado, Is.True);
        }

        [Test]
        public void ProbarValidarClaveNoRepetida_ClaveExistente()
        {
            bool resultado = implementacion.ValidarClaveNoRepetida("V86ZS316IC");

            Assert.That(resultado, Is.False);
        }
    }
}
