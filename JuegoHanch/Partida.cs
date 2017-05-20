public class Partida
{

    // Componentes del juego
    private Personaje miPersonaje;
    private Enemigo miEnemigo;
    private Mapa miPantallaJuego;

    // Otros datos del juego
    int puntos;             // Puntuacion obtenida por el usuario
    bool partidaTerminada;  // Si ha terminado una partida


    // Inicialización al comenzar la sesión de juego
    public Partida()
    {
        miPersonaje = new Personaje(this);
        miEnemigo = new Enemigo(this);
        miPantallaJuego = new Mapa(this);
        puntos = 0;
        partidaTerminada = false;
    }


    // --- Comprobación de teclas, ratón y joystick -----
    void comprobarTeclas()
    {
        // Muevo si se pulsa alguna flecha del teclado
        if (Hardware.TeclaPulsada(Hardware.TECLA_DER))
            miPersonaje.MoverDerecha();

        if (Hardware.TeclaPulsada(Hardware.TECLA_IZQ))
            miPersonaje.MoverIzquierda();

        if (Hardware.TeclaPulsada(Hardware.TECLA_ARR))
            miPersonaje.MoverArriba();

        if (Hardware.TeclaPulsada(Hardware.TECLA_ABA))
            miPersonaje.MoverAbajo();

        if (Hardware.TeclaPulsada(Hardware.TECLA_ESP))
        {
            if (Hardware.TeclaPulsada(Hardware.TECLA_DER))
                miPersonaje.SaltarDerecha();
            else if (Hardware.TeclaPulsada(Hardware.TECLA_IZQ))
                miPersonaje.SaltarIzquierda();
            else
                miPersonaje.Saltar();
        }

        // Compruebo el Joystick
        if (Hardware.JoystickPulsado(0))
            miPersonaje.Disparar();

        int posXJoystick, posYJoystick;
        if (Hardware.JoystickMovido(out posXJoystick, out posYJoystick))
        {
            if (posXJoystick > 0) miPersonaje.MoverDerecha();
            else if (posXJoystick < 0) miPersonaje.MoverIzquierda();
            else if (posYJoystick > 0) miPersonaje.MoverAbajo();
            else if (posYJoystick < 0) miPersonaje.MoverArriba();
        }

        // Compruebo el raton
        int posXRaton = 0, posYRaton = 0;
        if (Hardware.RatonPulsado(out posXRaton, out posYRaton))
        {
            miPersonaje.MoverA(posXRaton, posYRaton);
        }


        // Si se pulsa ESC, por ahora termina la partida... y el juego
        if (Hardware.TeclaPulsada(Hardware.TECLA_ESC))
            partidaTerminada = true;
    }


    // --- Animación de los enemigos y demás objetos "que se muevan solos" -----
    void moverElementos()
    {
        miEnemigo.Mover();
        miPersonaje.Mover();
    }


    // --- Comprobar colisiones de enemigo con personaje, etc ---
    void comprobarColisiones()
    {
        // Nada por ahora
    }


    // --- Dibujar en pantalla todos los elementos visibles del juego ---
    void dibujarElementos()
    {
        // Borro pantalla      
        Hardware.BorrarPantallaOculta(0, 0, 0);

        // Dibujo todos los elementos
        miPantallaJuego.DibujarOculta();
        miPersonaje.DibujarOculta();
        miEnemigo.DibujarOculta();

        // Finalmente, muestro en pantalla
        Hardware.VisualizarOculta();
    }


    // --- Pausa tras cada fotograma de juego, para velocidad de 25 fps -----
    void pausaFotograma()
    {
        Hardware.Pausa(40);
    }


    // --- Bucle principal de juego -----
    public void BuclePrincipal()
    {

        partidaTerminada = false;
        do
        {
            comprobarTeclas();
            moverElementos();
            comprobarColisiones();
            dibujarElementos();
            pausaFotograma();
        } while (!partidaTerminada);
    }

    //para que el mapa y enemigos puedan saber los mov del personaje
    public Personaje GetPersonaje()
    {
        return miPersonaje;
    }

    public Mapa GetMapa()
    {
        return miPantallaJuego;
    }

} /* fin de la clase Partida */
