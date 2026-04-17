namespace itm.csharp.basic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese su nombre:");
            string nombre = Console.ReadLine();
            Console.WriteLine($"¡Hola, {nombre}! Por favor, elige un ejercicio del siguiente menú:");
            Menu.MostrarMenu();
            while (true)
            {
                string entrada = Console.ReadLine().ToLower();
                if (entrada == "q" || entrada == "exit")
                {
                    break; // Salir del bucle
                }

                if (int.TryParse(entrada, out int eleccion) && eleccion >= 1 && eleccion <= 15)
                {
                    switch (eleccion)
                    {
                        case 1:
                            var Challenge1 = new Challenge1();
                            Challenge1.Run();
                            break;
                        case 2:
                            var Challenge2 = new Challenge2();
                            Challenge2.Run();
                            break;
                        case 3:
                            var Challenge3 = new Challenge3();
                            Challenge3.Run();
                            break;
                        case 4:
                            var Challenge4 = new Challenge4();
                            Challenge4.Run();
                            break;
                        case 5:
                            var Challenge5 = new Challenge5();
                            Challenge5.Run();
                            break;
                        case 6:
                            var Challenge6 = new Challenge6();
                            Challenge6.Run();
                            break;
                        case 7:
                            var Challenge7 = new Challenge7();
                            Challenge7.Run();
                            break;
                        case 8:
                            var Challenge8 = new Challenge8();
                            Challenge8.Run();
                            break;
                        case 9:
                            var Challenge9 = new Challenge9();
                            Challenge9.Run();
                            break;
                        case 10:
                            var Challenge10 = new Challenge10();
                            Challenge10.Run();
                            break;
                        case 11:
                            var Challenge = new Challenge11();
                            Challenge.Run();
                            break;
                        case 12:
                            var Challenge11 = new Challenge12();
                            Challenge11.Run();
                            break;
                        case 13:
                            var Challenge13 = new Challenge13();
                            Challenge13.Run();
                            break;
                        case 14:
                            var Challenge14 = new Challenge14();
                            Challenge14.Run();
                            break;
                        case 15:
                             var Challenge15 = new Challenge15();
                            Challenge15.Run();
                            break;
                        default:
                            Console.WriteLine("Presiona una tecla para continuar...");
                            Console.ReadKey();
                            Console.Clear();
                            Menu.MostrarMenu();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opción no válida. Por favor, ingrese un número entre 1 y 15, o 'q' o 'exit' para salir.");
                }

            }
            Console.WriteLine("¡Hasta luego!");
        }
    }
}