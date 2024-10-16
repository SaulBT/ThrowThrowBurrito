using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Clases
{
    public class ConfiguracionGeneral
    {
        private int volumen;
        private int volumenMusica;
        private int volumenSonidos;
        private int brillo;
        private int sensibilidad;
        private String idioma;

        public ConfiguracionGeneral()
        {

        }

        public ConfiguracionGeneral(int volumen, int volumenMusica, int volumenSonidos, int brillo, int sensibilidad, string idioma)
        {
            this.volumen = volumen;
            this.volumenMusica = volumenMusica;
            this.volumenSonidos = volumenSonidos;
            this.brillo = brillo;
            this.sensibilidad = sensibilidad;
            this.idioma = idioma;
        }

        public int Volumen
        {
            get { return volumen; }
            set { volumen = value; }
        }

        public int VolumenMusica
        {
            get { return volumenMusica; }
            set { volumenMusica = value; }
        }

        public int VolumenSonidos
        {
            get { return volumenSonidos; }
            set { volumenSonidos = value; }
        }

        public int Brillo
        {
            get { return brillo; }
            set { brillo = value; }
        }

        public int Sensibilidad
        {
            get { return sensibilidad; }
            set { sensibilidad = value; }
        }

        public String Idioma
        {
            get { return idioma; }
            set { idioma = value; }
        }
    }
}
