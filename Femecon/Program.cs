﻿using Data;
using Femecon_2_0;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace Femecon
{

    public static class Program
    {

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)

        {

            Configuracion ConfiguracionGlobal = Configuracion.getInstance();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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

                    case "--show":
                        ConfiguracionGlobal.setMostrarNavegadorChromeDriver(true);
                        break;

                    case "-s":
                        ConfiguracionGlobal.setMostrarNavegadorChromeDriver(true);
                        break;

                }
            }



            Procedimientos.moverArchivosALocalData();

            if (!Directory.Exists(Rutas.LOCAL_APPDATA))
            {
                Directory.CreateDirectory(Rutas.LOCAL_APPDATA);
            }

            Form_principal ventanaPrincipal = new Form_principal();
            Application.Run(ventanaPrincipal);

        }

    }

}
