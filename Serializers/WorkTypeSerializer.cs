using Classes;

namespace Serializers
{
    public class WorkTypeSerializer
    {

        public static WorkType CatchEntity(StreamReader fs)
        {
            string line = fs.ReadLine();
            string[] mas = line.Split(',');
            WorkType _new = new WorkType();
            _new.WorkType_ID = int.Parse(mas[0]);
            _new.Description = mas[1];
            _new.Payment = int.Parse(mas[2]);
            return _new;

        }

        public static string ReverseClass(WorkType entity)
        {
            string[] mas = new string[3];
            mas[0] = entity.WorkType_ID.ToString();
            mas[1] = entity.Description.ToString();
            mas[2] = entity.Payment.ToString();
            string line = mas[0] + "," + mas[1] + "," + mas[2];
            return line;
        }

    }
}
