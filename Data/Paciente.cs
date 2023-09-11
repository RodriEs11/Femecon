using System;
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
        public char sexo { get; set; }
        public string dni { get; set; }
        public DateTime fechaDeNacimiento { get; set; }

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

        public Paciente Clone()
        {

            Paciente pacienteClonado = new Paciente();

            pacienteClonado.nombre = this.nombre;
            pacienteClonado.apellido = this.apellido;
            pacienteClonado.numeroAfiliado = this.numeroAfiliado;
            pacienteClonado.clinica = this.clinica;
            pacienteClonado.epo = this.epo;
            pacienteClonado.esFemecon = this.esFemecon;
            pacienteClonado.activo = this.activo;
            pacienteClonado.sexo = this.sexo;
            pacienteClonado.dni = this.dni;
            pacienteClonado.fechaDeNacimiento = this.fechaDeNacimiento;
            pacienteClonado.practicasParaAutorizar = new List<Practica>();

            foreach (var practica in this.practicasParaAutorizar)
            {
                pacienteClonado.practicasParaAutorizar.Add(practica.Clone());
            }

            return pacienteClonado;
        }

        public void restaurarDesde(Paciente otroPaciente)
        {
           
            this.nombre = otroPaciente.nombre;
            this.apellido = otroPaciente.apellido;
            this.numeroAfiliado = otroPaciente.numeroAfiliado;
            this.clinica = otroPaciente.clinica;
            this.epo = otroPaciente.epo;
            this.esFemecon = otroPaciente.esFemecon;
            this.activo = otroPaciente.activo;
            this.sexo = otroPaciente.sexo;
            this.dni = otroPaciente.dni;
            this.fechaDeNacimiento = otroPaciente.fechaDeNacimiento;

           
            this.practicasParaAutorizar.Clear();
            this.practicasParaAutorizar.AddRange(otroPaciente.practicasParaAutorizar);
        }

        public bool contienePractica(Practica practica) {

            for (int i = 0; i < this.practicasParaAutorizar.Count; i++)
            {
                if (practicasParaAutorizar[i].iguales(practica))
                {
                    return true;
                    
                }
            }

            return false;

        }
        
    }
}
