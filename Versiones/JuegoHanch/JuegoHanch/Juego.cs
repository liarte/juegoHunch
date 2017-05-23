
public class Juego
{
    private Presentacion presentacion;
    private Partida partida;
    private Creditos creditos;
    private Opciones opciones;


    // Inicialización al comenzar la sesión de juego
    public Juego()
    {
        // Inicializo modo grafico 800x600 puntos, 24 bits de color
        bool pantallaCompleta = false;
        Hardware.Inicializar(800, 600, 24, pantallaCompleta);

        // Inicializo componentes del juego
        presentacion = new Presentacion();
        partida = new Partida();
        creditos = new Creditos();
        opciones = new Opciones();
    }


    // --- Comienzo de un nueva partida: reiniciar variables ---
    public void Ejecutar()
    {
        do
        {
            presentacion.Ejecutar();
            switch (presentacion.GetOpcionEscogida())
            {
                case Presentacion.OPC_CREDITOS:
                    creditos.Ejecutar();
                    break;
                case Presentacion.OPC_PARTIDA:
                    partida.BuclePrincipal();
                    break;
                case Presentacion.OPC_OPCIONES:
                    opciones.Ejecutar();
                    break;
            }
        }
        while (presentacion.GetOpcionEscogida() != Presentacion.OPC_SALIR);
    }


    // --- Cuerpo del programa -----
    public static void Main()
    {
        Juego juego = new Juego();
        juego.Ejecutar();
    }

}

