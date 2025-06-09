namespace PetSimulator
{
    public static class ItemDatabase
    {
        public static List<Item> Items { get; } = new List<Item>
        {
            new Item("Dog Food", ItemType.Food, 30, 0, 5, 3, PetType.Dog),
            new Item("Cat Food", ItemType.Food, 25, 0, 5, 2, PetType.Cat),
            new Item("Bird Seeds", ItemType.Food, 20, 0, 10, 2, PetType.Bird),
            new Item("Fish Flakes", ItemType.Food, 15, 0, 5, 1, PetType.Fish),
            new Item("Hamster Pellets", ItemType.Food, 20, 0, 5, 2, PetType.Hamster),
            
            new Item("Tennis Ball", ItemType.Toy, 0, 0, 25, 5, PetType.Dog),
            new Item("Catnip Mouse", ItemType.Toy, 0, 0, 30, 4, PetType.Cat),
            new Item("Mirror Toy", ItemType.Toy, 0, 0, 20, 3, PetType.Bird),
            new Item("Hamster Wheel", ItemType.Toy, 0, 0, 35, 6, PetType.Hamster),
            
            new Item("Cozy Bed", ItemType.Bed, 0, 40, 10, 8, PetType.Dog, PetType.Cat, PetType.Hamster),
            new Item("Perch", ItemType.Bed, 0, 30, 5, 6, PetType.Bird),
            
            new Item("Premium Treat", ItemType.Treat, 15, 5, 20, 3, PetType.Dog, PetType.Cat, PetType.Hamster),
            new Item("Special Seeds", ItemType.Treat, 10, 5, 25, 2, PetType.Bird),
            new Item("Vitamin Drops", ItemType.Medicine, 5, 10, 5, 4, PetType.Fish)
        };

        public static List<Item> GetItemsForPetType(PetType petType)
        {
            return Items.Where(item => item.IsCompatibleWith(petType)).ToList();
        }

        public static List<Item> GetItemsByType(ItemType itemType)
        {
            return Items.Where(item => item.Type == itemType).ToList();
        }
    }
}