using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulačka
{
    class Program
    {
        private static Double z = new double();
        private static void Main(string[] args)
        {
            bool chain = false;
            bool exitCode = false;
            while (exitCode == false)
            {
                Console.Clear();
                string xStr = "0";
                if (!chain)
                {
                    Console.WriteLine("Calculator, enter number");
                    xStr = Console.ReadLine();
                }
                double x;
                if (double.TryParse(xStr, out x))
                {
                    if (chain) x = z;
                    Console.WriteLine("Operation (*,/,+,-,pow,root)");
                    string inp = Console.ReadLine();
                    if (inp.Equals("+") || inp.Equals("-") || inp.Equals("/") || inp.Equals("*") || inp.Equals("pow") || inp.Equals("root"))
                    {
                        Console.WriteLine("2nd number");
                        double y;
                        if (double.TryParse(Console.ReadLine(), out y))
                        {
                            if (!inp.Equals("/"))
                            {
                                Calc(x, y, inp);
                                chain = true;
                            }
                            else if (x != 0 && y != 0)
                            {
                                Calc(x, y, inp);
                                chain = true;
                            }
                            else Console.WriteLine("Cant divide by zero");
                        }
                    }
                }
                Console.WriteLine("ESC to Exit, C to Clear");
                ConsoleKey w = Console.ReadKey().Key;
                if (w == ConsoleKey.Escape) exitCode = true;
                else if (w == ConsoleKey.C) chain = false;
            }
        }
        static void Calc(double a, double b, string toDo)
        {
            switch (toDo)
            {
                case "*":
                    Res(a * b);
                    break;
                case "/":
                    Res(a / b);
                    break;
                case "-":
                    Res(a - b);
                    break;
                case "+":
                    Res(a + b);
                    break;
                case "pow":
                    Res(Math.Pow(a, b));
                    break;
                case "root":
                    Res(Math.Pow(a, 1 / b));
                    break;
                default:
                    Console.WriteLine("Fatal error");
                    break;
            }
        }
        private static void Res(double a)
        {
            z = a;
            Console.WriteLine(z);
        }
    }
}
