using System;


namespace Data
{
    public class Practica : IComparable<Practica>
    {
        public int orden { get; set; }
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string codigoAutorizacion { get; set; }
        public string codigoDx { get; set; }
        public int cantidad { get; set; } = 1;
        public bool tieneCantidad { get; set; }
        public string nombreGuardadoEnArchivos { get; set; }
        public string matricula { get; set; }
        public bool matriculaModificada { get; set; }
        public bool tieneSubsiguiente { get; set; }
        public string descripcion { get; set; }


        public Practica() { }



        public Practica(string nombre, int orden, int codigo, bool tieneCantidad, string nombreGuardadoEnArchivos)
        {
            this.nombre = nombre;
            this.orden = orden;
            this.codigo = codigo;
            this.tieneCantidad = tieneCantidad;
            this.nombreGuardadoEnArchivos = nombreGuardadoEnArchivos;
        }





        public int CompareTo(Practica practica)
        {
            if (this.orden > practica.orden)
            {
                return 1;
            }
            else if (this.orden < practica.orden)
            {
                return -1;
            }
            else
            {
                return 0;
            }

        }



        public override string ToString()
        {

            return $"Orden:[{this.orden}] - Codigo:[{this.codigo}] - Nombre:[{this.nombre}] - CodigoDx:[{this.codigoDx}] - Cantidad:[{this.cantidad}] - TieneCantidad:[{this.tieneCantidad}] - TieneSubsiguiente:[{this.tieneSubsiguiente}] - Matricula:[{this.matricula}] - MatriculaModificada:[{this.matriculaModificada}] - NombreEnArchivo:[{this.nombreGuardadoEnArchivos}]";

        }

    }

}
