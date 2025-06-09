namespace PetSimulator
{
    public class PetStatChangeEventArgs : EventArgs
    {
        public Pet Pet { get; }
        public PetStat Stat { get; }
        public int OldValue { get; }
        public int NewValue { get; }

        public PetStatChangeEventArgs(Pet pet, PetStat stat, int oldValue, int newValue)
        {
            Pet = pet;
            Stat = stat;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}