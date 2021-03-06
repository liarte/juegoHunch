﻿
public class Presentacion
{
    // Atributos    
    private ElemGrafico imagenFondo, cartelMovil;
    private Fuente fuenteSans18;
    private int opcionEscogida;

    // Opciones posibles
    public const byte OPC_PARTIDA = 0;
    public const byte OPC_SALIR = 1;
    public const byte OPC_CREDITOS = 2;
    public const byte OPC_OPCIONES = 3;


    /// Constructor
    public Presentacion()  // Constructor
    {
        imagenFondo = new ElemGrafico("imagenes/presentacion2.png");
        cartelMovil = new ElemGrafico("imagenes/present_letras.png");
        fuenteSans18 = new Fuente("FreeSansBold.ttf", 18);
    }

    /// Lanza la presentacion
    public void Ejecutar()
    {
        int x = 100, y = 100; // Coordenadas del cartel movil
        int incrX = 3, incrY = 3; // Velocidad del cartel movil

        //hasta que se pulse espacio
        do
        {
            // Dibujo la imagen de la presentacion
            imagenFondo.DibujarOculta(0, 0);

            // Escribo avisos de las teclas utilizables
            Hardware.EscribirTextoOculta(
                    "Pulsa J para jugar",
                    310, 550, 0xAA, 0xAA, 0xAA, fuenteSans18);
            Hardware.EscribirTextoOculta(
                    "S para salir, C para créditos, O para opciones",
                    170, 575, 0xAA, 0xAA, 0xAA, fuenteSans18);

            // Dibujo y desplazo el cartel móvil
            cartelMovil.DibujarOculta(x, y);
            x += incrX;
            y += incrY;

            // Invierto velocidad si llega al borde
            if ((x < 10) || (x > 800 - 10 - 192))
                incrX = -incrX;
            if ((y < 10) || (y > 600 - 10 - 196))
                incrY = -incrY;

            // Finalmente, muestro en pantalla y espero
            Hardware.VisualizarOculta();
            Hardware.Pausa(40);
        } while ((!Hardware.TeclaPulsada(Hardware.TECLA_J))
                    && (!Hardware.TeclaPulsada(Hardware.TECLA_S))
                    && (!Hardware.TeclaPulsada(Hardware.TECLA_C))
                    && (!Hardware.TeclaPulsada(Hardware.TECLA_O)));

        opcionEscogida = OPC_PARTIDA;
        if (Hardware.TeclaPulsada(Hardware.TECLA_S))
            opcionEscogida = OPC_SALIR;
        if (Hardware.TeclaPulsada(Hardware.TECLA_C))
            opcionEscogida = OPC_CREDITOS;
        if (Hardware.TeclaPulsada(Hardware.TECLA_O))
            opcionEscogida = OPC_OPCIONES;
    }

    public int GetOpcionEscogida()
    {
        return opcionEscogida;
    }
}
