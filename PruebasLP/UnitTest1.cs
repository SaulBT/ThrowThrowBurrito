using AccesoDatos;
using Servicios;

namespace PruebasLP
{
    [TestFixture]
    public class Tests
    {
        //no se prueba captura de excepciones, eso se maneja desde el cliente

        private ImplementacionRegistrarUsuario implementacion;
        private ModeloDBContainer contexto;

        [SetUp]
        public void SetUp()
        {
            contexto = new ModeloDBContainer();
            implementacion = new ImplementacionRegistrarUsuario(contexto);
            contexto.Database.BeginTransaction();
        }

        [TearDown]
        public void TearDown()
        {
            if (contexto.Database.CurrentTransaction != null)
            {
                contexto.Database.CurrentTransaction.Rollback();
            }
            contexto.Dispose();
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

            bool resultado = implementacion.RegistrarUsuario(usuario);

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