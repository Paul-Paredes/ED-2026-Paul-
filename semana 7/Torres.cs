using System;
using System.Collections.Generic;

class Program
{
    static int paso = 0;

    static void Main()
    {
        Console.WriteLine("Ingrese el número de discos:");
        int n = int.Parse(Console.ReadLine());

        // Torres como pilas
        Stack<int> A = new Stack<int>();
        Stack<int> B = new Stack<int>();
        Stack<int> C = new Stack<int>();

        // Cargar discos en A (n abajo, 1 arriba)
        for (int d = n; d >= 1; d--) A.Push(d);

        Console.WriteLine("\nEstado inicial:");
        Imprimir(A, B, C);

        // Resolver usando pilas
        Hanoi(n, A, C, B, "A", "C", "B");

        Console.WriteLine("\nEstado final:");
        Imprimir(A, B, C);
    }

    static void Hanoi(int n, Stack<int> origen, Stack<int> destino, Stack<int> auxiliar,
                      string nomOrigen, string nomDestino, string nomAux)
    {
        if (n == 1)
        {
            Mover(origen, destino, nomOrigen, nomDestino);
            return;
        }

        Hanoi(n - 1, origen, auxiliar, destino, nomOrigen, nomAux, nomDestino);
        Mover(origen, destino, nomOrigen, nomDestino);
        Hanoi(n - 1, auxiliar, destino, origen, nomAux, nomDestino, nomOrigen);
    }

    static void Mover(Stack<int> desde, Stack<int> hacia, string nomDesde, string nomHacia)
    {
        int disco = desde.Pop();

        if (hacia.Count > 0 && hacia.Peek() < disco)
            throw new InvalidOperationException("Movimiento inválido: disco grande sobre disco pequeño.");

        hacia.Push(disco);

        paso++;
        Console.WriteLine($"Paso {paso}: Mover disco {disco} de {nomDesde} a {nomHacia}");
    }

    static void Imprimir(Stack<int> A, Stack<int> B, Stack<int> C)
    {
        Console.WriteLine("A: " + Formato(A));
        Console.WriteLine("B: " + Formato(B));
        Console.WriteLine("C: " + Formato(C));
        Console.WriteLine("----------------------------------");
    }

    static string Formato(Stack<int> t)
    {
        int[] arr = t.ToArray();   // top -> bottom
        Array.Reverse(arr);        // bottom -> top
        return "[" + string.Join(", ", arr) + "]";
    }
} 
