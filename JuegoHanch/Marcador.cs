using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Marcador
{
    private ElemGrafico imgVidas;
    private int vidas;
    private int puntuacion;
    private string nombreNivel;

    private Partida miPartida;
    Fuente tipoDeLetra;

    public void SetVidas(int valor)
    {
        vidas = valor;
    }

    public int GetVidas()
    {
        return vidas;
    }

    public Marcador(Partida p)
    {
        miPartida = p;
        tipoDeLetra = new Fuente("FreeSansBold.ttf", 18);
        imgVidas = new ElemGrafico("imagenes/vidas.png");
    }

    public int GetPuntuacion()
    {
        return puntuacion;
    }

    public void SetPuntuacion(int valor)
    {
        puntuacion = valor;
    }

    public void SetNombre(string valor)
    {
        nombreNivel = valor;
    }

    public void DibujarOculta()
    {
        Hardware.EscribirTextoOculta("Puntuación: 00000",
            550, 520, 0xFF, 0xFF, 0x00, tipoDeLetra);

        //vidas
        int vidasMostrar = vidas;
        if (vidasMostrar > 5)
            vidasMostrar = 5;
        for (int i = 0; i < vidasMostrar; i++)
        {
            imgVidas.DibujarOculta(20 + 40 * i, 500);
        }
    }
}
