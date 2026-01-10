using System;
using System.Collections.Generic;

class AsignaturasPendientes
{
    static void Main(string[] args)
    {
        // Lista de asignaturas
        List<string> asignaturas = new List<string>()
        {
            "Matemáticas",
            "Física",
            "Química",
            "Historia",
            "Lengua"
        };

        // Lista para almacenar las asignaturas reprobadas
        List<string> pendientes = new List<string>();

        foreach (string asignatura in asignaturas)
        {
            Console.Write("Ingrese la nota de " + asignatura + ": ");
            double nota = Convert.ToDouble(Console.ReadLine());

            // Si la nota es menor a 7, se considera reprobada
            if (nota < 7)
            {
                pendientes.Add(asignatura);
            }
        }

        Console.WriteLine("\nAsignaturas que debe repetir:");

        if (pendientes.Count == 0)
        {
            Console.WriteLine("No tiene asignaturas pendientes. ¡Felicidades!");
        }
        else
        {
            foreach (string asignatura in pendientes)
            {
                Console.WriteLine("- " + asignatura);
            }
        }
    }
}
