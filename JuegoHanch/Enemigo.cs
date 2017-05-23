using System;

public class Enemigo : ElemGrafico
{

    // Datos del enemigo
    Partida miPartida;

    // Constructor
    public Enemigo(Partida p)
    {
        miPartida = p;   // Para enlazar con el resto de componentes
        //x = 200;         // Resto de valores iniciales
        //y = 296;
        MoverA(600, 296);
        SetAnchoAlto(30, 30);
        SetVelocidad(4, 0);
        //incrX = 4;
        CargarImagen("imagenes/enemigo.png");
    }


    // Métodos de movimiento
    public void Mover()
    {
        x += incrX;

        if ((x < 100) || (x > 700))
            incrX = (short)(-incrX);
    }

}

