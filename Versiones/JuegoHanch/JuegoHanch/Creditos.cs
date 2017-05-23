public class Creditos
{
    // Atributos
    private Fuente fuenteSans18;
    private Fuente fuenteSans12;

    public Creditos()  // Constructor
    {
        fuenteSans18 = new Fuente("FreeSansBold.ttf", 18);
        fuenteSans12 = new Fuente("FreeSansBold.ttf", 12);
    }


    /// Lanza la pantalla de creditos
    public void Ejecutar()
    {
        bool salir = false;

        byte color = 0x55;
        while (!salir)
        {

            Hardware.BorrarPantallaOculta(0, 0, 0); // Borro en negro

            Hardware.EscribirTextoOculta(
                " PANTALLA CREDITOS ", 110, 100,
                  0x77, 0x77, color, fuenteSans18);

            Hardware.EscribirTextoOculta("Nombre y apellidos:", 200, 240,
                  color, color, 0, fuenteSans18);
            Hardware.EscribirTextoOculta("Carla Liarte Felipe ", 200, 270,
                  color, color, 0, fuenteSans18);
            Hardware.EscribirTextoOculta("Profesora: ", 200, 300,
                  color, color, 0, fuenteSans18);
            Hardware.EscribirTextoOculta("Mari chelo", 200, 330,
                  color, color, 0, fuenteSans18);

            Hardware.EscribirTextoOculta("1º DAM - Programación - 2016/2017", 200, 390,
                  color, color, 0, fuenteSans18);

            Hardware.EscribirTextoOculta(
                  "Pulsa ESC para volver a la presentación...",
                  110, 550, 0xAA, 0xAA, 0xAA, fuenteSans12);


            Hardware.VisualizarOculta();
            Hardware.Pausa(40);

            salir = Hardware.TeclaPulsada(Hardware.TECLA_ESC);
        }
    }

} /* fin de la clase Creditos */
