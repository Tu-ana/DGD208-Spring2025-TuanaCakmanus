namespace PetSimulator
{
    public class PetEventArgs : EventArgs
    {
        public Pet Pet { get; }

        public PetEventArgs(Pet pet)
        {
            Pet = pet;
        }
    }
}