using System;
using System.Collections.Generic;

class BalanceChecker
{
    static void Main()
    {
        Console.Write("Ingrese una expresión matemática: ");
        string expresion = Console.ReadLine() ?? "";

        bool ok = EstaBalanceada(expresion, out string detalle);

        Console.WriteLine(ok ? "Salida esperada: Fórmula balanceada."
                             : $"Salida esperada: Fórmula NO balanceada. {detalle}");
    }

    /// <summary>
    /// Verifica el balanceo de (), {}, [] usando una pila.
    /// Además entrega un detalle si detecta un error.
    /// </summary>
    static bool EstaBalanceada(string texto, out string detalle)
    {
        detalle = "";

        // Mapa de cierres -> aperturas esperadas
        var esperado = new Dictionary<char, char>
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' }
        };

        var pila = new Stack<char>();

        for (int i = 0; i < texto.Length; i++)
        {
            char ch = texto[i];

            // Apilamos solo símbolos de apertura
            if (ch == '(' || ch == '[' || ch == '{')
            {
                pila.Push(ch);
                continue;
            }

            // Si es cierre, validamos contra el tope
            if (esperado.ContainsKey(ch))
            {
                if (!pila.TryPeek(out char tope))
                {
                    detalle = $"(Error en posición {i}: se encontró '{ch}' sin nada que cerrar)";
                    return false;
                }

                if (tope != esperado[ch])
                {
                    detalle = $"(Error en posición {i}: '{ch}' no corresponde a '{tope}')";
                    return false;
                }

                pila.Pop();
            }
        }

        if (pila.Count > 0)
        {
            detalle = $"(Error: quedaron símbolos de apertura sin cerrar. Pendientes: {pila.Count})";
            return false;
        }

        return true;
    }
} 
