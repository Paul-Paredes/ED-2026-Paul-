using System;

class Nodo
{
    public int Valor;
    public Nodo Izquierdo;
    public Nodo Derecho;

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierdo = null;
        Derecho = null;
    }
}

class ArbolBST
{
    public Nodo Raiz;

    // INSERTAR
    public void Insertar(int valor)
    {
        Raiz = InsertarRec(Raiz, valor);
    }

    private Nodo InsertarRec(Nodo raiz, int valor)
    {
        if (raiz == null)
            return new Nodo(valor);

        if (valor < raiz.Valor)
            raiz.Izquierdo = InsertarRec(raiz.Izquierdo, valor);
        else if (valor > raiz.Valor)
            raiz.Derecho = InsertarRec(raiz.Derecho, valor);

        return raiz;
    }

    // BUSCAR
    public bool Buscar(int valor)
    {
        return BuscarRec(Raiz, valor);
    }

    private bool BuscarRec(Nodo raiz, int valor)
    {
        if (raiz == null) return false;
        if (raiz.Valor == valor) return true;

        if (valor < raiz.Valor)
            return BuscarRec(raiz.Izquierdo, valor);
        else
            return BuscarRec(raiz.Derecho, valor);
    }

    // ELIMINAR
    public Nodo Eliminar(Nodo raiz, int valor)
    {
        if (raiz == null) return raiz;

        if (valor < raiz.Valor)
            raiz.Izquierdo = Eliminar(raiz.Izquierdo, valor);
        else if (valor > raiz.Valor)
            raiz.Derecho = Eliminar(raiz.Derecho, valor);
        else
        {
            // Caso 1: sin hijos
            if (raiz.Izquierdo == null && raiz.Derecho == null)
                return null;

            // Caso 2: un hijo
            if (raiz.Izquierdo == null)
                return raiz.Derecho;
            else if (raiz.Derecho == null)
                return raiz.Izquierdo;

            // Caso 3: dos hijos
            Nodo sucesor = MinNodo(raiz.Derecho);
            raiz.Valor = sucesor.Valor;
            raiz.Derecho = Eliminar(raiz.Derecho, sucesor.Valor);
        }

        return raiz;
    }

    private Nodo MinNodo(Nodo nodo)
    {
        while (nodo.Izquierdo != null)
            nodo = nodo.Izquierdo;
        return nodo;
    }

    // RECORRIDOS
    public void InOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            InOrden(nodo.Izquierdo);
            Console.Write(nodo.Valor + " ");
            InOrden(nodo.Derecho);
        }
    }

    public void PreOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.Valor + " ");
            PreOrden(nodo.Izquierdo);
            PreOrden(nodo.Derecho);
        }
    }

    public void PostOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            PostOrden(nodo.Izquierdo);
            PostOrden(nodo.Derecho);
            Console.Write(nodo.Valor + " ");
        }
    }

    // MINIMO
    public int Minimo()
    {
        Nodo actual = Raiz;
        while (actual.Izquierdo != null)
            actual = actual.Izquierdo;
        return actual.Valor;
    }

    // MAXIMO
    public int Maximo()
    {
        Nodo actual = Raiz;
        while (actual.Derecho != null)
            actual = actual.Derecho;
        return actual.Valor;
    }

    // ALTURA
    public int Altura(Nodo nodo)
    {
        if (nodo == null) return -1;

        int izq = Altura(nodo.Izquierdo);
        int der = Altura(nodo.Derecho);

        return Math.Max(izq, der) + 1;
    }

    // LIMPIAR
    public void Limpiar()
    {
        Raiz = null;
    }
}

class Program
{
    static void Main()
    {
        ArbolBST arbol = new ArbolBST();
        int opcion = -1;

        do
        {
            Console.WriteLine("\n===== MENU BST =====");
            Console.WriteLine("1. Insertar valor");
            Console.WriteLine("2. Buscar valor");
            Console.WriteLine("3. Eliminar valor");
            Console.WriteLine("4. Mostrar recorridos");
            Console.WriteLine("5. Minimo, Maximo y Altura");
            Console.WriteLine("6. Limpiar árbol");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Entrada inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese valor: ");
                    if (int.TryParse(Console.ReadLine(), out int insertar))
                    {
                        arbol.Insertar(insertar);
                        Console.WriteLine("Valor insertado.");
                    }
                    break;

                case 2:
                    Console.Write("Ingrese valor a buscar: ");
                    if (int.TryParse(Console.ReadLine(), out int buscar))
                    {
                        Console.WriteLine(arbol.Buscar(buscar) ? "Sí existe." : "No existe.");
                    }
                    break;

                case 3:
                    Console.Write("Ingrese valor a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int eliminar))
                    {
                        arbol.Raiz = arbol.Eliminar(arbol.Raiz, eliminar);
                        Console.WriteLine("Proceso completado.");
                    }
                    break;

                case 4:
                    Console.WriteLine("\nInorden:");
                    arbol.InOrden(arbol.Raiz);

                    Console.WriteLine("\nPreorden:");
                    arbol.PreOrden(arbol.Raiz);

                    Console.WriteLine("\nPostorden:");
                    arbol.PostOrden(arbol.Raiz);
                    Console.WriteLine();
                    break;

                case 5:
                    if (arbol.Raiz != null)
                    {
                        Console.WriteLine("Minimo: " + arbol.Minimo());
                        Console.WriteLine("Maximo: " + arbol.Maximo());
                        Console.WriteLine("Altura: " + arbol.Altura(arbol.Raiz));
                    }
                    else
                    {
                        Console.WriteLine("El árbol está vacío.");
                    }
                    break;

                case 6:
                    arbol.Limpiar();
                    Console.WriteLine("Árbol eliminado.");
                    break;

                case 0:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 0);
    }
}
