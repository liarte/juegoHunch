
public class Mapa : ElemGrafico
{

    // Datos del mapa del nivel actual
    Partida miPartida;

    private int altoMapa = 16, anchoMapa = 32;
    private int anchoTile = 24, altoTile = 24;
    private int margenIzqdo = 20, margenSuperior = 100;
    private int numCampanas = 1;

    //ElemGrafico arbol, deslizante, ladrillo, ladrilloX, llave, puerta,
    //  sueloFino, sueloFragil, sueloGrueso, techo;

    ElemGrafico lateral, ladrillo, techo, campana;

    string[] datosNivel;
    string[] datosNivelIniciales =
    {"                                ",
     " TTTTT                          ",
     "                                ",
     "                 TTTTTTTTT      ",
     "                   C     L      ",
     "                         L      ",
     "                         L      ",
     "                         L      ",
     "                         L      ",
     "SSSSSSSSSSSSSSSSSSSSSSSSSSSSS   ",
     "SSSSSSSSSSSSSSSSSSSSSSSSSSSSS   ",
     "SSSSSSSSSSSSSSSSSSSSSSSSSSSSS   ",
     "SSSSSSSSSSSSSSSSSSSSSSSSSSSSS   ",
     "SSSSSSSSSSSSSSSSSSSSSSSSSSSSS   ",
     "                                ",
     "                                "};

    // Constructor
    public Mapa(Partida p)
    {
        miPartida = p;   // Para enlazar con el resto de componentes

        lateral = new ElemGrafico("imagenes/lateral.png");
        campana= new ElemGrafico("imagenes/campana1.png");
        ladrillo = new ElemGrafico("imagenes/ladrillo.png");
        //ladrilloX = new ElemGrafico("imagenes/ladrillo2.png");
        //llave = new ElemGrafico("imagenes/llave.png");
        //puerta = new ElemGrafico("imagenes/puerta.png");
        //sueloFino = new ElemGrafico("imagenes/suelo.png");
        //sueloFragil = new ElemGrafico("imagenes/sueloFragil.png");
        techo = new ElemGrafico("imagenes/techo.png");

        datosNivel = new string[altoMapa];
        Reiniciar();
    }

    public  void Reiniciar()
    {
        for (int fila = 0; fila < altoMapa; fila++)
            datosNivel[fila] = datosNivelIniciales[fila];

    }


    public void DibujarOculta()
    {
        // Dibujo el fondo
        for (int fila = 0; fila < altoMapa; fila++)
            for (int colum = 0; colum < anchoMapa; colum++)
            {
                int posX = colum * anchoTile + margenIzqdo;
                int posY = fila * altoTile + margenSuperior;
                switch (datosNivel[fila][colum])
                {
                    case 'L': lateral.DibujarOculta(posX, posY); break;
                    case 'S': ladrillo.DibujarOculta(posX, posY); break;
                    case 'T': techo.DibujarOculta(posX, posY); break;
                    case 'C': campana.DibujarOculta(posX, posY); break;
                        //case 'P': puerta.DibujarOculta(posX, posY); break;
                        //case 'S': sueloFino.DibujarOculta(posX, posY); break;
                        //case 'T': techo.DibujarOculta(posX, posY); break;
                        //case 'V': llave.DibujarOculta(posX, posY); break;
                }
            }
    }

    //comprobacion de si hay alguna casilla de fondo
    public bool EsPosibleMover(int x, int y, int xMax, int yMax)
    {
        for (int fila = 0; fila < altoMapa; fila++)
            for (int colum = 0; colum < anchoMapa; colum++)
            {
                int posX = colum * anchoTile + margenIzqdo;
                int posY = fila * altoTile + margenSuperior;
                // Si se solapa con la posic a la que queremos mover
                if ((posX + anchoTile > x) && (posY + altoTile > y)
                    && (xMax > posX) && (yMax > posY))
                    // Y no es espacio blanco, campana, o vida
                    if ((datosNivel[fila][colum] != ' ')
                        && (datosNivel[fila][colum] != 'V'))
                       // && (datosNivel[fila][colum] != 'V'))
                    {
                        return false;
                    }
            }
        return true;
    }

    public int ObtenerPuntosPosicion(int x, int y, int xmax, int ymax)
    {

        // Compruebo si choca con alguna casilla del fondo
        for (int fila = 0; fila < altoMapa; fila++)
            for (int colum = 0; colum < anchoMapa; colum++)
            {
                int posX = colum * anchoTile + margenIzqdo;
                int posY = fila * altoTile + margenSuperior;

                // Si choca con la casilla que estoy mirando
                if ((posX + anchoTile > x) && (posY + altoTile > y)
                    && (xmax > posX) && (ymax > posY))
                {
                    /*// Si choca con el techo o con un arbol
                    if ((datosNivel[fila][colum] == 'T')
                             || (datosNivel[fila][colum] == 'A'))
                        return -1; // (puntuacion -1: perder vida)*/

                    // Si toca una llave
                    if (datosNivel[fila][colum] == 'V')
                    {
                        // datosNivel[fila][colum] = ' '; (No valido en C#: 2 pasos)
                        datosNivel[fila] = datosNivel[fila].Remove(colum, 1);
                        datosNivel[fila] = datosNivel[fila].Insert(colum, " ");

                        return 10;
                    }

                    // Si toca la puerta y no quedan llaves, 50 puntos
                    // y (pronto) pasar al nivel siguiente
                    if ((datosNivel[fila][colum] == 'C')
                        && (numCampanas == 0))
                    {
                        return 50;
                    }

                }
            }
        return 0;

    }
}
