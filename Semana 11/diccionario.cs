using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static Dictionary<string, string> EsToEn = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    static Dictionary<string, string> EnToEs = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        InicializarDiccionario();

        int opcion;

        do
        {
            Console.WriteLine("\n==================== MENÚ =====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabra al diccionario");
            Console.WriteLine("3. Ver frases de ejemplo");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            int.TryParse(Console.ReadLine(), out opcion);

            switch (opcion)
            {
                case 1:
                    TraducirFrase();
                    break;
                case 2:
                    AgregarPalabra();
                    break;
                case 3:
                    MostrarFrases();
                    break;
                case 0:
                    Console.WriteLine("Programa finalizado.");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

        } while (opcion != 0);
    }

    static void InicializarDiccionario()
    {
        // ====== PALABRAS BASE (100+) ======

        Agregar("Tiempo", "Time");
        Agregar("Persona", "Person");
        Agregar("Año", "Year");
        Agregar("Día", "Day");
        Agregar("Hombre", "Man");
        Agregar("Mundo", "World");
        Agregar("Vida", "Life");
        Agregar("Mano", "Hand");
        Agregar("Parte", "Part");
        Agregar("Niño", "Child");
        Agregar("Ojo", "Eye");
        Agregar("Mujer", "Woman");
        Agregar("Lugar", "Place");
        Agregar("Semana", "Week");
        Agregar("Caso", "Case");
        Agregar("Gobierno", "Government");
        Agregar("Empresa", "Company");
        Agregar("Amor", "Love");
        Agregar("Familia", "Family");
        Agregar("Amigo", "Friend");
        Agregar("Escuela", "School");
        Agregar("Ciudad", "City");
        Agregar("País", "Country");
        Agregar("Casa", "House");
        Agregar("Comida", "Food");
        Agregar("Agua", "Water");
        Agregar("Sol", "Sun");
        Agregar("Luna", "Moon");
        Agregar("Estrella", "Star");
        Agregar("Cielo", "Sky");
        Agregar("Mar", "Sea");
        Agregar("Río", "River");
        Agregar("Montaña", "Mountain");
        Agregar("Perro", "Dog");
        Agregar("Gato", "Cat");
        Agregar("Libro", "Book");
        Agregar("Mesa", "Table");
        Agregar("Silla", "Chair");
        Agregar("Puerta", "Door");
        Agregar("Ventana", "Window");
        Agregar("Camino", "Road");
        Agregar("Dinero", "Money");
        Agregar("Salud", "Health");
        Agregar("Música", "Music");
        Agregar("Arte", "Art");
        Agregar("Historia", "History");
        Agregar("Ciencia", "Science");
        Agregar("Fuerza", "Strength");
        Agregar("Poder", "Power");
        Agregar("Problema", "Problem");
        Agregar("Solución", "Solution");
        Agregar("Idea", "Idea");
        Agregar("Información", "Information");
        Agregar("Servicio", "Service");
        Agregar("Cambio", "Change");
        Agregar("Feliz", "Happy");
        Agregar("Triste", "Sad");
        Agregar("Rápido", "Fast");
        Agregar("Lento", "Slow");
        Agregar("Nuevo", "New");
        Agregar("Viejo", "Old");
        Agregar("Grande", "Big");
        Agregar("Pequeño", "Small");
        Agregar("Alto", "Tall");
        Agregar("Bajo", "Short");
        Agregar("Trabajo", "Work");
        Agregar("Profesor", "Teacher");
        Agregar("Estudiante", "Student");
        Agregar("Paz", "Peace");
        Agregar("Guerra", "War");
        Agregar("Fuego", "Fire");
        Agregar("Tierra", "Earth");
        Agregar("Aire", "Air");
        Agregar("Luz", "Light");
        Agregar("Oscuro", "Dark");
        Agregar("Verdad", "Truth");
        Agregar("Mentira", "Lie");
        Agregar("Esperanza", "Hope");
        Agregar("Miedo", "Fear");
        Agregar("Amable", "Kind");
        Agregar("Difícil", "Difficult");
        Agregar("Fácil", "Easy");
        Agregar("Importante", "Important");
        Agregar("Interesante", "Interesting");
        Agregar("Rico", "Rich");
        Agregar("Pobre", "Poor");
        Agregar("Carro", "Car");
        Agregar("Teléfono", "Phone");
        Agregar("Computadora", "Computer");
        Agregar("Programa", "Program");
        Agregar("Lenguaje", "Language");
    }

    static void Agregar(string es, string en)
    {
        EsToEn[es] = en;
        EnToEs[en] = es;
    }

    static void TraducirFrase()
    {
        Console.Write("\nIngrese la frase: ");
        string frase = Console.ReadLine();

        string resultado = Regex.Replace(frase, @"\b[\wáéíóúÁÉÍÓÚñÑ]+\b", palabra =>
        {
            string texto = palabra.Value;

            if (EsToEn.ContainsKey(texto))
                return EsToEn[texto];
            else if (EnToEs.ContainsKey(texto))
                return EnToEs[texto];
            else
                return texto;
        });

        Console.WriteLine("Traducción: " + resultado);
    }

    static void AgregarPalabra()
    {
        Console.Write("Palabra en Español: ");
        string es = Console.ReadLine();

        Console.Write("Traducción en Inglés: ");
        string en = Console.ReadLine();

        Agregar(es, en);
        Console.WriteLine("Palabra agregada correctamente.");
    }

    static void MostrarFrases()
    {
        Console.WriteLine("\n===== FRASES EN ESPAÑOL =====");
        Console.WriteLine("1. El amor es importante en la vida.");
        Console.WriteLine("2. El perro está en la casa.");
        Console.WriteLine("3. La mujer trabaja en la empresa.");
        Console.WriteLine("4. El niño juega con el gato.");
        Console.WriteLine("5. La ciudad es grande y hermosa.");
        Console.WriteLine("6. El libro está sobre la mesa.");
        Console.WriteLine("7. La salud es más importante que el dinero.");
        Console.WriteLine("8. La familia vive en una casa pequeña.");
        Console.WriteLine("9. El profesor explica la lección.");
        Console.WriteLine("10. El estudiante lee un libro interesante.");
        Console.WriteLine("11. La paz es mejor que la guerra.");
        Console.WriteLine("12. La música es parte del arte.");
        Console.WriteLine("13. El cielo está claro hoy.");
        Console.WriteLine("14. La montaña es muy alta.");
        Console.WriteLine("15. El río fluye hacia el mar.");
        Console.WriteLine("16. La información es poder.");
        Console.WriteLine("17. El problema tiene solución.");
        Console.WriteLine("18. El trabajo es difícil pero importante.");
        Console.WriteLine("19. La esperanza vence el miedo.");
        Console.WriteLine("20. El cambio es parte de la vida.");

        Console.WriteLine("\n===== PHRASES IN ENGLISH =====");
        Console.WriteLine("1. Love is important in life.");
        Console.WriteLine("2. The dog is in the house.");
        Console.WriteLine("3. The woman works in the company.");
        Console.WriteLine("4. The child plays with the cat.");
        Console.WriteLine("5. The city is big and beautiful.");
        Console.WriteLine("6. The book is on the table.");
        Console.WriteLine("7. Health is more important than money.");
        Console.WriteLine("8. The family lives in a small house.");
        Console.WriteLine("9. The teacher explains the lesson.");
        Console.WriteLine("10. The student reads an interesting book.");
        Console.WriteLine("11. Peace is better than war.");
        Console.WriteLine("12. Music is part of art.");
        Console.WriteLine("13. The sky is clear today.");
        Console.WriteLine("14. The mountain is very tall.");
        Console.WriteLine("15. The river flows to the sea.");
        Console.WriteLine("16. Information is power.");
        Console.WriteLine("17. The problem has a solution.");
        Console.WriteLine("18. Work is difficult but important.");
        Console.WriteLine("19. Hope defeats fear.");
        Console.WriteLine("20. Change is part of life.");
    }
}
