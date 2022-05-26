using Data;
using Femecon_2_0;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Femecon
{

    static class Program
    {

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)

        {


            if (File.Exists("Codigos.txt"))
            {

                try { File.Move("Codigos.txt", Rutas.CODIGOS_TXT); } catch (Exception e) { }

            }

            if (File.Exists("Paciente.txt"))
            {

                try { File.Move("Paciente.txt", Rutas.PACIENTE_TXT); } catch (Exception e) { }
            }

            if (File.Exists("chromedriver.exe"))
            {

                try { File.Move("chromedriver.exe", Rutas.CHROME_DRIVER_EXE); } catch (Exception e) { }
            }

                foreach (string s in args)
                {
                    switch (s)
                    {

                        case "--export":
                            Procedimientos.generarZipRelease();
                            break;

                        case "-e":
                            Procedimientos.generarZipRelease();
                            break;
                    }
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);


                //Configuracion global = Configuracion.getInstance();
                //bool estaActualizado = Configuracion.actualizado;



                if (args.Length == 0)
                {
                    //if (estaActualizado)
                    if (true)
                    {

                        if (!Directory.Exists(Rutas.LOCAL_APPDATA))
                        {
                            Directory.CreateDirectory(Rutas.LOCAL_APPDATA);
                        }

                        Form_principal ventanaPrincipal = new Form_principal();
                        Application.Run(ventanaPrincipal);

                    }
                    else
                    {

                        Process.Start(Rutas.UPDATER);

                    }
                }

            }
        }

    }
