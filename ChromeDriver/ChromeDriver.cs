﻿using Data;
using libs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


public class ChromeDriver
{

    private OpenQA.Selenium.Chrome.ChromeDriver driver;
    private string versionBaseChrome;
    private string versionBaseChromeDriver;
    Configuracion configuracion = Configuracion.getInstance();

    public ChromeDriver()
    {
         

    }

    public void setup(bool mostrarVentana)
    {
        ChromeDriverService driverService = ChromeDriverService.CreateDefaultService(Rutas.LOCAL_APPDATA);
        driverService.HideCommandPromptWindow = true;
        ChromeOptions options = new ChromeOptions();

        
        if (!mostrarVentana) {

            options.AddArguments("headless");
        }

        driver = new OpenQA.Selenium.Chrome.ChromeDriver(driverService, options);

        // Obtener Version Base de Chrome y ChromeDriver
        versionBaseChrome = Procedimientos.obtenerVersionBase(driver.Capabilities.GetCapability("browserVersion").ToString());
        Dictionary<string, object> capabilities = (Dictionary<string, object>)driver.Capabilities.GetCapability("chrome");
        versionBaseChromeDriver = Procedimientos.obtenerVersionBase(capabilities["chromedriverVersion"].ToString());

    }

    public void salir()
    {

        driver.Dispose();
    }


    public void login(Paciente paciente)
    {


        driver.Url = Rutas.FEMECON_URL;

        string clinica = paciente.clinica;

        try
        {
            this.colocarDatosLogin(clinica);


        }
        catch (Exception)
        {
            throw new Exception("La página de Femecon no está disponible en este momento, intente más tarde");
        }

    }


    public void autorizar(List<Practica> practica, int index, string numeroAfiliado)
    {

        driver.Url = Rutas.FEMECON_URL_AUTORIZADOR;
        

        try
        {

            IWebElement nroPlanillaBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtIdPaper"));
            nroPlanillaBox.Clear();
            nroPlanillaBox.SendKeys("0");
            


            IWebElement numAfiliadoBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtIdPatient"));
            numAfiliadoBox.Clear();
            numAfiliadoBox.SendKeys(numeroAfiliado);
            numAfiliadoBox.SendKeys(Keys.Tab);


            IWebElement diagnosticoBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_lblIdDiagnostic"));
            diagnosticoBox.Clear();
            diagnosticoBox.SendKeys(practica[index].codigoDx);
            diagnosticoBox.SendKeys(Keys.Tab);




            IWebElement codPracticaBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_lblIdPractices"));
            codPracticaBox.Clear();
            codPracticaBox.SendKeys(practica[index].codigo.ToString());



            if (practica[index].tieneCantidad)
            {

                codPracticaBox.SendKeys(Keys.Tab);
                IWebElement cantidadBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtQuantity"));
                cantidadBox.Clear();
                cantidadBox.SendKeys(practica[index].cantidad.ToString());

            }

            IWebElement numMatriculaBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtIdDoctorForPractice"));
            numMatriculaBox.Clear();
            numMatriculaBox.SendKeys(practica[index].matricula);
            numMatriculaBox.SendKeys(Keys.Tab);

            IWebElement nombreAfiliadoBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_divPatient"));
            string nombreAfiliado = nombreAfiliadoBox.GetAttribute("value");

            bool salir = false;

            while (!salir) {

                if (!nombreAfiliado.Equals("")) {
                    salir = true;
                }

                nombreAfiliado = nombreAfiliadoBox.GetAttribute("value");

            }
            
            numMatriculaBox.Submit();

            
            practica[index].codigoAutorizacion = Procedimientos.obtenerCodigoSegunURL(driver.Url);
            
            /*
            IWebElement codigoDeAutorizacionBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_lblCode"));
            practica[index].codigoAutorizacion = codigoDeAutorizacionBox.GetAttribute("value");
            */
        }
        catch (Exception)
        {

            // Si por algun motivo no termina de autorizar, vuelve a intentarlo hasta que este completo
            this.autorizar(practica, index, numeroAfiliado);

        }



    }

    public void autorizar(Paciente paciente)
    {

        string numeroAfiliado = paciente.numeroAfiliado;



        for (int i = 0; i < paciente.practicasParaAutorizar.Count; i++)
        {
            this.autorizar(paciente.practicasParaAutorizar, i, numeroAfiliado);


        }


    }


    private void colocarDatosLogin(string clinica)
    {


        IWebElement clinicaBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtHospitalId"));
        clinicaBox.Clear();
        clinicaBox.SendKeys(clinica);



        IWebElement usuario = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtUserId"));
        usuario.Clear();
        switch (clinica)
        {
            case "30":
                usuario.SendKeys(Credenciales.user_1);
                break;
            case "40":
                usuario.SendKeys(Credenciales.user_2);
                break;
            case "50":
                usuario.SendKeys(Credenciales.user_3);
                break;
            default:
                break;
        }



        IWebElement pass = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_Password"));
        pass.Clear();

        switch (clinica)
        {
            case "30":
                pass.SendKeys(Credenciales.pass_1);
                break;
            case "40":
                pass.SendKeys(Credenciales.pass_2);
                break;
            case "50":
                pass.SendKeys(Credenciales.pass_3);
                break;
            default:
                break;
        }

        pass.Submit();

    }


    public bool validarVersion()
    {
        bool validado = true;

        try
        {
            this.setup(Configuracion.mostrarNavegadorChromeDriver);
       
            this.salir();

            // Las versiones base de Chrome y el ChromeDriver debe ser iguales
            if ( !(versionBaseChrome == versionBaseChromeDriver) ) {
                validado = false;
                throw new Exception("Las versiones del Chrome Driver y el Navegador no coinciden");
            }
          
            
        }
        catch (Exception)
        {

            throw new Exception("Existe un error en la versión del programa o en el navegador Chrome, asegúrese de que ambos estén actualizados.");
        }

        return validado;
    }


    public void descargarValidacionPDF(Paciente paciente) {


        string url = Rutas.URL_IOMA + paciente.dni + paciente.sexo;

        driver.Url = url;

        PrintDocument document = driver.Print(new PrintOptions());

        byte[] sPDFDecoded = Convert.FromBase64String(document.AsBase64EncodedString);

        File.WriteAllBytes(Rutas.CERTIFICACION_PDF, sPDFDecoded);

        this.salir();

    }

    public DateTime obtenerFechaDeNacimiento(Paciente paciente) {

        driver.Url = Rutas.URL_IOMA_PADRON_AFILIADO;

        IWebElement textBoxAfiliado = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/form/div/div[1]/div/input"));
        IWebElement buttonBuscar = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/form/fieldset/div[2]/button[1]"));

        textBoxAfiliado.Clear();
        textBoxAfiliado.SendKeys(paciente.numeroAfiliado);

        buttonBuscar.Submit();

        IWebElement textFechaNacimiento = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div/div[6]/span[2]"));

        //EJ. DEVUELVE 13-Jun-1947
        return Procedimientos.parseToDateTime(textFechaNacimiento.Text);

    }

    public void abrirYConfigurarCertificacionAfiliatoria(Paciente paciente) {

        string url = "https://autogestion.ioma.gba.gov.ar/usuarios/certificacion";

        driver.Url = url;

        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)driver;

        
        string numAfiliado = paciente.numeroAfiliado;
        int sexo = 1;

        // MASCULINO = 1
        // FEMENINO = 2
        if (paciente.sexo == 'F') {
            sexo = 2;

        }

        int year = paciente.fechaDeNacimiento.Year;
        int month = paciente.fechaDeNacimiento.Month;
        int day = paciente.fechaDeNacimiento.Day;

        string fechaDeNacimiento = $"{day.ToString("D2")}/{month.ToString("D2")}/{year}";

        string dni = paciente.dni;

        string script = $"document.querySelector(\"#AFI_NroAfil\").value = {numAfiliado}; ";
        scriptExecutor.ExecuteScript(script);

        script = $"document.querySelector(\"#AFI_Sexo\").value = {sexo};";
        scriptExecutor.ExecuteScript(script);


        script = $"document.querySelector(\"#AFI_Sexo > option:nth-child({sexo})\").selected = \"selected\";";
        scriptExecutor.ExecuteScript(script);


        //Selecciona el campo "Identidad" y lo selecciona para que se muestre en el select
        script = "var selectElement = document.getElementById('AFI_Sexo'); " +
        $"selectElement.value = '{sexo}'; " +
        "var event = new Event('change', { bubbles: true });" +
        "selectElement.dispatchEvent(event);";

        scriptExecutor.ExecuteScript(script);


        script = $"document.querySelector(\"#birthdate\").value = '{fechaDeNacimiento} 12:00:00 a.m.';";
        scriptExecutor.ExecuteScript(script);

        script = $"document.querySelector(\"#AFI_NroDoc\").value = {dni};";
        scriptExecutor.ExecuteScript(script);

        

    }

    public void consultarAutorizaciones(DateTime desde, DateTime hasta, Paciente paciente) {

        driver.Url = Rutas.FEMECON_URL_CONSULTA_AUTORIZACIONES;


        IWebElement fechaDesde = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtFrom_dateInput_text"));
        IWebElement fechaHasta = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_txtTo_dateInput_text"));
        

        IJavaScriptExecutor scriptExecutor = (IJavaScriptExecutor)driver;

        string script = "";

        int diaDesde = desde.Day;
        int mesDesde = desde.Month;
        int yearDesde = desde.Year;

        int diaHasta = hasta.Day;
        int mesHasta = hasta.Month;
        int yearHasta = hasta.Year;

        script = $"document.querySelector(\"#ctl00_ContentPlaceHolder1_txtFrom_dateInput_text\").attributes[1].value = '{diaDesde.ToString("D2")}/{mesDesde.ToString("D2")}/{yearDesde.ToString()}';";

        scriptExecutor.ExecuteScript(script);



        script = $"document.querySelector(\"#ctl00_ContentPlaceHolder1_txtFrom_dateInput\").attributes[5].value = '{yearDesde.ToString()}-{mesDesde.ToString("D2")}-{diaDesde.ToString("D2")}-00-00-00';";

        scriptExecutor.ExecuteScript(script);


        script = $"document.querySelector(\"#ctl00_ContentPlaceHolder1_txtTo_dateInput_text\").attributes[1].value = '{diaHasta.ToString("D2")}/{mesHasta.ToString("D2")}/{yearHasta.ToString()}';";

        scriptExecutor.ExecuteScript(script);



        script = $"document.querySelector(\"#ctl00_ContentPlaceHolder1_txtTo_dateInput\").attributes[5].value = '{yearHasta.ToString()}-{mesHasta.ToString("D2")}-{diaHasta.ToString("D2")}-00-00-00';";

        scriptExecutor.ExecuteScript(script);





        IWebElement pacienteBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_grdQuery_ctl00_ctl02_ctl02_FilterTextBox_name"));
        pacienteBox.SendKeys(paciente.apellido + " " + paciente.nombre);

        pacienteBox.SendKeys(Keys.Tab);

        IWebElement botonCSV = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_Button9"));
        botonCSV.Submit();

        
        //driver.Close();

    }
}

