namespace PetSimulator
{
    public class Game
    {
        private readonly PetManager petManager;
        private bool isRunning;

        public Game()
        {
            petManager = new PetManager();
            petManager.PetAdopted += OnPetAdopted;
            petManager.PetDied += OnPetDied;
            isRunning = true;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Welcome to Pet Simulator!");
            Console.WriteLine("Press any key to start.");
            Console.ReadKey();

            while (isRunning)
            {
                await ShowMainMenuAsync();
            }

            petManager.StopAllPets();
            Console.WriteLine("Thanks for playing!");
        }

        private async Task ShowMainMenuAsync()
        {
            var mainMenu = new Menu<GameCommand>("Pet Simulator - Main Menu");
            mainMenu.AddItem("Adopt a Pet", GameCommand.AdoptPet);
            mainMenu.AddItem("View Your Pets", GameCommand.ViewPets);
            mainMenu.AddItem("Use Item on Pet", GameCommand.UseItem);
            mainMenu.AddItem("Show Credits", GameCommand.ShowCredits);
            mainMenu.AddItem("Exit Game", GameCommand.Exit);

            var choice = mainMenu.Display();

            switch (choice)
            {
                case GameCommand.AdoptPet:
                    await AdoptPetAsync();
                    break;
                case GameCommand.ViewPets:
                    ViewPets();
                    break;
                case GameCommand.UseItem:
                    await UseItemAsync();
                    break;
                case GameCommand.ShowCredits:
                    ShowCredits();
                    break;
                case GameCommand.Exit:
                    isRunning = false;
                    break;
            }
        }

        private async Task AdoptPetAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Adopt a Pet ===");
            
            var petTypeMenu = new Menu<PetType>("Choose Pet Type");
            foreach (PetType petType in Enum.GetValues<PetType>())
            {
                petTypeMenu.AddItem(petType.ToString(), petType);
            }

            var selectedPetType = petTypeMenu.Display();

            Console.Clear();
            Console.WriteLine($"=== Name Your {selectedPetType} ===");
            Console.Write("Enter pet name: ");
            var petName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(petName))
            {
                Console.WriteLine("Invalid name! Press any key to continue.");
                Console.ReadKey();
                return;
            }

            petManager.AdoptPet(petName, selectedPetType);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private void ViewPets()
        {
            Console.Clear();
            petManager.DisplayAllPetStats();
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private async Task UseItemAsync()
        {
            var alivePets = petManager.GetAlivePets();
            
            if (!alivePets.Any())
            {
                Console.Clear();
                Console.WriteLine("You don't have any pets to use items on!");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }

            var petMenu = new Menu<Pet>("Select Pet");
            foreach (var pet in alivePets)
            {
                petMenu.AddItem($"{pet.Name} ({pet.Type})", pet);
            }

            var selectedPet = petMenu.Display();

            var compatibleItems = ItemDatabase.GetItemsForPetType(selectedPet.Type);

            if (!compatibleItems.Any())
            {
                Console.Clear();
                Console.WriteLine($"No items available for {selectedPet.Type}s!");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }

            var itemMenu = new Menu<Item>("Select Item");
            foreach (var item in compatibleItems)
            {
                var description = $"{item.Name} (+{item.HungerIncrease} Hunger, +{item.SleepIncrease} Sleep, +{item.FunIncrease} Fun)";
                itemMenu.AddItem(description, item);
            }

            var selectedItem = itemMenu.Display();

            Console.Clear();
            await selectedPet.UseItem(selectedItem);
            
            Console.WriteLine($"\n{selectedPet.GetStatusString()}");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine("=== Credits ===");
            Console.WriteLine("Project Creator: Tuana Çakmanus");
            Console.WriteLine("Student Number: 2305041022");
            Console.WriteLine("Course: DGD208 - Game Programming 2");
            Console.WriteLine("Spring 2025 Final Project");
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        private void OnPetAdopted(object? sender, PetEventArgs e)
        {
            
        }

        private void OnPetDied(object? sender, PetEventArgs e)
        {
            
        }
    }
}