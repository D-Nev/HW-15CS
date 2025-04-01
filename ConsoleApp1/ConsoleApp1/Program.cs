using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        class Note
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime Created { get; set; }

            public Note(string title, string content)
            {
                Title = title;
                Content = content;
                Created = DateTime.Now;
            }
        }

        class Notemngr
        {
            private List<Note> notes = new List<Note>();
            private string directoryPath;
            private string filePath;
            public Notemngr(string path)
            {
                directoryPath = path;
                filePath = Path.Combine(directoryPath, "notes.txt");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }
            public void Addnote()
            {
                Console.Write("Заголовок: ");
                string title = Console.ReadLine();
                Console.Write("Введите текст заметки: ");
                string content = Console.ReadLine();

                notes.Add(new Note(title, content));
                SaveFile();
            }
            public void DeleteNote()
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                }
            }
            public void SaveFile()
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var note in notes)
                    {
                        writer.WriteLine($"Title: {note.Title}");
                        writer.WriteLine($"Created: {note.Created}");
                        writer.WriteLine($"Content: {note.Content}\n");
                    }
                }
            }
            public void FromFile()
            {
                if (File.Exists(filePath))
                {
                    notes.Clear();
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i += 4)
                    {
                        string title = lines[i].Replace("Title: ", "");
                        string content = lines[i + 2].Replace("Content: ", "");
                        notes.Add(new Note(title, content));
                    }
                }
            }
            public void DispNote()
            {
                foreach (var note in notes)
                {
                    Console.WriteLine($"{note.Title} ({note.Created})\n{note.Content}\n");
                }
            }
        }
        class Programs
        {
            static void Main()
            {
                Console.Write("Введите путь к папке: ");
                string path = Console.ReadLine();

                Notemngr manager = new Notemngr(path);
                manager.FromFile();

                manager.Addnote();
                manager.DispNote();
            }
        }

    }
}

