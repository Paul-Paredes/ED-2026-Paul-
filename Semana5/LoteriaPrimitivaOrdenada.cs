using System;
using System.Collections.Generic;

class LoteriaPrimitivaOrdenada
{
    static void Main()
    {
        List<int> numeros = new List<int>();

        Console.WriteLine("Ingrese los números ganadores de la lotería primitiva:");

        for (int i = 1; i <= 6; i++)
        {
            Console.Write("Número " + i + ": ");
            int numero = int.Parse(Console.ReadLine());
            numeros.Add(numero);
        }

        // Ordenar los números de menor a mayor
        numeros.Sort();

        Console.WriteLine("\nNúmeros ganadores ordenados:");
        foreach (int n in numeros)
        {
            Console.WriteLine(n);
        }
    }
}
