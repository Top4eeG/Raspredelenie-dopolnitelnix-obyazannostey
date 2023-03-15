using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspredelenie
{
    internal class Main
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "Распределение дополнительных обязанностей";
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                              "[:  (1)Работники  |  (2)Виды работ  |  (3)Работы  |  (4)Выход из программы  :]\n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Введите код операции: ");
            char Code = Console.ReadKey(true).KeyChar;
            switch (Code)
            {
                case '1':
                    WorkerInterface.WorkerMenu();
                    break;
                case '2':
                    WorkTypeInterface.WorkTypeMenu();
                    break;
                case '3':
                    WorkInterface.WorkMenu();
                    break;
                case '4':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Вы ввели неверный код, повторите ввод");
                    Thread.Sleep(1000);
                    MainMenu();
                    break;
            }
        }
    }
}
