using Classes;
using Serializers;

namespace Raspredelenie
{
    internal class WorkInterface
    {

        public static void WorkMenu()
        {
            ICollection<Work> works = new List<Work>();
            int work_id;
            
            using (StreamReader reader = new StreamReader("works.txt"))
            {
                work_id = int.Parse(reader.ReadLine());
                while (!reader.EndOfStream) 
                { 
                    works.Add(WorkSerializer.CatchEntity(reader));
                }
            }

            ICollection<Worker> workers = new List<Worker>();

            using (StreamReader reader = new StreamReader("workers.txt"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream) 
                { 
                    workers.Add(WorkerSerializer.CatchEntity(reader));
                }
            }

            ICollection<WorkType> worktypes = new List<WorkType>();

            using (StreamReader reader = new StreamReader("worktypes.txt"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    worktypes.Add(WorkTypeSerializer.CatchEntity(reader));
                }
            }

            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                              "[:  (1)Список работ  |  (2)Добавить работу  |  (3)Удалить работу  |  (4)Выход в главное меню  :]\n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Введите код операции: ");
            char Code = Console.ReadKey(true).KeyChar;
            switch (Code)
            {
                case '1':
                    ShowWorks(workers, worktypes, works);
                    break;
                case '2':
                    AddWork(workers, worktypes, ref works, ref work_id);
                    break;
                case '3':
                    DeleteWork(ref works, ref work_id);
                    break;
                case '4':
                    Main.MainMenu();
                    break;
            }
        }

        internal static void ShowWorks(ICollection<Worker> workers, ICollection<WorkType> worktypes, ICollection<Work> works)
        {
            Console.Clear();
            if (works.Count == 0)
            {
                Console.WriteLine("-=-=-=-=-=-=-\n" +
                                  "| Работ нет |\n" +
                                  "-=-=-=-=-=-=-");
            }    
            else
            {
                foreach (Work work in works)
                {
                    var temp_WorkType = worktypes.Where(d => d.WorkType_ID == work.WorkType_ID).First();
                    var temp_Worker = workers.Where(d => d.Worker_ID == work.Worker_ID).First();
                    Console.WriteLine("-------------------------------------------------\n" +
                                      $"Код работы: {work.Work_ID}\n" +
                                      $"Фамилия сотрудника: {temp_Worker.SecondName}\n" +
                                      $"Имя сотрудника: {temp_Worker.FirstName}\n" +
                                      $"Отчество сотрудника: {temp_Worker.MiddleName}\n" +
                                      $"Дата начала выполнения работы: {work.DateStart}\n" +
                                      $"Дата окончания выполнения работы: {work.DateEnd}\n" +
                                      $"Дополнительная сумма к зарплате: {work.Reward}\n" +
                                      $"-------------------------------------------------\n");
                }
            }
            Console.ReadKey();
            WorkMenu();
        }
        
        internal static void AddWork(ICollection<Worker> workers, ICollection<WorkType> worktypes, ref ICollection<Work> works, ref int work_id)
        {
            Console.Clear();
            Work work = new Work();
            if (workers.Count() == 0)
            {
                Console.WriteLine("-=-=-=-=-=-=-=-=-=\n" +
                                  "| Работников нет |\n" +
                                  "-=-=-=-=-=-=-=-=-=");
                Console.ReadKey();
                WorkMenu();
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
            Console.WriteLine("Введите код работника который будет выполнять работу");
            int Code = int.Parse(Console.ReadLine());

            var temp = workers.Where(d => d.Worker_ID == Code).First();
            if (temp != null)
            {
                work.Worker_ID = Code;

            }
            Console.Clear();
            if (worktypes.Count() != 0)
            {
                foreach (WorkType worktype in worktypes)
                {
                    Console.WriteLine("------------------------------------------------\n" +
                                     $"Код вида работы: {worktype.WorkType_ID}\n" +
                                     $"Описание: {worktype.Description}\n" +
                                     $"Оплата за день: {worktype.Payment}\n" +
                                     "-------------------------------------------------\n");
                }
            }
            else
            {
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-\n" +
                                  "| Видов работ нет |\n" +
                                  "-=-=-=-=-=-=-=-=-=-");
            }
            Console.WriteLine("Введите код вида работы: ");
            int Code_type = int.Parse(Console.ReadLine());
            var temp_type = worktypes.Where(d => d.WorkType_ID == Code_type).First();
            if (temp_type !=null)
            {
                work.WorkType_ID = Code_type;

                Console.WriteLine("Введите дату начала выполнения работы: ");
                DateTime datestart = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Введите дату окончания выполнения работы: ");
                DateTime dateend = DateTime.Parse(Console.ReadLine());
                
                work.Reward = Convert.ToInt32((dateend - datestart).TotalDays)*temp_type.Payment;
                work.DateStart = datestart;
                work.DateEnd = dateend;
                work.Work_ID = work_id;
                work_id++;
                works.Add(work);
                using (StreamWriter writer = new StreamWriter("works.txt", false))
                {
                    writer.WriteLine(work_id);
                    foreach (Work _work in works)
                    {
                        writer.WriteLine(WorkSerializer.ReverseClass(_work));
                    }
                }

            }
            WorkMenu();
            

        
        }

        internal static void DeleteWork(ref ICollection<Work> works, ref int work_id)
        {
            Console.Clear();
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                              "| Введите код работы которую нужно удалить: |\n" +
                              "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            int id = int.Parse(Console.ReadLine());
            var temp = works.Where(d => d.Work_ID == id).First();
            if (temp != null)
            {
                works.Remove(temp);
                using (StreamWriter writer = new StreamWriter("works.txt", false))
                {
                    writer.WriteLine(work_id);
                    foreach (Work _work in works)
                    {
                        writer.WriteLine(WorkSerializer.ReverseClass(_work));
                    }
                }
            }
            else
            {
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n" +
                                  "| Работы с таким кодом не существует |\n" +
                                  "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.ReadKey();
            }
            WorkMenu();
        }

    }
}
