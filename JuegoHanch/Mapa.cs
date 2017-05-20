/** 
 *   Mapa: Mapa (array) que representa a un nivel de juego
 *  
 *   @see Hardware ElemGrafico Juego
 *   @author 1-DAI IES San Vicente 2010/11
 */

/* --------------------------------------------------
   Versiones hasta la fecha:
   
   Num.   Fecha       Por / Cambios
   ---------------------------------------------------
   0.03  23-Dic-2010  Nacho Cabanes
                      Mapa inicial, de la primera pantalla
 ---------------------------------------------------- */

public class Mapa : ElemGrafico
{

    // Datos del mapa del nivel actual
    Partida miPartida;

    private int altoMapa = 16, anchoMapa = 32; //anchoMapa = 32;
    private int anchoTile = 24, altoTile = 24;
    private int margenIzqdo = 20, margenSuperior = 100;

    //ElemGrafico arbol, deslizante, ladrillo, ladrilloX, llave, puerta,
    //  sueloFino, sueloFragil, sueloGrueso, techo;

    ElemGrafico lateral, ladrillo, techo;


    string[] datosNivel =
    {"                                ",
     " TTTTT                          ",
     "                                ",
     "                TTTTTTTTTT      ",
     "                         L      ",
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
        //deslizante = new ElemGrafico("imagenes/deslizante.png");
        ladrillo = new ElemGrafico("imagenes/ladrillo.png");
        //ladrilloX = new ElemGrafico("imagenes/ladrillo2.png");
        //llave = new ElemGrafico("imagenes/llave.png");
        //puerta = new ElemGrafico("imagenes/puerta.png");
        //sueloFino = new ElemGrafico("imagenes/suelo.png");
        //sueloFragil = new ElemGrafico("imagenes/sueloFragil.png");
        techo = new ElemGrafico("imagenes/techo.png");
    }

    public void DibujarOculta()
    {
        // Dibujo el fondo
        for (int i = 0; i < altoMapa; i++)
            for (int j = 0; j < anchoMapa; j++)
            {
                int posX = j * anchoTile + margenIzqdo;
                int posY = i * altoTile + margenSuperior;
                switch (datosNivel[i][j])
                {
                    case 'L': lateral.DibujarOculta(posX, posY); break;
                    case 'S': ladrillo.DibujarOculta(posX, posY); break;
                    case 'T': techo.DibujarOculta(posX, posY); break;
                        //case 'L': ladrillo.DibujarOculta(posX, posY); break;
                        //case 'P': puerta.DibujarOculta(posX, posY); break;
                        //case 'S': sueloFino.DibujarOculta(posX, posY); break;
                        //case 'T': techo.DibujarOculta(posX, posY); break;
                        //case 'V': llave.DibujarOculta(posX, posY); break;
                }
            }
    }

    //comprobacion de si hay alguna casilla de fondo
    public bool EsPosibleMover (int x, int y, int xMax, int yMax)
    {
        for (int fila = 0; fila < altoMapa; fila++)
            for (int colum = 0; colum < anchoMapa; colum++)
            {
                int posX = colum * anchoTile + margenIzqdo;
                int posY = fila * altoTile + margenSuperior;
                //si no hay espacios en blancos
                if((datosNivel[fila][colum] != ' ')
                    && (posX+anchoTile > x) && (posY+altoTile > y)
                        && (xMax > posX) && (yMax > posY))
                {
                    return false;
                }
            }
        return true;

    }


} /* fin de la clase Mapa */
