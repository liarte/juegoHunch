public class Personaje : ElemGrafico
{

    // Datos del personaje
    Partida miPartida; // Para poder comunicar con la partida
                       
    short vidas;  // Vidas restantes
    //incremento de saltos
    bool saltando;
    int incrXSalto;
    int fotogramaMvto;
    int cantidadMovimientoSalto;
    int[] pasosSaltoArriba = {-14, -14, -11, -8, -6, -4, -2, 0,
                             0, 2, 4, 6, 8, 11, 14, 14 };

    // Constructor
    public Personaje(Partida p)
    {
        miPartida = p;   // Para enlazar con el resto de componentes
        x = 200;         // Resto de valores iniciales
        y = 274;
        SetAnchoAlto(40, 30);
        SetVelocidad(4, 4);
        vidas = 3;
        saltando = false;
        incrXSalto = 0;
        cantidadMovimientoSalto = pasosSaltoArriba.Length;

        CargarImagen("imagenes/p1.png");
    }


    // Métodos de movimiento
    public void MoverDerecha()
    {
        if (saltando) return; //no se puede mover mientras salta
        if (miPartida.GetMapa().EsPosibleMover(x + incrX, y,
            x + ancho + incrX, y + alto))
            x += incrX;
    }

    public void MoverIzquierda()
    {
        if (saltando) return;
        if (miPartida.GetMapa().EsPosibleMover(x - incrX, y,
           x + ancho - incrX, y + alto))
            x -= incrX;
    }

    public void MoverArriba()
    {
        if (saltando) return;
        if (miPartida.GetMapa().EsPosibleMover(x, y - incrY,
                  x + ancho, y + alto - incrY))
            y -= incrY;
    }

    public void MoverAbajo()
    {
        if (saltando) return;
        if (miPartida.GetMapa().EsPosibleMover(x, y + incrY,
          x + ancho, y + alto + incrY))
            y += incrY;
    }


    public new void Mover()
    {
        if (saltando)
        {
            //calculo de las siguientes pos
            short xProxMov = (short)(x + incrXSalto);
            short yProxMov = (short)(y + pasosSaltoArriba[fotogramaMvto]);
            bool subiendoSalto = (pasosSaltoArriba[fotogramaMvto] < 0);

            //si aun se puede mover
            if (miPartida.GetMapa().EsPosibleMover(
                xProxMov, yProxMov + alto - 4,
                xProxMov + ancho, yProxMov + alto)
                || subiendoSalto)
            {
                x = xProxMov;
                y = yProxMov;
            }
            //si no, esta cayendo
            else
                saltando = false;
            fotogramaMvto++;
            if (fotogramaMvto >= cantidadMovimientoSalto)
                saltando = false;
        }
    }

    public void Saltar()
    {
        if (saltando)
            return;
        saltando = true;
        fotogramaMvto = 0;
        incrXSalto = 0;
    }

    public void SaltarDerecha()
    {
        Saltar();
        incrXSalto = incrX;
    }

    public void SaltarIzquierda()
    {
        Saltar();
        incrXSalto = -incrX;
    }
    public void Disparar()
    {
        // TODO: Vacio por ahora
    }


    // Métodos de acceso a las vidas
    public int GetVidas()
    {
        return vidas;
    }

    public void SetVidas(short n)
    {
        vidas = n;
    }

    public void Morir()
    {
        vidas--;
    }


}
