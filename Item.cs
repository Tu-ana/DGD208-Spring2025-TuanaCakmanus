namespace PetSimulator
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int HungerIncrease { get; set; }
        public int SleepIncrease { get; set; }
        public int FunIncrease { get; set; }
        public int UsageTimeSeconds { get; set; }
        public List<PetType> CompatiblePetTypes { get; set; }

        public Item(string name, ItemType type, int hungerIncrease, int sleepIncrease, 
                   int funIncrease, int usageTimeSeconds, params PetType[] compatiblePetTypes)
        {
            Name = name;
            Type = type;
            HungerIncrease = hungerIncrease;
            SleepIncrease = sleepIncrease;
            FunIncrease = funIncrease;
            UsageTimeSeconds = usageTimeSeconds;
            CompatiblePetTypes = compatiblePetTypes.ToList();
        }

        public bool IsCompatibleWith(PetType petType)
        {
            return CompatiblePetTypes.Contains(petType);
        }
    }
}