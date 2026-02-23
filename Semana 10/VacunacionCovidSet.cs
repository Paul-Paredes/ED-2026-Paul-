using System;
using System.Collections.Generic;
using System.Linq;

namespace VacunacionCovidSet
{
    class Ciudadano
    {
        public string Nombre { get; set; } = "";
        public override string ToString() => Nombre;
    }

    class Program
    {
        static void Main()
        {
            // 1) Universo U: 500 ciudadanos
            var universo = GenerarCiudadanos(500);

            // 2) Tamaños requeridos
            int totalPfizer = 75;   // |P|
            int totalAstra = 75;    // |A|

            // 3) Traslape para que exista intersección (ambas dosis / ambas marcas)
            int traslape = 25;      // |P ∩ A|  (ajusta si deseas)

            // Validación básica
            traslape = Math.Max(0, Math.Min(traslape, Math.Min(totalPfizer, totalAstra)));

            // 4) Selección aleatoria sin repetir
            var rnd = new Random();
            var mezclados = universo.OrderBy(_ => rnd.Next()).ToList();

            // Construir P (75)
            var P = new HashSet<Ciudadano>(mezclados.Take(totalPfizer));

            // Elegir intersección desde P (traslape)
            var interseccion = P.OrderBy(_ => rnd.Next()).Take(traslape).ToList();

            // Construir A (75) = intersección + (75 - traslape) nuevos no Pfizer
            var candidatos = mezclados.Where(c => !P.Contains(c)).ToList();
            var soloA = candidatos.OrderBy(_ => rnd.Next()).Take(totalAstra - traslape).ToList();
            var A = new HashSet<Ciudadano>(interseccion.Concat(soloA));

            // 5) Operaciones de teoría de conjuntos
            // Unión: P ∪ A
            var union = new HashSet<Ciudadano>(P);
            union.UnionWith(A);

            // Intersección: P ∩ A  (ambas dosis / ambas marcas)
            var ambasDosis = new HashSet<Ciudadano>(P);
            ambasDosis.IntersectWith(A);

            // Solo Pfizer: P \ A
            var soloPfizer = new HashSet<Ciudadano>(P);
            soloPfizer.ExceptWith(A);

            // Solo AstraZeneca: A \ P
            var soloAstra = new HashSet<Ciudadano>(A);
            soloAstra.ExceptWith(P);

            // No vacunados: U \ (P ∪ A)
            var noVacunados = new HashSet<Ciudadano>(universo);
            noVacunados.ExceptWith(union);

            // 6) Salidas (listados)
            ImprimirListado("Ciudadanos que no se han vacunado  (U \\ (P ∪ A))", noVacunados);
            ImprimirListado("Ciudadanos que han recibido ambas dosis  (P ∩ A)", ambasDosis);
            ImprimirListado("Ciudadanos que solo han recibido Pfizer  (P \\ A)", soloPfizer);
            ImprimirListado("Ciudadanos que solo han recibido AstraZeneca  (A \\ P)", soloAstra);

            // Control de tamaños (para demostrar que cumples el enunciado)
            Console.WriteLine("\n--- CONTROL ---");
            Console.WriteLine($"Total ciudadanos |U| = {universo.Count}");
            Console.WriteLine($"Vacunados Pfizer |P| = {P.Count}");
            Console.WriteLine($"Vacunados AstraZeneca |A| = {A.Count}");
            Console.WriteLine($"Intersección |P ∩ A| = {ambasDosis.Count}");
            Console.WriteLine($"Unión |P ∪ A| = {union.Count}");
            Console.WriteLine($"No vacunados |U \\ (P ∪ A)| = {noVacunados.Count}");
        }

        static List<Ciudadano> GenerarCiudadanos(int total)
        {
            // Puedes usar nombres reales o "Ciudadano 1..500".
            // Para que sea 100% simple y original:
            var lista = new List<Ciudadano>(total);
            for (int i = 1; i <= total; i++)
            {
                lista.Add(new Ciudadano { Nombre = $"Ciudadano {i}" });
            }
            return lista;
        }

        static void ImprimirListado(string titulo, IEnumerable<Ciudadano> conjunto)
        {
            var lista = conjunto.OrderBy(c => c.Nombre).ToList();
            Console.WriteLine($"\n{titulo}: {lista.Count}");
            foreach (var c in lista)
                Console.WriteLine($"- {c.Nombre}");
        }
    }
}
