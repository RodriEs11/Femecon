using Data;
using HtmlAgilityPack;
using System;

namespace Femecon
{
    class ConexionIoma
    {
        private HtmlDocument document;

        public ConexionIoma(string dni, char sexo)
        {
            this.document = obtenerHtmlIoma(dni, sexo);

        }

        public static Paciente obtenerDatosPaciente(string dni, char sexo)
        {

            try
            {

                ConexionIoma conexionIoma = new ConexionIoma(dni, sexo);

                string nombre = conexionIoma.obtenerNombre();
                string apellido = conexionIoma.obtenerApellido();
                string numeroAfiliado = conexionIoma.obtenerNumeroAfiliado();
                string epo = conexionIoma.obtenerEpo();
                bool activo = conexionIoma.isAfiliadoActivo();

                Paciente pacienteTemp = new Paciente(nombre, apellido, numeroAfiliado, epo, activo);

                Paciente paciente = Paciente.getInstance();

                paciente.nombre = pacienteTemp.nombre;
                paciente.apellido = pacienteTemp.apellido;
                paciente.numeroAfiliado = pacienteTemp.numeroAfiliado;
                paciente.clinica = pacienteTemp.clinica;
                paciente.epo = pacienteTemp.epo;
                paciente.esFemecon = pacienteTemp.esFemecon;
                paciente.activo = pacienteTemp.activo;
                paciente.sexo = sexo;
                paciente.dni = dni;

                return paciente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }

        }

        private HtmlDocument obtenerHtmlIoma(string dni, char sexo)
        {

            HtmlWeb web = new HtmlWeb();


            string url = Rutas.URL_IOMA + dni + sexo;

            try
            {

                HtmlDocument document = web.Load(url);

                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//table");
                nodes.ToString(); // LANZA LA EXCEPCION SI HACE REFERENCIA A UN NULL, ES QUE NO EXISTE EL PACIENTe


                return document;

            }
            catch (System.Net.WebException)
            {

                throw new Exception("No se ha podido conectar con los servicios de IOMA");
            }
            catch (NullReferenceException)
            {

                throw new Exception("No existe ningún paciente con ese DNI");

            }

        }
        public bool isAfiliadoActivo()
        {
            bool activo = true;

            if (document.ParsedText.Contains("El afiliado no se encuentra activo en el padrón de afiliados."))
            {
                activo = false;
            }

            return activo;
        }

        public string obtenerApellido()
        {


            string xPathApellido = "/html[1]/body[1]/table[1]/tr[2]/td[1]";

            string apellido = obtenerDatoPorxPath(xPathApellido);

            return apellido;
        }

        public string obtenerNombre()
        {


            string xPathNombre = "/html[1]/body[1]/table[1]/tr[2]/td[2]";

            string nombre = obtenerDatoPorxPath(xPathNombre);

            return nombre;
        }

        public string obtenerNumeroAfiliado()
        {


            string xPathNumAfiliado = "/html[1]/body[1]/table[1]/tr[2]/td[4]";

            string numeroAfiliado = obtenerDatoPorxPath(xPathNumAfiliado);

            return numeroAfiliado;
        }


        public string obtenerEpo()
        {
            string epo = "";

            string xPathEpo = "/html[1]/body[1]/table[1]/tr[2]/td[6]";


            epo = obtenerDatoPorxPath(xPathEpo);



            return epo;
        }


        private string obtenerDatoPorxPath(string xPath)
        {

            string dato = "";



            foreach (HtmlNode table in document.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    foreach (HtmlNode cell in row.SelectNodes("th|td"))
                    {

                        if (cell.XPath.Equals(xPath))
                        {
                            dato = cell.InnerText;
                        }
                    }
                }
            }


            return dato;
        }


    }
}
