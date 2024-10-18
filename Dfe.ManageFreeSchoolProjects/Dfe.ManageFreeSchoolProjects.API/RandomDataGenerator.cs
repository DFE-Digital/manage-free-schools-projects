using System.Reflection;

namespace Dfe.ManageFreeSchoolProjects.API;

public class RandomDataGenerator
{
    private static Random random = new Random();

    // Method to generate a random string
    private static string GenerateRandomString(int length = 10)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    // Method to generate a random DateTime between a range
    private static DateTime GenerateRandomDate()
    {
        int range = (DateTime.Today - DateTime.MinValue).Days;
        return DateTime.MinValue.AddDays(random.Next(range));
    }

    // Method to generate random int
    private static int GenerateRandomInt(int min = 1, int max = 1000)
    {
        return random.Next(min, max);
    }

    // Method to generate random nullable int
    private static int? GenerateRandomNullableInt()
    {
        return random.Next(0, 2) == 1 ? (int?)GenerateRandomInt() : null;
    }

    // Method to generate random nullable DateTime
    private static DateTime? GenerateRandomNullableDate()
    {
        return random.Next(0, 2) == 1 ? (DateTime?)GenerateRandomDate() : null;
    }

    public static T GenerateRandomValues<T>() where T : new()
    {
        T obj = new T();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            if (!prop.CanWrite) continue;

            var propType = prop.PropertyType;

            // Check the type of the property and assign random value accordingly
            if (propType == typeof(string))
            {
                prop.SetValue(obj, GenerateRandomString());
            }
            else if (propType == typeof(int))
            {
                prop.SetValue(obj, GenerateRandomInt());
            }
            else if (propType == typeof(int?))
            {
                prop.SetValue(obj, GenerateRandomNullableInt());
            }
            else if (propType == typeof(DateTime))
            {
                prop.SetValue(obj, GenerateRandomDate());
            }
            else if (propType == typeof(DateTime?))
            {
                prop.SetValue(obj, GenerateRandomNullableDate());
            }
            // Add more types as necessary
        }

        return obj;
    }
}