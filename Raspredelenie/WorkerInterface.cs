using Classes;
using Serializers;


namespace Raspredelenie
{
    internal class WorkerInterface
    {
        public static void WorkerMenu()
        {
            ICollection<Worker> workers = new List<Worker>();
            int worker_id;

            using (StreamReader reader = new StreamReader("workers.txt"))
            {
                worker_id = int.Parse(reader.ReadLine());
                while (!reader.EndOfStream) 
                {
                    workers.Add(WorkerSerializer.CatchEntity(reader));                
                }
            }
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                              "[:  (1)Показать работников  |  (2)Добавить работника  |  (3)Удалить работника  |  (4)Вернуться в главное меню  :]\n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Введите код операции: ");
            char Code = Console.ReadKey(true).KeyChar;
            switch (Code)
            {
                case '1':
                    ShowWorkers(ref workers);
                    break;
                case '2':
                    AddWorker(ref workers, ref worker_id);
                    break;
                case '3':
                    DeleteWorker(ref workers, ref worker_id);
                    break;
                case '4':
                    Main.MainMenu();
                    break;
            }
        }

        internal static void ShowWorkers(ref ICollection<Worker> workers)
        {
            Console.Clear();
            if (workers.Count() == 0)
            {
                Console.WriteLine("-=-=-=-=-=-=-=-=-=\n" +
                                  "| Работников нет |\n" +
                                  "-=-=-=-=-=-=-=-=-=");
            }
            else
            {
                foreach (Worker worker in workers)
                {
                    Console.WriteLine("-------------------------------------------------\n" +
                                      $"Код работника: {worker.Worker_ID}\n" +
                                      $"Фамилия: {worker.SecondName}\n" +
                                      $"Имя: {worker.FirstName}\n" +
                                      $"Отчество: {worker.MiddleName}\n" +
                                      $"Оклад: {worker.Salary}\n" +
                                      "-------------------------------------------------\n");
                }
            }
            Console.ReadKey();
            WorkerMenu();
        }

        static void AddWorker(ref ICollection<Worker> workers, ref int worker_id)
        {
            Console.Clear();
            Worker worker = new Worker();
            C0:
            Console.WriteLine("Введите фамилию: ");
            worker.SecondName = Console.ReadLine();
            if (worker.SecondName.Any(c => char.IsNumber(c)||worker.SecondName.Any(c => char.IsPunctuation(c))))
            {
                Console.WriteLine("В данной строке не может быть цифр или спец. символов\n");
                goto C0;
            }
            C1:
            Console.WriteLine("Введите имя: ");
            worker.FirstName = Console.ReadLine();
            if (worker.FirstName.Any(c => char.IsNumber(c) || worker.FirstName.Any(c => char.IsPunctuation(c))))
            {
                Console.WriteLine("В данной строке не может быть цифр или спец. символов\n");
                goto C1;
            }
            C2:
            Console.WriteLine("Введите отчество: ");
            worker.MiddleName = Console.ReadLine();
            if (worker.MiddleName.Any(c => char.IsNumber(c) || worker.MiddleName.Any(c => char.IsPunctuation(c))))
            {
                Console.WriteLine("В данной строке не может быть цифр или спец. символов\n");
                goto C2;
            }

            Console.WriteLine("Введите оклад: ");
            worker.Salary = decimal.Parse(Console.ReadLine());

            worker.Worker_ID = worker_id;
            worker_id++;
            workers.Add(worker);
            using (StreamWriter writer = new StreamWriter("workers.txt", false))
            {
                writer.WriteLine(worker_id);
                foreach (Worker _worker in workers)
                {
                    writer.WriteLine(WorkerSerializer.ReverseClass(_worker));
                }
            }
            WorkerMenu();
        }

        static void DeleteWorker(ref ICollection<Worker> workers, ref int worker_id)
        {
            Console.Clear();
            foreach (Worker worker in workers)
            {
                
                Console.WriteLine("-------------------------------------------------\n" +
                                  $"Код работника: {worker.Worker_ID}\n" +
                                  $"Фамилия: {worker.SecondName}\n" +
                                  $"Имя: {worker.FirstName}\n" +
                                  $"Отчество: {worker.MiddleName}\n" +
                                  $"Оклад: {worker.Salary}\n" +
                                  "-------------------------------------------------\n");
            }
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                              "| Введите код работника которого нужно удалить: |\n" +
                              "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            int id = int.Parse(Console.ReadLine());
            var temp = workers.Where(d => d.Worker_ID == id).First();
            if (temp is not null)
            {
                workers.Remove(temp);
                using (StreamWriter writer =new StreamWriter("workers.txt", false))
                {
                    writer.WriteLine(worker_id);
                    foreach (Worker _worker in workers)
                    {
                        writer.WriteLine(WorkerSerializer.ReverseClass(_worker));
                    }
                }
            }
            else
            {
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                                  "| Работника с таким кодом не существует |\n" +
                                  "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.ReadKey();
            }
            WorkerMenu();
        }
    }
}
