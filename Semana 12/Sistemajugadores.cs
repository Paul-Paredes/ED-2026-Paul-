using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TorneoFutbolConjuntosMapas
{
    // Representa a un jugador dentro del torneo.
    // La igualdad se basa en el ID para evitar duplicados en HashSet.
    public class Jugador : IEquatable<Jugador>
    {
        public string Id { get; }
        public string Nombre { get; }
        public string Posicion { get; }
        public int Edad { get; }

        public Jugador(string id, string nombre, string posicion, int edad)
        {
            Id = id?.Trim() ?? "";
            Nombre = nombre?.Trim() ?? "";
            Posicion = posicion?.Trim() ?? "";
            Edad = edad;
        }

        public bool Equals(Jugador? other)
        {
            if (other is null) return false;
            return string.Equals(Id, other.Id, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as Jugador);

        public override int GetHashCode()
            => StringComparer.OrdinalIgnoreCase.GetHashCode(Id);

        public override string ToString()
            => $"ID: {Id} | Nombre: {Nombre} | Posición: {Posicion} | Edad: {Edad}";
    }

    internal class Program
    {
        // Mapa (diccionario): Equipo -> Conjunto de jugadores
        private static readonly Dictionary<string, HashSet<Jugador>> equipos =
            new Dictionary<string, HashSet<Jugador>>(StringComparer.OrdinalIgnoreCase);

        // Conjunto global para evitar que un jugador exista en más de un equipo (o repetido en todo el sistema)
        private static readonly HashSet<string> idsJugadores =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool salir = false;
            while (!salir)
            {
                MostrarMenu();
                Console.Write("Seleccione una opción: ");
                string opcion = (Console.ReadLine() ?? "").Trim();

                Console.WriteLine();
                switch (opcion)
                {
                    case "1":
                        RegistrarEquipo();
                        break;
                    case "2":
                        RegistrarJugador();
                        break;
                    case "3":
                        VerEquipos();
                        break;
                    case "4":
                        VerJugadoresPorEquipo();
                        break;
                    case "5":
                        BuscarJugador();
                        break;
                    case "6":
                        EliminarJugador();
                        break;
                    case "7":
                        EliminarEquipo();
                        break;
                    case "8":
                        EstadoDelSistema();
                        break;
                    case "0":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione ENTER para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("============== MENÚ ==============");
            Console.WriteLine("1. Registrar equipo");
            Console.WriteLine("2. Registrar jugador en un equipo");
            Console.WriteLine("3. Ver equipos registrados");
            Console.WriteLine("4. Ver jugadores de un equipo");
            Console.WriteLine("5. Buscar jugador (y ver su equipo)");
            Console.WriteLine("6. Eliminar jugador");
            Console.WriteLine("7. Eliminar equipo");
            Console.WriteLine("8. Estado del sistema (reporte general)");
            Console.WriteLine("0. Salir");
            Console.WriteLine("==================================");
        }

        // ------------------ OPCIÓN 1 ------------------
        private static void RegistrarEquipo()
        {
            Console.WriteLine("=== Registrar equipo ===");
            Console.Write("Nombre del equipo: ");
            string nombreEquipo = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(nombreEquipo))
            {
                Console.WriteLine("No se puede registrar un equipo sin nombre.");
                return;
            }

            var sw = Stopwatch.StartNew();

            if (equipos.ContainsKey(nombreEquipo))
            {
                sw.Stop();
                Console.WriteLine("Ese equipo ya existe. No se registró un duplicado.");
                MostrarTiempo("Registrar equipo (ya existía)", sw);
                return;
            }

            equipos[nombreEquipo] = new HashSet<Jugador>();
            sw.Stop();

            Console.WriteLine($"Equipo '{nombreEquipo}' registrado correctamente.");
            MostrarTiempo("Registrar equipo", sw);
        }

        // ------------------ OPCIÓN 2 ------------------
        private static void RegistrarJugador()
        {
            Console.WriteLine("=== Registrar jugador ===");

            if (equipos.Count == 0)
            {
                Console.WriteLine("Primero registre al menos un equipo (opción 1).");
                return;
            }

            Console.Write("ID del jugador (único): ");
            string id = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("El ID no puede estar vacío.");
                return;
            }

            if (idsJugadores.Contains(id))
            {
                Console.WriteLine("Ese jugador ya está registrado en el sistema (ID duplicado).");
                return;
            }

            Console.Write("Nombre del jugador: ");
            string nombre = (Console.ReadLine() ?? "").Trim();

            Console.Write("Posición (ej: Arquero, Defensa, Mediocampo, Delantero): ");
            string posicion = (Console.ReadLine() ?? "").Trim();

            int edad = LeerEntero("Edad: ", min: 5, max: 70);

            Console.Write("Equipo al que pertenece: ");
            string equipo = (Console.ReadLine() ?? "").Trim();

            if (!equipos.ContainsKey(equipo))
            {
                Console.WriteLine("El equipo no existe. Revise el nombre o registre el equipo primero.");
                return;
            }

            var sw = Stopwatch.StartNew();

            Jugador nuevo = new Jugador(id, nombre, posicion, edad);

            // Como el HashSet usa Equals/GetHashCode, evita duplicados por ID
            bool agregadoAlEquipo = equipos[equipo].Add(nuevo);

            if (!agregadoAlEquipo)
            {
                sw.Stop();
                Console.WriteLine("El jugador ya existe dentro del equipo (duplicado).");
                MostrarTiempo("Registrar jugador (duplicado en equipo)", sw);
                return;
            }

            // Control global: el ID queda bloqueado para todo el sistema
            idsJugadores.Add(id);

            sw.Stop();
            Console.WriteLine($"Jugador registrado correctamente en '{equipo}'.");
            MostrarTiempo("Registrar jugador", sw);
        }

        // ------------------ OPCIÓN 3 ------------------
        private static void VerEquipos()
        {
            Console.WriteLine("=== Equipos registrados ===");

            var sw = Stopwatch.StartNew();

            if (equipos.Count == 0)
            {
                sw.Stop();
                Console.WriteLine("No hay equipos registrados.");
                MostrarTiempo("Ver equipos", sw);
                return;
            }

            int i = 1;
            foreach (var kvp in equipos.OrderBy(e => e.Key))
            {
                Console.WriteLine($"{i}. {kvp.Key} (Jugadores: {kvp.Value.Count})");
                i++;
            }

            sw.Stop();
            MostrarTiempo("Ver equipos", sw);
        }

        // ------------------ OPCIÓN 4 ------------------
        private static void VerJugadoresPorEquipo()
        {
            Console.WriteLine("=== Ver jugadores por equipo ===");
            Console.Write("Ingrese el nombre del equipo: ");
            string equipo = (Console.ReadLine() ?? "").Trim();

            var sw = Stopwatch.StartNew();

            if (!equipos.TryGetValue(equipo, out var jugadores))
            {
                sw.Stop();
                Console.WriteLine("Equipo no encontrado.");
                MostrarTiempo("Ver jugadores por equipo (no encontrado)", sw);
                return;
            }

            if (jugadores.Count == 0)
            {
                sw.Stop();
                Console.WriteLine($"El equipo '{equipo}' no tiene jugadores registrados.");
                MostrarTiempo("Ver jugadores por equipo (vacío)", sw);
                return;
            }

            Console.WriteLine($"\nEquipo: {equipo}");
            foreach (var j in jugadores.OrderBy(j => j.Nombre))
            {
                Console.WriteLine($"- {j}");
            }

            sw.Stop();
            MostrarTiempo("Ver jugadores por equipo", sw);
        }

        // ------------------ OPCIÓN 5 ------------------
        private static void BuscarJugador()
        {
            Console.WriteLine("=== Buscar jugador ===");
            Console.Write("Ingrese el ID del jugador: ");
            string id = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Debe ingresar un ID.");
                return;
            }

            var sw = Stopwatch.StartNew();

            // Verificación rápida por conjunto global
            if (!idsJugadores.Contains(id))
            {
                sw.Stop();
                Console.WriteLine("Jugador no encontrado en el sistema.");
                MostrarTiempo("Buscar jugador (no encontrado)", sw);
                return;
            }

            // Para encontrar equipo, se recorre el diccionario (consulta O(n) en número de equipos)
            foreach (var kvp in equipos)
            {
                Jugador? encontrado = kvp.Value.FirstOrDefault(j => j.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (encontrado != null)
                {
                    sw.Stop();
                    Console.WriteLine("Jugador encontrado:");
                    Console.WriteLine(encontrado);
                    Console.WriteLine($"Equipo: {kvp.Key}");
                    MostrarTiempo("Buscar jugador", sw);
                    return;
                }
            }

            // Caso raro: el ID está en el conjunto global pero no aparece en equipos (inconsistencia)
            sw.Stop();
            Console.WriteLine("Jugador no localizado en equipos (posible inconsistencia).");
            MostrarTiempo("Buscar jugador (inconsistente)", sw);
        }

        // ------------------ OPCIÓN 6 ------------------
        private static void EliminarJugador()
        {
            Console.WriteLine("=== Eliminar jugador ===");
            Console.Write("Ingrese el ID del jugador a eliminar: ");
            string id = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Debe ingresar un ID.");
                return;
            }

            var sw = Stopwatch.StartNew();

            if (!idsJugadores.Contains(id))
            {
                sw.Stop();
                Console.WriteLine("El jugador no existe en el sistema.");
                MostrarTiempo("Eliminar jugador (no existe)", sw);
                return;
            }

            foreach (var kvp in equipos)
            {
                // Remover usando búsqueda
                Jugador? encontrado = kvp.Value.FirstOrDefault(j => j.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (encontrado != null)
                {
                    kvp.Value.Remove(encontrado);
                    idsJugadores.Remove(id);

                    sw.Stop();
                    Console.WriteLine($"Jugador eliminado correctamente del equipo '{kvp.Key}'.");
                    MostrarTiempo("Eliminar jugador", sw);
                    return;
                }
            }

            // Si no se encontró en equipos, igual quitamos del conjunto global para no bloquear el ID
            idsJugadores.Remove(id);
            sw.Stop();
            Console.WriteLine("El ID estaba registrado, pero no se encontró en equipos. Se corrigió el control global.");
            MostrarTiempo("Eliminar jugador (corrección)", sw);
        }

        // ------------------ OPCIÓN 7 ------------------
        private static void EliminarEquipo()
        {
            Console.WriteLine("=== Eliminar equipo ===");
            Console.Write("Ingrese el nombre del equipo a eliminar: ");
            string equipo = (Console.ReadLine() ?? "").Trim();

            var sw = Stopwatch.StartNew();

            if (!equipos.TryGetValue(equipo, out var jugadores))
            {
                sw.Stop();
                Console.WriteLine("Equipo no encontrado.");
                MostrarTiempo("Eliminar equipo (no encontrado)", sw);
                return;
            }

            // Al eliminar el equipo, liberamos los IDs de sus jugadores del conjunto global
            foreach (var j in jugadores)
                idsJugadores.Remove(j.Id);

            equipos.Remove(equipo);

            sw.Stop();
            Console.WriteLine($"Equipo '{equipo}' eliminado correctamente (y sus jugadores quedaron eliminados del sistema).");
            MostrarTiempo("Eliminar equipo", sw);
        }

        // ------------------ OPCIÓN 8 ------------------
        private static void EstadoDelSistema()
        {
            Console.WriteLine("=== Estado del sistema ===");
            var sw = Stopwatch.StartNew();

            int totalEquipos = equipos.Count;
            int totalJugadores = equipos.Sum(e => e.Value.Count);

            Console.WriteLine($"Equipos registrados: {totalEquipos}");
            Console.WriteLine($"Jugadores registrados: {totalJugadores}");

            if (totalEquipos > 0)
            {
                Console.WriteLine("\nDetalle por equipo:");
                foreach (var kvp in equipos.OrderBy(e => e.Key))
                {
                    Console.WriteLine($"- {kvp.Key}: {kvp.Value.Count} jugador(es)");
                }
            }

            // Indicador simple de consistencia
            Console.WriteLine($"\nControl global de IDs (HashSet): {idsJugadores.Count} ID(s)");
            Console.WriteLine($"Consistencia (IDs == jugadores): {(idsJugadores.Count == totalJugadores ? "OK" : "REVISAR")}");

            sw.Stop();
            MostrarTiempo("Estado del sistema", sw);
        }

        // ------------------ UTILIDADES ------------------
        private static int LeerEntero(string mensaje, int min, int max)
        {
            while (true)
            {
                Console.Write(mensaje);
                string input = (Console.ReadLine() ?? "").Trim();

                if (int.TryParse(input, out int valor))
                {
                    if (valor < min || valor > max)
                    {
                        Console.WriteLine($"Ingrese un número entre {min} y {max}.");
                        continue;
                    }
                    return valor;
                }

                Console.WriteLine("Entrada no válida. Intente nuevamente.");
            }
        }

        private static void MostrarTiempo(string operacion, Stopwatch sw)
        {
            Console.WriteLine($"\n[Tiempo de ejecución] {operacion}: {sw.ElapsedTicks} ticks (~{sw.ElapsedMilliseconds} ms)");
        }
    }
}
