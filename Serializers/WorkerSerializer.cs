using Classes;

namespace Serializers
{
    public class WorkerSerializer
    {
        public static Worker CatchEntity(StreamReader fs)
        {
            string line = fs.ReadLine();
            string[] mas = line.Split(',');
            Worker _new = new Worker();
            _new.Worker_ID = int.Parse(mas[0]);
            _new.SecondName = mas[1];
            _new.FirstName = mas[2];
            _new.MiddleName = mas[3];
            _new.Salary = int.Parse(mas[4]);
            return _new;
        
        }

        public static string ReverseClass(Worker entity)
        {
            string[] mas = new string[5];
            mas[0] = entity.Worker_ID.ToString();
            mas[1] = entity.SecondName.ToString();
            mas[2] = entity.FirstName.ToString();
            mas[3] = entity.MiddleName.ToString();
            mas[4] = entity.Salary.ToString();
            string line = mas[0] + "," + mas[1] + "," + mas[2] + "," + mas[3] + "," + mas[4];
            return line;
        }

    }
}
