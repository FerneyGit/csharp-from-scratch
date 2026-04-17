using System;
using System.Globalization;
using System.Numerics;

namespace itm.csharp.basic
{
    public static class ChallengeHelpers
    {
        public static void FinalizarYVolverAlMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey(true);
            Console.Clear();
            Menu.MostrarMenu();
        }

        public static int LeerEntero(string mensaje)
        {
            while (true)
            {
                Console.WriteLine(mensaje);
                string? entrada = Console.ReadLine();

                if (int.TryParse(entrada, NumberStyles.Integer, CultureInfo.CurrentCulture, out int valor) ||
                    int.TryParse(entrada, NumberStyles.Integer, CultureInfo.InvariantCulture, out valor))
                {
                    return valor;
                }

                Console.WriteLine("Entrada no válida. Intenta de nuevo.");
            }
        }

        public static decimal LeerDecimal(string mensaje)
        {
            while (true)
            {
                Console.WriteLine(mensaje);
                string? entrada = Console.ReadLine();

                if (TryParseDecimalFlexible(entrada, out decimal valor))
                {
                    return valor;
                }

                Console.WriteLine("Entrada no válida. Intenta de nuevo.");
            }
        }

        public static double LeerDouble(string mensaje)
        {
            return (double)LeerDecimal(mensaje);
        }

        public static string LeerTexto(string mensaje)
        {
            Console.WriteLine(mensaje);
            return (Console.ReadLine() ?? string.Empty).Trim();
        }

        public static Fraction LeerFraccion(string mensaje)
        {
            while (true)
            {
                Console.WriteLine(mensaje);
                string? entrada = Console.ReadLine();

                if (Fraction.TryParse(entrada, out Fraction fraccion))
                {
                    return fraccion;
                }

                Console.WriteLine("Fracción no válida. Usa el formato a/b, por ejemplo 1/2.");
            }
        }

        public static string FormatearNumero(decimal valor)
        {
            if (valor == decimal.Truncate(valor))
            {
                return decimal.ToInt64(valor).ToString(CultureInfo.InvariantCulture);
            }

            return valor.ToString("0.##", CultureInfo.InvariantCulture);
        }

        public static string FormatearNumero(double valor)
        {
            if (Math.Abs(valor - Math.Round(valor)) < 0.0000001)
            {
                return Math.Round(valor).ToString(CultureInfo.InvariantCulture);
            }

            return valor.ToString("0.##", CultureInfo.InvariantCulture);
        }

        public static int ContarVocales(string texto)
        {
            int contador = 0;
            string vocales = "aeiouáéíóúü";

            foreach (char c in texto.ToLowerInvariant())
            {
                if (vocales.Contains(c))
                {
                    contador++;
                }
            }

            return contador;
        }

        private static bool TryParseDecimalFlexible(string? entrada, out decimal valor)
        {
            valor = default;

            if (string.IsNullOrWhiteSpace(entrada))
            {
                return false;
            }

            entrada = entrada.Trim();

            if (decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.CurrentCulture, out valor))
            {
                return true;
            }

            string normalizada = entrada.Replace(',', '.');

            return decimal.TryParse(normalizada, NumberStyles.Number, CultureInfo.InvariantCulture, out valor);
        }
    }

    public readonly struct Fraction
    {
        public long Numerator { get; }
        public long Denominator { get; }

        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException("El denominador no puede ser cero.");
            }

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            long gcd = Gcd(Math.Abs(numerator), denominator);

            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        public static bool TryParse(string? texto, out Fraction fraccion)
        {
            fraccion = default;

            if (string.IsNullOrWhiteSpace(texto))
            {
                return false;
            }

            texto = texto.Trim();

            if (texto.Contains('/'))
            {
                string[] partes = texto.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (partes.Length != 2)
                {
                    return false;
                }

                if (long.TryParse(partes[0], NumberStyles.Integer, CultureInfo.CurrentCulture, out long numerador) &&
                    long.TryParse(partes[1], NumberStyles.Integer, CultureInfo.CurrentCulture, out long denominador))
                {
                    if (denominador == 0)
                    {
                        return false;
                    }

                    fraccion = new Fraction(numerador, denominador);
                    return true;
                }

                if (long.TryParse(partes[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out numerador) &&
                    long.TryParse(partes[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out denominador))
                {
                    if (denominador == 0)
                    {
                        return false;
                    }

                    fraccion = new Fraction(numerador, denominador);
                    return true;
                }

                return false;
            }

            if (long.TryParse(texto, NumberStyles.Integer, CultureInfo.CurrentCulture, out long entero))
            {
                fraccion = new Fraction(entero, 1);
                return true;
            }

            if (long.TryParse(texto, NumberStyles.Integer, CultureInfo.InvariantCulture, out entero))
            {
                fraccion = new Fraction(entero, 1);
                return true;
            }

            return false;
        }

        public static Fraction DiferenciaAbsoluta(Fraction a, Fraction b)
        {
            long numerador = Math.Abs((a.Numerator * b.Denominator) - (b.Numerator * a.Denominator));
            long denominador = a.Denominator * b.Denominator;

            return new Fraction(numerador, denominador);
        }

        public override string ToString()
        {
            return Denominator == 1
                ? Numerator.ToString(CultureInfo.InvariantCulture)
                : $"{Numerator}/{Denominator}";
        }

        private static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                long temp = a % b;
                a = b;
                b = temp;
            }

            return a == 0 ? 1 : a;
        }
    }

    public class Challenge1
    {
        public void Run()
        {
            decimal numero = ChallengeHelpers.LeerDecimal("Ejercicio 1 - Ingresa un número:");

            if (numero < 0)
            {
                Console.WriteLine("Resultado: Número negativo.");
            }
            else
            {
                Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(numero * numero)}");
            }

            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge2
    {
        public void Run()
        {
            decimal primero = ChallengeHelpers.LeerDecimal("Ejercicio 2 - Ingresa el primer número:");
            decimal segundo = ChallengeHelpers.LeerDecimal("Ejercicio 2 - Ingresa el segundo número:");

            decimal resultado = primero > segundo ? primero * 2 : segundo * 3;

            Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(resultado)}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge3
    {
        public void Run()
        {
            double numero = ChallengeHelpers.LeerDouble("Ejercicio 3 - Ingresa un número:");

            if (numero > 0)
            {
                double raiz = Math.Sqrt(numero);
                Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(raiz)}");
            }
            else
            {
                Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(numero * numero)}");
            }

            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge4
    {
        public void Run()
        {
            double radio = ChallengeHelpers.LeerDouble("Ejercicio 4 - Ingresa el radio del círculo:");
            double perimetro = 2 * Math.PI * Math.Abs(radio);

            Console.WriteLine($"Resultado: {perimetro:0.00}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge5
    {
        public void Run()
        {
            int dia = ChallengeHelpers.LeerEntero("Ejercicio 5 - Ingresa un número entre 1 y 7:");

            string resultado = dia switch
            {
                1 => "Lunes",
                2 => "Martes",
                3 => "Miércoles",
                4 => "Jueves",
                5 => "Viernes",
                _ => "Número fuera del rango laboral."
            };

            Console.WriteLine($"Resultado: {resultado}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge6
    {
        public void Run()
        {
            decimal salarioAnual = ChallengeHelpers.LeerDecimal("Ejercicio 6 - Ingresa tu salario anual:");

            if (salarioAnual > 12000m)
            {
                decimal impuesto = (salarioAnual - 12000m) * 0.15m;
                Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(impuesto)}");
            }
            else
            {
                Console.WriteLine("Resultado: No debe impuestos.");
            }

            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge7
    {
        public void Run()
        {
            decimal primero = ChallengeHelpers.LeerDecimal("Ejercicio 7 - Ingresa el primer número:");
            decimal segundo = ChallengeHelpers.LeerDecimal("Ejercicio 7 - Ingresa el segundo número:");

            if (segundo == 0)
            {
                Console.WriteLine("Resultado: No se puede dividir entre cero.");
            }
            else
            {
                decimal residuo = primero % segundo;
                Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(residuo)}");
            }

            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge8
    {
        public void Run()
        {
            int suma = 0;

            for (int i = 2; i <= 50; i += 2)
            {
                suma += i;
            }

            Console.WriteLine($"Resultado: {suma}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge9
    {
        public void Run()
        {
            Fraction fraccion1 = ChallengeHelpers.LeerFraccion("Ejercicio 9 - Ingresa la primera fracción (ejemplo 1/2):");
            Fraction fraccion2 = ChallengeHelpers.LeerFraccion("Ejercicio 9 - Ingresa la segunda fracción (ejemplo 1/3):");

            Fraction diferencia = Fraction.DiferenciaAbsoluta(fraccion1, fraccion2);

            Console.WriteLine($"Resultado: {diferencia}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge10
    {
        public void Run()
        {
            string palabra = ChallengeHelpers.LeerTexto("Ejercicio 10 - Ingresa una palabra:");
            Console.WriteLine($"Resultado: {palabra.Length}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge11
    {
        public void Run()
        {
            decimal suma = 0m;

            for (int i = 1; i <= 4; i++)
            {
                suma += ChallengeHelpers.LeerDecimal($"Ejercicio 11 - Ingresa el número {i}:");
            }

            decimal promedio = suma / 4m;

            Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(promedio)}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge12
    {
        public void Run()
        {
            decimal menor = ChallengeHelpers.LeerDecimal("Ejercicio 12 - Ingresa el número 1:");

            for (int i = 2; i <= 5; i++)
            {
                decimal numero = ChallengeHelpers.LeerDecimal($"Ejercicio 12 - Ingresa el número {i}:");
                if (numero < menor)
                {
                    menor = numero;
                }
            }

            Console.WriteLine($"Resultado: {ChallengeHelpers.FormatearNumero(menor)}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge13
    {
        public void Run()
        {
            string palabra = ChallengeHelpers.LeerTexto("Ejercicio 13 - Ingresa una palabra:");
            int vocales = ChallengeHelpers.ContarVocales(palabra);

            Console.WriteLine($"Resultado: {vocales}");
            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge14
    {
        public void Run()
        {
            int numero = ChallengeHelpers.LeerEntero("Ejercicio 14 - Ingresa un número:");

            if (numero < 0)
            {
                Console.WriteLine("Resultado: No existe factorial para números negativos.");
            }
            else
            {
                BigInteger factorial = 1;

                for (int i = 2; i <= numero; i++)
                {
                    factorial *= i;
                }

                Console.WriteLine($"Resultado: {factorial}");
            }

            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }

    public class Challenge15
    {
        public void Run()
        {
            int numero = ChallengeHelpers.LeerEntero("Ejercicio 15 - Ingresa un número:");

            if (numero >= 10 && numero <= 20)
            {
                Console.WriteLine("Resultado: Está en el rango.");
            }
            else
            {
                Console.WriteLine("Resultado: Fuera del rango.");
            }

            ChallengeHelpers.FinalizarYVolverAlMenu();
        }
    }
}