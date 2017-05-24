using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Nivel
{
    Partida miPartida;

    private int altoMapa = 16, anchoMapa = 32;
    private int anchoTile = 24, altoTile = 24;
    private int margenIzqdo = 20, margenSuperior = 100;
    private int numLlaves = 2;

    ElemGrafico lateral, ladrillo, techo, campana,llave;

    protected string nombre = "(Algo)";

    string[] datosNivel;

    protected string[] datosNivelIniciales =
       {"                                ",
        " TTTTT                          ",
        "                                ",
        "                       TTTTTTT  ",
        "                       C     L  ",
        "                             L  ",
        "                             L  ",
        "                             L  ",
        "       V      V              L  ",
        "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS",
        "SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS",
        "                                ",
        "                                "};


    // Constructor
    public Nivel()
    {
        //miPartida = p;   // Para enlazar con el resto de componentes

        lateral = new ElemGrafico("imagenes/lateral.png");
        campana = new ElemGrafico("imagenes/campana1.png");
        ladrillo = new ElemGrafico("imagenes/ladrillo.png");
        llave = new ElemGrafico("imagenes/llave.png");
        techo = new ElemGrafico("imagenes/techo.png");

        datosNivel = new string[altoMapa];
        Reiniciar();
    }

    public void Reiniciar()
    {
        for (int fila = 0; fila < altoMapa; fila++)
            datosNivel[fila] = datosNivelIniciales[fila];

    }


    public void DibujarOculta()
    {
        numLlaves = 0;
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
                    case 'V': llave.DibujarOculta(posX, posY);
                        numLlaves++;  break;
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
                       && (datosNivel[fila][colum] != 'C')
                     && (datosNivel[fila][colum] != 'V'))
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
                        && (numLlaves == 0))
                    {
                        return 50;
                        
                    }

                }
            }
        return 0;
    }

    public string LeerNombre()
    {
        return nombre;
    }
}
