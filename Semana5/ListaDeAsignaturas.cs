using System;
using System.Collections.Generic;

class ListaDeAsignaturas
{
    static void Main()
    {
        List<string> asignaturas = new List<string>();

        asignaturas.Add("Lenguaje");
        asignaturas.Add("Estudios Sociales");
        asignaturas.Add("Ciencias Naturales");
        asignaturas.Add("Historia");

        Console.WriteLine("Asignaturas del curso:");
        foreach (string materia in asignaturas)
        {
            Console.WriteLine(materia);
        }
    }
}
