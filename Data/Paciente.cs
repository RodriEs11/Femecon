using System.Collections.Generic;


namespace Data
{
    public class Paciente
    {

        private static Paciente instancia;


        public string nombre { get; set; }
        public string apellido { get; set; }
        public string numeroAfiliado { get; set; }
        public string clinica { get; set; }
        public string epo { get; set; }
        public bool esFemecon { get; set; }
        public bool activo { get; set; }

        public List<Practica> practicasParaAutorizar = new List<Practica>();

        public Paciente() { }
        public Paciente(string nombre, string apellido, string numeroAfiliado, string epo, bool activo)
        {

            this.nombre = nombre;
            this.apellido = apellido;
            this.numeroAfiliado = numeroAfiliado;
            this.epo = epo;
            this.activo = activo;
            setClinica();

        }

        private void setClinica()
        {
            this.esFemecon = true;
            switch (this.epo)
            {

                case "490":
                    this.clinica = "40";
                    break;

                case "260":
                    this.clinica = "30";
                    break;

                case "028":
                    this.clinica = "50";
                    break;

                case "270":
                    this.clinica = "50";
                    break;

                case "063":
                    this.clinica = "50";
                    break;

                case "245":
                    this.clinica = "50";
                    break;

                case "357":
                    this.clinica = "50";
                    break;

                case "371":
                    this.clinica = "50";
                    break;

                case "408":
                    this.clinica = "50";
                    break;

                case "410":
                    this.clinica = "50";
                    break;

                case "568":
                    this.clinica = "50";
                    break;

                case "756":
                    this.clinica = "50";
                    break;

                case "791":
                    this.clinica = "50";
                    break;

                case "826":
                    this.clinica = "50";
                    break;

                case "840":
                    this.clinica = "50";
                    break;

                case "861":
                    this.clinica = "50";
                    break;

                default:
                    this.clinica = "";
                    this.esFemecon = false;
                    break;

            }


        }
        public static Paciente getInstance()
        {
            if (instancia == null)
            {
                instancia = new Paciente();
            }

            return instancia;
        }

        public override string ToString()
        {

            return $"Paciente:[{this.nombre} {this.apellido}] - [{this.numeroAfiliado}] Epo:[{this.epo}] Clinica:[{this.clinica}] Femecon:[{this.esFemecon}] Activo:[{this.activo}] Total de prácticas:[{this.practicasParaAutorizar.Count}]";

        }

    }
}
