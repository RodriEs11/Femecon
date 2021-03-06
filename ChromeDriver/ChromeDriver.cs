using Data;
using libs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;


public class ChromeDriver
{

    private OpenQA.Selenium.Chrome.ChromeDriver driver;
    Configuracion configuracion = Configuracion.getInstance();

    public ChromeDriver()
    {

    }

    public void setup()
    {
        ChromeDriverService driverService = ChromeDriverService.CreateDefaultService(Rutas.LOCAL_APPDATA);
        driverService.HideCommandPromptWindow = true;
        ChromeOptions options = new ChromeOptions();

        bool mostrarVentana = configuracion.getMostrarNavegadorChromeDriver();

        if (!mostrarVentana) {

            options.AddArguments("headless");
        }

        
        driver = new OpenQA.Selenium.Chrome.ChromeDriver(driverService, options);

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


            numMatriculaBox.Submit();

            IWebElement codigoDeAutorizacionBox = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_lblCode"));
            practica[index].codigoAutorizacion = codigoDeAutorizacionBox.GetAttribute("value");

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
        bool validado;

        try
        {
            this.setup();
            this.salir();
            validado = true;
        }
        catch (Exception)
        {

            throw new Exception("Existe un error en la versión del programa o en el navegador Chrome, asegúrese de que ambos estén actualizados.");
        }

        return validado;
    }
}

