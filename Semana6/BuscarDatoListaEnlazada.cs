using System;

class Nodo
{
    public int Dato;
    public Nodo Siguiente;

    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

class ListaEnlazada
{
    private Nodo cabeza;

    public ListaEnlazada()
    {
        cabeza = null;
    }

    public void Agregar(int dato)
    {
        Nodo nuevo = new Nodo(dato);

        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }

    public int Buscar(int valor)
    {
        int contador = 0;
        Nodo actual = cabeza;

        while (actual != null)
        {
            if (actual.Dato == valor)
            {
                contador++;
            }
            actual = actual.Siguiente;
        }

        if (contador == 0)
        {
            Console.WriteLine("El dato no fue encontrado en la lista.");
        }

        return contador;
    }
}

class BuscarDatoListaEnlazada
{
    static void Main(string[] args)
    {
        ListaEnlazada lista = new ListaEnlazada();

        lista.Agregar(10);
        lista.Agregar(20);
        lista.Agregar(10);
        lista.Agregar(30);
        lista.Agregar(10);

        int valorBuscar = 10;
        int repeticiones = lista.Buscar(valorBuscar);

        if (repeticiones > 0)
        {
            Console.WriteLine("El dato " + valorBuscar + " se encontró " + repeticiones + " veces en la lista.");
        }
    }
}
