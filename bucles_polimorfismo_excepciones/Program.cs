var circ = new Circulo();

circ.Radio = 20;
var result_area = circ.Area();
var result_perim = circ.Perimetro();

Console.WriteLine($"El area es: {result_area} y el perimetro es: {result_perim} el nuevo radio es : {circ.Add10()}");

int prev = 0, next = 1, sum;
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(prev);
    sum = prev + next;
    prev = next;
    next = sum;
}

try
{
    Console.WriteLine("Número a dividir:");
    double n = double.Parse(Console.ReadLine());
    Console.WriteLine("Divisor:");
    double divisor = double.Parse(Console.ReadLine());
    if (divisor == 0)
    {
        throw new DivideByZeroException();
    }
    Console.WriteLine($"Resultado: {n / divisor}");
}
catch (DivideByZeroException)
{
    Console.WriteLine("No se puede dividir por cero!");
}
catch (Exception)
{
    Console.WriteLine("Error desconocido ingreso valores no admitidos.");
}

// Clase de figura abstracta
    /* {
    public abstract double Area();
    public abstract double Perimetro();
    } */

interface IExtraCalculations
{
    public double Add10();
}
interface IFigura
{
    public double Area();
    public double Perimetro();
}

class Circulo : IFigura, IExtraCalculations
{
    public double Radio { get; set; }
    public double Area() => Math.PI * Radio * Radio;
    public double Perimetro() => 2 * Math.PI * Radio;

    public double Add10()
    {
        return Radio + 10;
    }
}