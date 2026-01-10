using System;
using System.Collections.Generic;

class NumerosInversosLista
{
    static void Main()
    {
        List<int> numeros = new List<int>();

        // Almacenar los números del 1 al 10 en la lista
        for (int i = 1; i <= 10; i++)
        {
            numeros.Add(i);
        }

        // Mostrar los números en orden inverso separados por comas
        Console.WriteLine("Números en orden inverso:");

        for (int i = numeros.Count - 1; i >= 0; i--)
        {
            Console.Write(numeros[i]);

            if (i > 0)
            {
                Console.Write(", ");
            }
        }
    }
}
