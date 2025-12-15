using System;

namespace RegistroEstudiante
{
    /// <summary>
    /// Clase que representa a un estudiante con sus datos personales
    /// </summary>
    public class Estudiante
    {
        // Propiedades de la clase Estudiante
        public int ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        
        // Array para almacenar los tres números de teléfono
        public string[] Telefonos { get; set; }

        /// <summary>
        /// Constructor de la clase Estudiante
        /// </summary>
        /// <param name="id">Identificador único del estudiante</param>
        /// <param name="nombres">Nombres del estudiante</param>
        /// <param name="apellidos">Apellidos del estudiante</param>
        /// <param name="direccion">Dirección de residencia</param>
        /// <param name="telefonos">Array con tres números de teléfono</param>
        public Estudiante(int id, string nombres, string apellidos, string direccion, string[] telefonos)
        {
            ID = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            
            // Inicializar el array de teléfonos con exactamente 3 elementos
            Telefonos = new string[3];
            
            // Validar que se proporcionen exactamente 3 números
            if (telefonos.Length == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    Telefonos[i] = telefonos[i];
                }
            }
            else
            {
                Console.WriteLine("Error: Debe proporcionar exactamente 3 números de teléfono.");
            }
        }

        /// <summary>
        /// Método para mostrar toda la información del estudiante
        /// </summary>
        public void MostrarInformacion()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║       INFORMACIÓN DEL ESTUDIANTE REGISTRADO       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine($"\n  ID:              {ID}");
            Console.WriteLine($"  Nombres:         {Nombres}");
            Console.WriteLine($"  Apellidos:       {Apellidos}");
            Console.WriteLine($"  Dirección:       {Direccion}");
            Console.WriteLine("\n  Números de Teléfono:");
            
            // Recorrer el array de teléfonos y mostrar cada uno
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"    [{i + 1}] {Telefonos[i]}");
            }
            Console.WriteLine("\n════════════════════════════════════════════════════\n");
        }

        /// <summary>
        /// Método para actualizar un número de teléfono específico
        /// </summary>
        /// <param name="posicion">Posición en el array (0-2)</param>
        /// <param name="nuevoTelefono">Nuevo número de teléfono</param>
        public void ActualizarTelefono(int posicion, string nuevoTelefono)
        {
            // Validar que la posición esté en el rango válido
            if (posicion >= 0 && posicion < Telefonos.Length)
            {
                Telefonos[posicion] = nuevoTelefono;
                Console.WriteLine($"✓ Teléfono {posicion + 1} actualizado correctamente.");
            }
            else
            {
                Console.WriteLine("✗ Error: Posición inválida. Debe ser entre 1 y 3.");
            }
        }
    }

    /// <summary>
    /// Clase principal del programa
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║    SISTEMA DE REGISTRO DE ESTUDIANTES - C#        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝\n");

            // Solicitar datos del estudiante
            Console.Write("Ingrese el ID del estudiante: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Ingrese los nombres: ");
            string nombres = Console.ReadLine();

            Console.Write("Ingrese los apellidos: ");
            string apellidos = Console.ReadLine();

            Console.Write("Ingrese la dirección: ");
            string direccion = Console.ReadLine();

            // Array para almacenar los tres números de teléfono
            string[] telefonos = new string[3];
            
            Console.WriteLine("\n--- Ingrese 3 números de teléfono ---");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Teléfono {i + 1}: ");
                telefonos[i] = Console.ReadLine();
            }

            // Crear una instancia de la clase Estudiante
            Estudiante estudiante = new Estudiante(id, nombres, apellidos, direccion, telefonos);

            // Mostrar la información del estudiante
            estudiante.MostrarInformacion();

            // Demostración de actualización de teléfono
            Console.WriteLine("¿Desea actualizar algún número de teléfono? (S/N): ");
            string respuesta = Console.ReadLine().ToUpper();

            if (respuesta == "S")
            {
                Console.Write("Ingrese el número de teléfono a actualizar (1-3): ");
                int posicion = int.Parse(Console.ReadLine()) - 1; // Restar 1 porque el array inicia en 0

                Console.Write("Ingrese el nuevo número: ");
                string nuevoTelefono = Console.ReadLine();

                estudiante.ActualizarTelefono(posicion, nuevoTelefono);
                
                // Mostrar información actualizada
                estudiante.MostrarInformacion();
            }

            Console.WriteLine("\n¡Gracias por usar el sistema de registro!");
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}