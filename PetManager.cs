namespace PetSimulator
{
    public class PetManager
    {
        public event EventHandler<PetEventArgs>? PetAdopted;
        public event EventHandler<PetEventArgs>? PetDied;

        private readonly List<Pet> pets;

        public PetManager()
        {
            pets = new List<Pet>();
        }

        public void AdoptPet(string name, PetType type)
        {
            var pet = new Pet(name, type);
            pet.PetDied += OnPetDied;
            pets.Add(pet);
            pet.StartStatDecay();
            
            PetAdopted?.Invoke(this, new PetEventArgs(pet));
            Console.WriteLine($"Congratulations! You adopted {name} the {type}!");
        }

        private void OnPetDied(object? sender, PetEventArgs e)
        {
            Console.WriteLine($"\n OH NO! {e.Pet.Name} the {e.Pet.Type} has died!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
            pets.Remove(e.Pet);
            PetDied?.Invoke(this, e);
        }

        public List<Pet> GetAlivePets()
        {
            return pets.Where(p => p.IsAlive).ToList();
        }

        public List<Pet> GetAllPets()
        {
            return pets.ToList();
        }

        public Pet? GetPetByName(string name)
        {
            return pets.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && p.IsAlive);
        }

        public void DisplayAllPetStats()
        {
            var alivePets = GetAlivePets();
            
            if (!alivePets.Any())
            {
                Console.WriteLine("You don't have any pets yet!");
                return;
            }

            Console.WriteLine("=== Your Pets ===");
            foreach (var pet in alivePets)
            {
                Console.WriteLine(pet.GetStatusString());
            }
        }

        public void StopAllPets()
        {
            foreach (var pet in pets)
            {
                pet.StopStatDecay();
            }
        }
    }
}