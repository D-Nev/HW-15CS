using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Введите путь к файлу: ");
            string filePath = Console.ReadLine();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    string content = Encoding.UTF8.GetString(buffer);

                    string[] numbers = content.Split(',');
                    int sum = 0;

                    foreach (var num in numbers)
                    {
                        if (int.TryParse(num.Trim(), out int value))
                        {
                            sum += value;
                        }
                    }

                    Console.WriteLine($"Сумма чисел: {sum}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
