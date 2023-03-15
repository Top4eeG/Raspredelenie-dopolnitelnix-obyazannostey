using Classes;
using Serializers;

namespace Raspredelenie
{
    internal class WorkTypeInterface
    {

        public static void WorkTypeMenu()
        {
            ICollection<WorkType> worktypes = new List<WorkType>();
            int worktype_id;

            using (StreamReader reader = new StreamReader("worktypes.txt"))
            {
                worktype_id = int.Parse(reader.ReadLine());
                while (!reader.EndOfStream)
                {
                    worktypes.Add(WorkTypeSerializer.CatchEntity(reader));
                }    
            }
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                              "[:  (1)Показать виды работ  |  (2)Добавить вид работы  |  (3)Удалить вид работы  |  (4)Выход в главное меню  :]\n" +
                              "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Введите код операции:  ");
            char Code = Console.ReadKey(true).KeyChar;
            switch (Code)
            {
                case '1':
                    ShowWorkTypes(ref worktypes);
                    break;
                case '2':
                    AddWorkType(ref worktypes, ref worktype_id);
                    break;
                case '3':
                    DeleteWorkType(ref worktypes, ref worktype_id);
                    break;
                case '4':
                    Main.MainMenu();
                    break;
            }
        }

        internal static void ShowWorkTypes(ref ICollection<WorkType> worktypes)
        {
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
            Console.ReadKey();
            WorkTypeMenu();
        }
        
        static void AddWorkType(ref ICollection<WorkType> worktypes, ref int worktype_id)
        {
            Console.Clear();
            WorkType worktype = new WorkType();
            C0:
            Console.WriteLine("Введите описание вида работы");
            worktype.Description = Console.ReadLine();
            if (worktype.Description.Any(c => char.IsNumber(c) || worktype.Description.Any(c => char.IsPunctuation(c))))
            {
                Console.WriteLine("В данной строке не может быть цифр или спец. символов\n");
                goto C0;
            }
            Console.WriteLine("Введите оплату за день");
            worktype.Payment = int.Parse(Console.ReadLine());

            worktype.WorkType_ID = worktype_id;
            worktype_id++;
            worktypes.Add(worktype);
            using (StreamWriter writer = new StreamWriter("worktypes.txt"))
            {
                writer.WriteLine(worktype_id);
                foreach (WorkType _worktype in worktypes)
                {
                    writer.WriteLine(WorkTypeSerializer.ReverseClass(_worktype));
                }
            }
            WorkTypeMenu();
        }
        static void DeleteWorkType(ref ICollection<WorkType> worktypes, ref int worktype_id)
        {
            Console.Clear();
            foreach (WorkType worktype in worktypes)
            {
                Console.WriteLine("-------------------------------------------------\n" +
                                 $"Код вида работы: {worktype.WorkType_ID}\n" +
                                 $"Описание: {worktype.Description}\n" +
                                 $"Оплата за день: {worktype.Payment}\n" +
                                 "-------------------------------------------------\n");
            }
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                              "| Введите код вида работ который нужно удалить: |\n" +
                              "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            int id = int.Parse(Console.ReadLine());
            var temp = worktypes.Where(d => d.WorkType_ID == id).First();
            if (temp !=null)
            {
                worktypes.Remove(temp);
                using (StreamWriter writer = new StreamWriter("worktypes.txt"))
                {
                    writer.WriteLine(worktype_id);
                    foreach (WorkType _worktype in worktypes)
                    {
                        writer.WriteLine(WorkTypeSerializer.ReverseClass(_worktype));
                    }    
                }    
            }
            else
            {
                Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n" +
                                  "| Вида работ с таким кодом не существует |\n" +
                                  "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
                Console.ReadKey();
            }
            WorkTypeMenu();
        }
    }
}
