using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Practica_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener la expresión ingresada en el RichTextBox
            string expresion = richTextBox1.Text;

            try
            {
                // Crear el analizador sintáctico y evaluar la expresión
                Nodo arbol = AnalizadorSintactico.Parse(expresion);
                double resultado = arbol.Evaluar();

                // Mostrar el resultado en el Label
                label4.Text = $"Resultado: {resultado}";

                // Mostrar el recorrido del árbol en el otro Label
                label3.Text = ObtenerRecorrido(arbol);
            }
            catch (Exception ex)
            {
                // Manejar errores (por ejemplo, división entre cero)
                label4.Text = $"\nError: {ex.Message}";
                label3.Text = string.Empty;
            }
        }

        // Método para obtener el recorrido del árbol en preorden
        private string ObtenerRecorrido(Nodo nodo)
        {
            if (nodo is Hoja hoja)
            {
                return $"{hoja.Tipo}({hoja.Valor})";
            }
            else if (nodo is OperacionBinaria operacion)
            {
                string izquierda = ObtenerRecorrido(operacion.Izquierda);
                string derecha = ObtenerRecorrido(operacion.Derecha);
                return $"{operacion.Operador}({izquierda}, {derecha})";
            }
            else
            {
                return "Nodo desconocido";
            }
        }
    }

    // Clases de nodos y analizador sintáctico
    public abstract class Nodo
    {
        public abstract double Evaluar();
    }

    public class Hoja : Nodo
    {
        public string Tipo { get; }
        public double Valor { get; }

        public Hoja(string tipo, double valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public override double Evaluar()
        {
            return Valor;
        }
    }

    public class OperacionBinaria : Nodo
    {
        public Nodo Izquierda { get; }
        public Nodo Derecha { get; }
        public char Operador { get; }

        public OperacionBinaria(Nodo izquierda, Nodo derecha, char operador)
        {
            Izquierda = izquierda;
            Derecha = derecha;
            Operador = operador;
        }

        public override double Evaluar()
        {
            double izq = Izquierda.Evaluar();
            double der = Derecha.Evaluar();

            switch (Operador)
            {
                case '+':
                    return izq + der;
                case '-':
                    return izq - der;
                case '*':
                    return izq * der;
                case '/':
                    if (der != 0)
                        return izq / der;
                    else
                        throw new DivideByZeroException("Error: División entre cero.");
                default:
                    throw new ArgumentException($"Operador no válido: {Operador}");
            }
        }
    }

    public static class AnalizadorSintactico
    {
        private static List<string> tokens;
        private static int index;

        public static Nodo Parse(string expresion)
        {
            // Tokeniza la expresión
            tokens = Tokenizar(expresion);
            index = 0;

            // Construye el árbol de análisis sintáctico
            return Expresion();
        }

        private static List<string> Tokenizar(string expresion)
        {
            List<string> tokens = new List<string>();
            int i = 0;

            while (i < expresion.Length)
            {
                char c = expresion[i];

                // Ignorar espacios en blanco
                if (char.IsWhiteSpace(c))
                {
                    i++;
                    continue;
                }

                // Operadores
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                // Paréntesis
                else if (c == '(' || c == ')')
                {
                    tokens.Add(c.ToString());
                    i++;
                }
                // Números
                else if (char.IsDigit(c))
                {
                    int j = i;
                    while (j < expresion.Length && char.IsDigit(expresion[j]))
                        j++;

                    string numero = expresion.Substring(i, j - i);
                    tokens.Add(numero);
                    i = j;
                }
                else
                {
                    // Error: Token no válido
                    throw new ArgumentException($"Error: Token no válido en posición {i}");
                }
            }

            return tokens;
        }

        private static Nodo Expresion()
        {
            Nodo izquierda = Termino();

            while (index < tokens.Count && (tokens[index] == "+" || tokens[index] == "-" || tokens[index] == "*" || tokens[index] == "/"))
            {
                char operador = tokens[index][0];
                index++;

                Nodo derecha = Termino();
                izquierda = new OperacionBinaria(izquierda, derecha, operador);
            }

            return izquierda;
        }

        private static Nodo Termino()
        {
            if (tokens[index] == "(")
            {
                index++;
                Nodo expresion = Expresion();
                if (tokens[index] != ")")
                    throw new ArgumentException("Error: Paréntesis desbalanceados");
                index++;
                return expresion;
            }
            else if (EsNumero(tokens[index]))
            {
                double valor = double.Parse(tokens[index]);
                index++;
                return new Hoja("num", valor);
            }
            else
            {
                throw new ArgumentException($"Error: Token no válido: {tokens[index]}");
            }
        }

        private static bool EsNumero(string token)
        {
            // Intenta convertir el token a un número
            return double.TryParse(token, out _);
        }
    }
}
