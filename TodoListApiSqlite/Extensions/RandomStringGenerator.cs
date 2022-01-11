using System.Text;

namespace TodoListApiSqlite.Extensions;

public static class RandomStringGenerator
{
    
    public static string Generate(int length)
    {
        StringBuilder stringBuilder = new StringBuilder();  
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            double flt = random.NextDouble();
            int shift = Convert.ToInt32(Math.Floor(25 * flt));
            var letter = Convert.ToChar(shift + 65);
            stringBuilder.Append(letter);  
        }
        return stringBuilder.ToString();
    }
    
}