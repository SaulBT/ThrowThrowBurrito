using System;
using System.IO;

namespace Cliente
{
    public class ConfiguracionGeneral
    {
        public const string nombreArchivoConfiguraciones = "ConfiguracionGeneral.txt";

        public static double VolumenGeneral { get; set; } = 1.0;
        public static double Musica { get; set; } = 0.6;
        public static double Sonidos { get; set; } = 1.0;
        public static double Sensibilidad { get; set; } = 1.0;
        public static double Brillo { get; set; } = 1.0;
        public static string Idioma { get; set; } = "Español";

        public static void cargarConfiguraciones()
        {
            using (StreamReader reader = new StreamReader(nombreArchivoConfiguraciones))
            {
                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    if (linea.StartsWith("Volumen:"))
                    {
                       VolumenGeneral = Double.Parse(linea.Substring("Volumen:".Length));
                    }
                    else if (linea.StartsWith("Musica:"))
                    {
                        Musica = Double.Parse(linea.Substring("Musica:".Length));
                    }
                    else if (linea.StartsWith("Sonidos:"))
                    {
                        Sonidos = Double.Parse(linea.Substring("Sonidos:".Length));
                    }
                    else if (linea.StartsWith("Sensibilidad:"))
                    {
                        Sensibilidad = Double.Parse(linea.Substring("Sensibilidad:".Length));
                    }
                    else if (linea.StartsWith("Brillo:"))
                    {
                        Brillo = Double.Parse(linea.Substring("Brillo:".Length));
                    }
                    else if (linea.StartsWith("Idioma:"))
                    {
                        Idioma = linea.Substring("Idioma:".Length);
                    }
                }
            }
        }


    }
}
