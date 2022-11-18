using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;


namespace Data
{
    public class Lista
    {

        public List<Practica> ecografia { get; set; }

        public List<Practica> rayos { get; set; }

        public string jsonPracticas { get; }
        public Lista()
        {

            jsonPracticas = @"
            {

                ""ecografia"": [
                  {
                    ""nombre"": ""Ecografía mamaria"",
                    ""orden"": 1,
                    ""codigo"": 180106,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco mama: ""
                  },
                  {
                    ""nombre"": ""Ecografía transvaginal"",
                    ""orden"": 2,
                    ""codigo"": 881807,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco tv: ""
                  },
                  {
                    ""nombre"": ""Mamografía bilateral"",
                    ""orden"": 30,
                    ""codigo"": 340601,
                    ""tieneCantidad"": true,
                    ""cantidad"": 2,
                    ""nombreGuardadoEnArchivos"": ""Mamo: ""
                  },
                  {
                    ""nombre"": ""Prolongación axilar bilateral"",
                    ""orden"": 31,
                    ""codigo"": 340602,
                    ""tieneCantidad"": true,
                    ""cantidad"": 2,
                    ""nombreGuardadoEnArchivos"": ""Prol: ""
                  },
                  {
                    ""nombre"": ""Magnificación / Focalización unilateral"",
                    ""orden"": 32,
                    ""codigo"": 883403,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Magni/Foc: ""
                  },
                  {
                    ""nombre"": ""Magnificación / Focalización bilateral"",
                    ""orden"": 33,
                    ""codigo"": 883403,
                    ""tieneCantidad"": true,
                    ""cantidad"": 2,
                    ""nombreGuardadoEnArchivos"": ""Magni/Foc: ""
                  },
                  {
                    ""nombre"": ""Densitometría 1 región"",
                    ""orden"": 40,
                    ""codigo"": 883470,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Dmo 1 reg: ""
                  },
                  {
                    ""nombre"": ""Densitometría 2 regiones"",
                    ""orden"": 41,
                    ""codigo"": 883471,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Dmo 2 reg: ""
                  },
                  {
                    ""nombre"": ""Ecografía ginecológica / obstétrica"",
                    ""orden"": 3,
                    ""codigo"": 180104,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco gine: ""
                  },
                  {
                    ""nombre"": ""Ecografía cerebral"",
                    ""orden"": 4,
                    ""codigo"": 180107,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco cereb: ""
                  },
                  {
                    ""nombre"": ""Ecografía tiroides"",
                    ""orden"": 5,
                    ""codigo"": 180110,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco tiro: ""
                  },
                  {
                    ""nombre"": ""Ecografía testicular"",
                    ""orden"": 6,
                    ""codigo"": 180111,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco test: ""
                  },
                  {
                    ""nombre"": ""Ecografía abdominal"",
                    ""orden"": 7,
                    ""codigo"": 180112,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco abd: ""
                  },
                  {
                    ""nombre"": ""Ecografía renal"",
                    ""orden"": 8,
                    ""codigo"": 180116,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco renal: ""
                  },
                  {
                    ""nombre"": ""Ecografía vesical / prostática"",
                    ""orden"": 9,
                    ""codigo"": 180114,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco vesi/prost: ""
                  },

                  {
                    ""nombre"": ""Ecografía hepatobiliar"",
                    ""orden"": 10,
                    ""codigo"": 180113,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco hepat: ""
                  },
                 
                  
                  {
                    ""nombre"": ""Ecografía ambas caderas"",
                    ""orden"": 11,
                    ""codigo"": 881803,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco caderas: ""
                  },
                  {
                    ""nombre"": ""Ecografía partes blandas"",
                    ""orden"": 12,
                    ""codigo"": 881804,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Eco pb: ""
                  }
  
                ],
    
                ""rayos"": [
                  {
                    ""nombre"": ""340201"",
                    ""orden"": 1,
                    ""codigo"": 340201,
                    ""tieneCantidad"": false,
                    ""tieneSubsiguiente"": true,
                    ""nombreGuardadoEnArchivos"": ""340201: "",
                    ""descripcion"": ""Rx del cráneo, cara, senos paranasales o cavún, primera exposición""
                  },
                  {
                    ""nombre"": ""340202 subsiguiente"",
                    ""orden"": 2,
                    ""codigo"": 340202,
                    ""tieneCantidad"": true,
                    ""cantidad"": 1,
                    ""nombreGuardadoEnArchivos"": ""340202: ""
                  },


                  {
                    ""nombre"": ""340203"",
                    ""orden"": 3,
                    ""codigo"": 340203,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""340203: "",
                    ""descripcion"": ""Rx hueso temporal o agujeros ópticos, comparativos""

                  },


                  {
                    ""nombre"": ""340204"",
                    ""orden"": 4,
                    ""codigo"": 340204,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""340204: "",
                    ""descripcion"": ""Rx articulación temporomandibular, tres posiciones comparativas""
                  },

                  {
                    ""nombre"": ""340205"",
                    ""orden"": 5,
                    ""codigo"": 340205,
                    ""tieneCantidad"": false,
                    ""tieneSubsiguiente"":true,
                    ""nombreGuardadoEnArchivos"": ""340205: "",
                    ""descripcion"": ""Rx panorámica de cráneo o cara""
                  },

                  {
                    ""nombre"": ""340206 subsiguiente"",
                    ""orden"": 6,
                    ""codigo"": 340206,
                    ""tieneCantidad"": true,
                    ""cantidad"": 1,
                    ""nombreGuardadoEnArchivos"": ""340206: ""
                  },

                  {
                    ""nombre"": ""340209"",
                    ""orden"": 7,
                    ""codigo"": 340209,
                    ""tieneCantidad"": false,
                    ""tieneSubsiguiente"":true,
                    ""nombreGuardadoEnArchivos"": ""340209: "",
                    ""descripcion"": ""Rx de columna, primera exposición""
                  },

                  {
                    ""nombre"": ""340210 subsiguiente"",
                    ""orden"": 8,
                    ""codigo"": 340210,
                    ""tieneCantidad"": true,
                    ""cantidad"":1,
                    ""nombreGuardadoEnArchivos"": ""340210: ""
                  },

                  {
                    ""nombre"": ""340211"",
                    ""orden"": 9,
                    ""codigo"": 340211,
                    ""tieneCantidad"": false,
                    ""tieneSubsiguiente"":true,
                    ""nombreGuardadoEnArchivos"": ""340211: "",
                    ""descripcion"": ""Rx de hombro, húmero, pelvis, cadera y fémur, primera exposición""
                  },    

                  {
                    ""nombre"": ""340212 subsiguiente"",
                    ""orden"": 10,
                    ""codigo"": 340212,
                    ""tieneCantidad"": true,
                    ""cantidad"":1,
                    ""nombreGuardadoEnArchivos"": ""340212: ""
                  },

                  {
                    ""nombre"": ""340213"",
                    ""orden"": 11,
                    ""codigo"": 340213,
                    ""tieneCantidad"": true,
                    ""cantidad"":1,
                    ""nombreGuardadoEnArchivos"": ""340213: "",
                    ""descripcion"": ""Rx de codo, antebrazo, muñeca, mano, dedos, rodillas, pierna, tobillo y pie, 2 por placa""
                  },

                  {
                    ""nombre"": ""340214"",
                    ""orden"": 12,
                    ""codigo"": 340214,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Medición de MMII: "",
                    ""descripcion"": ""Medición comparativa de miembros inferiores""
                  },

                  {
                    ""nombre"": ""340301"",
                    ""orden"": 13,
                    ""codigo"": 340301,
                    ""tieneCantidad"": false,
                    ""tieneSubsiguiente"":true,
                    ""nombreGuardadoEnArchivos"": ""340301: "",
                    ""descripcion"": ""Rx de tórax, primera exposición""
                  },

                  {
                    ""nombre"": ""340302 subsiguiente"",
                    ""orden"": 14,
                    ""codigo"": 340302,
                    ""tieneCantidad"": true,
                    ""nombreGuardadoEnArchivos"": ""340302: ""
                  },

                  {
                    ""nombre"": ""340421"",
                    ""orden"": 15,
                    ""codigo"": 340421,
                    ""tieneCantidad"": false,
                    ""tieneSubsiguiente"":true,
                    ""nombreGuardadoEnArchivos"": ""340421: "",
                    ""descripcion"": ""Rx simple de abdomen""
                  },
                    
                  {
                    ""nombre"": ""340422 subsiguiente"",
                    ""orden"": 16,
                    ""codigo"": 340422,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""340422: ""
                  },

   
                  {
                    ""nombre"": ""340501"",
                    ""orden"": 17,
                    ""codigo"": 340501,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""340501: "",
                    ""descripcion"": ""Rx simple del árbol urinario""
                  },

                  {
                    ""nombre"": ""Espinograma frente y perfil"",
                    ""orden"": 18,
                    ""codigo"": 883480,
                    ""tieneCantidad"": false,
                    ""nombreGuardadoEnArchivos"": ""Espino f/p: "",
                    ""descripcion"": ""Espinograma frente y perfil""
                  }
               ]

            }

            ";


        }

        public void setCodigoDxEco()
        {

            foreach (Practica practica in ecografia)
            {
                practica.codigoDx = "V700";
            }
        }
        public void setCodigoDxRayos()
        {

            foreach (Practica practica in rayos)
            {
                practica.codigoDx = "V725";
            }

        }

        public Lista cargarListadoDePracticasJson()
        {

            return JsonConvert.DeserializeObject<Lista>(this.jsonPracticas);

        }

        public Practica traerPracticaPorOrden(int orden)
        {


            return ecografia.ElementAt(orden); ;

        }

        public Practica traerPracticaPorCodigoEcografia(int codigo)
        {

            return ecografia.SingleOrDefault(ecografia => ecografia.codigo == codigo);
        }

        public Practica traerPracticaPorCodigoRayos(int codigo)
        {

            return rayos.SingleOrDefault(rayos => rayos.codigo == codigo);
        }

        public Practica traerPracticaPorNombre(string nombre)
        {

            return ecografia.SingleOrDefault(ecografia => ecografia.nombre == nombre);
        }



    }
}



