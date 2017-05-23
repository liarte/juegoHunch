
using System;
using Tao.Sdl;
public class Fuente
{
    // Atributos

    IntPtr punteroInterno;

    // Operaciones

    /// Constructor a partir de un nombre de fichero y un tamaño
    public Fuente(string nombreFichero, short tamanyo)
    {
        Cargar(nombreFichero, tamanyo);
    }

    public void Cargar(string nombreFichero, short tamanyo)
    {
        punteroInterno = Hardware.CargarFuente(nombreFichero, tamanyo);
        if (punteroInterno == IntPtr.Zero)
            Hardware.ErrorFatal("Fuente inexistente: " + nombreFichero);
    }

    /*public  void Escribir(short x, short y)
    {
        Hardware.EscribirTextoOculta(punteroInterno, x,y);
    }*/

    public IntPtr LeerPuntero()
    {
        return punteroInterno;
    }
}
