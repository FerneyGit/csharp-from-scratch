namespace HiWorld
{
    class Program
    {
        static void Main()
        {
            DateOnly dateConverted = new DateOnly ();
            string nameInput;
            string BirthdayInput;
            Console.WriteLine("¡Hola Bienvenido a la Calculadora de Años!");
            Console.WriteLine("Digite tu nombre: ");
            nameInput = Console.ReadLine();
            Console.WriteLine($"¡Un Gusto en Conocerte! {nameInput}");
            Console.WriteLine("Digite tu fecha de nacimiento en formato dd/mm/yy: ");
            BirthdayInput = Console.ReadLine();
            bool isDateValid = DateOnly.TryParse(BirthdayInput, out dateConverted);
            if (isDateValid == false) Console.WriteLine($"Su fecha de nacimiento no tiene el formato correcto {BirthdayInput} ");
            var Person = new Person
            {
                Name = nameInput,
                Birthday = dateConverted,
                Age = DateTime.Now.Year - dateConverted.Year
            };

            Console.WriteLine($"Tú nombre: {Person.Name}");
            Console.WriteLine($"Tú fecha de nacimiento: {Person.Birthday}");
            Console.WriteLine($"Tú edad es: {Person.Age}");

            Console.ReadLine();

            /*
            var Name = "Ferney Esteban";
            Console.WriteLine($"Hi World! {Name}");
            Console.WriteLine($"Thanks for comming! {Name}");
            Console.WriteLine();
            */
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateOnly Birthday { get; set; }
        }
    }
}