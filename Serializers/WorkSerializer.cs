using Classes;

namespace Serializers
{
    public class WorkSerializer
    {
        public static Work CatchEntity(StreamReader fs)
        {
            string line=fs.ReadLine();
            string[] mas = line.Split(',');
            Work _new = new Work();
            _new.Work_ID = int.Parse(mas[0]);
            _new.Worker_ID = int.Parse(mas[1]);
            _new.WorkType_ID = int.Parse(mas[2]);
            _new.DateStart = DateTime.Parse(mas[3]);
            _new.DateEnd = DateTime.Parse(mas[4]);
            _new.Reward = int.Parse(mas[5]);
            return _new;

        }

        public static string ReverseClass(Work entity)
        {
            string[] mas = new string[6];
            mas[0] = entity.Work_ID.ToString();
            mas[1] = entity.Worker_ID.ToString();
            mas[2] = entity.WorkType_ID.ToString();
            mas[3] = entity.DateStart.ToString();
            mas[4] = entity.DateEnd.ToString();
            mas[5] = entity.Reward.ToString();
            string line = mas[0] + "," + mas[1] + "," + mas[2] + "," + mas[3] + "," + mas[4] + "," + mas[5];
            return line;
        }

    }
}