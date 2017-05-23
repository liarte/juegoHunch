using System;

public class Partida
{

    // Componentes del juego
    private Personaje miPersonaje;
    private Enemigo miEnemigo;
    private Mapa miPantallaJuego;
    private Fuente fuenteSans18;
    private Marcador Mimarcador;

    // Otros datos del juego
    int puntos;             // Puntuacion obtenida por el usuario
    bool partidaTerminada;  // Si ha terminado una partida


    // Inicialización al comenzar la sesión de juego
    public Partida()
    {
        miPersonaje = new Personaje(this);
        miEnemigo = new Enemigo(this);
        miPantallaJuego = new Mapa(this);
        Mimarcador = new Marcador(this);
        puntos = 0;
        partidaTerminada = false;
        fuenteSans18 = new Fuente("FreeSansBold.ttf", 18);
    }


    // --- Comprobación de teclas
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

        // Si se pulsa ESC, por ahora termina la partida... y el juego
        if (Hardware.TeclaPulsada(Hardware.TECLA_ESC))
            partidaTerminada = true;
    }


    //Animación de los enemigos y demás objetos "que se muevan solos"
    public void moverElementos()
    {
        miEnemigo.Mover();
        miPersonaje.Mover();
    }


    // --- Comprobar colisiones de enemigo con personaje, etc
    public void comprobarColisiones()
    {
        // Colisiones de personaje con fondo: obtener puntos o perder vida
        int puntosMovimiento = miPantallaJuego.ObtenerPuntosPosicion(
          miPersonaje.GetX(),
          miPersonaje.GetY(),
          miPersonaje.GetX() + miPersonaje.GetAncho(),
          miPersonaje.GetY() + miPersonaje.GetAlto());

        // Si realmente ha recogido un objeto, sumamos los puntos en el juego
        if (puntosMovimiento > 0)
        {
            puntos += puntosMovimiento;

            // Si ademas es una campana, avanzamos de nivel
            if (puntosMovimiento == 50)
                //avanzarNivel()
                ;
        }
        if (miPersonaje.ColisionCon(miEnemigo))
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

        //Marcador
        Mimarcador.SetVidas(miPersonaje.GetVidas());
        Mimarcador.SetPuntuacion(puntos);
        Mimarcador.DibujarOculta();

        // Muestro vidas (pronto será parte del marcador)
        /*Hardware.EscribirTextoOculta("Vidas: " + miPersonaje.GetVidas(),
            280, 550, 0xAA, 0xAA, 0xAA, fuenteSans18);*/

        //Hardware.EscribirTextoOculta("Puntuación: "+puntos)

        // Finalmente, muestro en pantalla
        Hardware.VisualizarOculta();
        Hardware.BorrarPantallaOculta(0, 0, 0);
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


    // --- Para que mapa y enemigos puedan saber cosas del personaje
    public Personaje GetPersonaje()
    {
        return miPersonaje;
    }

    // --- Para que personaje y enemigos puedan saber cosas del mapa
    public Mapa GetMapa()
    {
        return miPantallaJuego;
    }
}