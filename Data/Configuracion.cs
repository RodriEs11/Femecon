
namespace Data
{
    public class Configuracion
    {



        public static Configuracion instancia;
        public static string versionOnline { get; set; }
        public static string versionLocal { get; set; }

        public static string notasLastRelease { get; set; }

        public static bool actualizado { get; set; }

        public static bool mostrarNavegadorChromeDriver { get; set; }


        public static Configuracion getInstance()
        {
            if (instancia == null)
            {
                instancia = new Configuracion();
            }

            return instancia;
        }


        public Configuracion()
        {
            //versionOnline = GithubApi.obtenerUltimaVersion();
            //notasLastRelease = GithubApi.obtenerNotasLastRelease();
            versionLocal = Constantes.VERSION_LOCAL;
            //actualizado = false || versionLocal.Equals(versionOnline);

            mostrarNavegadorChromeDriver = false;


        }

        public void setMostrarNavegadorChromeDriver(bool mostrar) {

            mostrarNavegadorChromeDriver = mostrar;
        }

        public bool getMostrarNavegadorChromeDriver( )
        {

            return mostrarNavegadorChromeDriver;
        }

    }
}
