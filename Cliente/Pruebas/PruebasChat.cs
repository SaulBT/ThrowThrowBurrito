using Cliente.Logica;
using Cliente.ServicioJuego;
using Cliente.ServicioLogin;
using Cliente.Ventanas;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace Cliente.Pruebas
{
    [TestFixture]
    public class PruebasChat : IServicioChatCallback
    {
        private string mensajeRetorno;
        private ServicioChatClient servicio;
        private ManualResetEvent esperar;
        private LogicaChat logica;
        private InstanceContext contexto;

        [SetUp]
        public void Setup()
        {
            contexto = new InstanceContext(this);
        }

        [Test]
        public void PruebaUnirse()
        {
            esperar = new ManualResetEvent(false);
            logica = new LogicaChat("CuentaPrueba10", contexto);
            logica.Unirse("CuentaPrueba10");
            String mensajeEsperado = String.Concat("CuentaPrueba10 se ha unido!");
            esperar.WaitOne();
            Assert.That(mensajeEsperado, Is.EqualTo(mensajeRetorno));
        }

        [Test]
        public void PruebaConexion()
        {
            servicio = new ServicioChatClient(contexto);
            servicio.Open();
            bool respuesta = servicio.ProbarConexion();
            Assert.That(respuesta, Is.EqualTo(true));
        }

        [Test]
        public void ProbarExcepcion()
        {
            servicio = new ServicioChatClient(contexto);
            servicio.Abort();
            Assert.Throws<CommunicationObjectAbortedException>(() => servicio.Unirse("CuentaPrueba10"));
        }

        public void RecibirMensaje(string mensajeCompleto)
        {
            mensajeRetorno = mensajeCompleto;
            Console.WriteLine("Si llegue");
            esperar.Set();
        }
    }
}
