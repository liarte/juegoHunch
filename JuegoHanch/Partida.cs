public class Partida
{

    // Componentes del juego
    private Personaje miPersonaje;
    private Enemigo miEnemigo;
    private Mapa miPantallaJuego;
    private Fuente fuenteSans18;

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
        fuenteSans18 = new Fuente("FreeSansBold.ttf", 18);
    }


    // --- Comprobación de teclas, ratón y joystick -----
    void comprobarTeclas()
    {
        // Muevo si se pulsa alguna flecha del teclado
        if (Hardware.TeclaPulsada(Hardware.TECLA_DER))
            miPersonaje.MoverDerecha();

        if (Hardware.TeclaPulsada(Hardware.TECLA_IZQ))
            miPersonaje.MoverIzquierda();

        /*if (Hardware.TeclaPulsada(Hardware.TECLA_ARR))
            miPersonaje.MoverArriba();

        if (Hardware.TeclaPulsada(Hardware.TECLA_ABA))
            miPersonaje.MoverAbajo();*/

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
        int posXJoystick, posYJoystick;
        bool JoystickUtilizado = Hardware.JoystickMovido(out posXJoystick, out posYJoystick);

        if (Hardware.JoystickPulsado(0))
        {
            if (posXJoystick > 0) miPersonaje.SaltarDerecha();
            else if (posXJoystick < 0) miPersonaje.SaltarIzquierda();
            else miPersonaje.Saltar();
        }
        else if (JoystickUtilizado)
        {
            if (posXJoystick > 0) miPersonaje.MoverDerecha();
            else if (posXJoystick < 0) miPersonaje.MoverIzquierda();
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
    public void moverElementos()
    {
        miEnemigo.Mover();
        miPersonaje.Mover();
    }


    // --- Comprobar colisiones de enemigo con personaje, etc ---
    public void comprobarColisiones()
    {
        //colision de personaje con fondo
        int puntosMovimiento = miPantallaJuego.ObtenerPuntosPosicion(
            miPersonaje.GetX(),
            miPersonaje.GetY(),
            miPersonaje.GetX() + miPersonaje.GetAncho(),
            miPersonaje.GetY() + miPersonaje.GetAlto());

        if (puntosMovimiento > 0)
        {
            puntos += puntosMovimiento;
        }

        if ((puntosMovimiento < 0) || miPersonaje.ColisionCon(miEnemigo))
        {
            miPersonaje.Morir();
            miPersonaje.Reiniciar();
            miEnemigo.Reiniciar();
        }

        if (miPersonaje.GetVidas() == 0)
            partidaTerminada = true;
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

        //muestro las vidas del personaje
        Hardware.EscribirTextoOculta("VIDAS: " + miPersonaje.GetVidas(),
            280, 550, 0xAA, 0xAA, 0xAA, fuenteSans18);


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
        miPersonaje.Reiniciar();
        miPersonaje.SetVidas(3);
        miEnemigo.Reiniciar();
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
