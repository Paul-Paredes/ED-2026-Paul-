using System;

class PalindromoComparador
{
    static void Main()
    {
        Console.Write("Ingrese una palabra: ");
        string texto = Console.ReadLine().ToLower();

        bool esPalindromo = true;
        int inicio = 0;
        int fin = texto.Length - 1;

        while (inicio < fin)
        {
            if (texto[inicio] != texto[fin])
            {
                esPalindromo = false;
                break;
            }
            inicio++;
            fin--;
        }

        if (esPalindromo)
            Console.WriteLine("La palabra ingresada es un palíndromo.");
        else
            Console.WriteLine("La palabra ingresada no es un palíndromo.");
    }
}
