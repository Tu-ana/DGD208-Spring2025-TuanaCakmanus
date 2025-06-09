namespace PetSimulator
{
    public class Menu<T>
    {
        private readonly string title;
        private readonly List<MenuItem<T>> menuItems;

        public Menu(string title)
        {
            this.title = title;
            this.menuItems = new List<MenuItem<T>>();
        }

        public void AddItem(string description, T value)
        {
            menuItems.Add(new MenuItem<T>(description, value));
        }

        public T Display()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {title} ===");
                Console.WriteLine();

                for (int i = 0; i < menuItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {menuItems[i].Description}");
                }

                Console.WriteLine();
                Console.Write("Select an option (1-" + menuItems.Count + "): ");

                if (int.TryParse(Console.ReadLine(), out int choice) && 
                    choice >= 1 && choice <= menuItems.Count)
                {
                    return menuItems[choice - 1].Value;
                }

                Console.WriteLine("Invalid selection. Press any key to try again.");
                Console.ReadKey();
            }
        }
    }

    public class MenuItem<T>
    {
        public string Description { get; }
        public T Value { get; }

        public MenuItem(string description, T value)
        {
            Description = description;
            Value = value;
        }
    }
}