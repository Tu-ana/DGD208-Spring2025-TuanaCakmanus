namespace PetSimulator
{
    public class Pet
    {
        public event EventHandler<PetEventArgs>? PetDied;
        public event EventHandler<PetStatChangeEventArgs>? StatChanged;

        public string Name { get; set; }
        public PetType Type { get; set; }
        public int Hunger { get; private set; }
        public int Sleep { get; private set; }
        public int Fun { get; private set; }
        public bool IsAlive { get; private set; }
        public DateTime AdoptedAt { get; private set; }

        private CancellationTokenSource? cancellationTokenSource;

        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
            Hunger = 50;
            Sleep = 50;
            Fun = 50;
            IsAlive = true;
            AdoptedAt = DateTime.Now;
        }

        public void StartStatDecay()
        {
            cancellationTokenSource = new CancellationTokenSource();
            _ = Task.Run(async () => await StatDecayLoop(cancellationTokenSource.Token));
        }

        public void StopStatDecay()
        {
            cancellationTokenSource?.Cancel();
        }

        private async Task StatDecayLoop(CancellationToken cancellationToken)
        {
            while (IsAlive && !cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(3000, cancellationToken); // Decrease stats every 3 seconds
                
                if (cancellationToken.IsCancellationRequested) break;

                DecreaseStat(PetStat.Hunger, 1);
                DecreaseStat(PetStat.Sleep, 1);
                DecreaseStat(PetStat.Fun, 1);

                CheckForDeath();
            }
        }

        private void DecreaseStat(PetStat stat, int amount)
        {
            var oldValue = GetStatValue(stat);
            
            switch (stat)
            {
                case PetStat.Hunger:
                    Hunger = Math.Max(0, Hunger - amount);
                    break;
                case PetStat.Sleep:
                    Sleep = Math.Max(0, Sleep - amount);
                    break;
                case PetStat.Fun:
                    Fun = Math.Max(0, Fun - amount);
                    break;
            }

            var newValue = GetStatValue(stat);
            if (oldValue != newValue)
            {
                StatChanged?.Invoke(this, new PetStatChangeEventArgs(this, stat, oldValue, newValue));
            }
        }

        public async Task UseItem(Item item)
        {
            if (!IsAlive) return;

            Console.WriteLine($"Using {item.Name} on {Name}... This will take {item.UsageTimeSeconds} seconds.");
            
            await Task.Delay(item.UsageTimeSeconds * 1000);

            IncreaseStat(PetStat.Hunger, item.HungerIncrease);
            IncreaseStat(PetStat.Sleep, item.SleepIncrease);
            IncreaseStat(PetStat.Fun, item.FunIncrease);

            Console.WriteLine($"Finished using {item.Name} on {Name}!");
        }

        private void IncreaseStat(PetStat stat, int amount)
        {
            if (amount <= 0) return;

            var oldValue = GetStatValue(stat);
            
            switch (stat)
            {
                case PetStat.Hunger:
                    Hunger = Math.Min(100, Hunger + amount);
                    break;
                case PetStat.Sleep:
                    Sleep = Math.Min(100, Sleep + amount);
                    break;
                case PetStat.Fun:
                    Fun = Math.Min(100, Fun + amount);
                    break;
            }

            var newValue = GetStatValue(stat);
            if (oldValue != newValue)
            {
                StatChanged?.Invoke(this, new PetStatChangeEventArgs(this, stat, oldValue, newValue));
            }
        }

        private int GetStatValue(PetStat stat)
        {
            return stat switch
            {
                PetStat.Hunger => Hunger,
                PetStat.Sleep => Sleep,
                PetStat.Fun => Fun,
                _ => 0
            };
        }

        private void CheckForDeath()
        {
            if (Hunger <= 0 || Sleep <= 0 || Fun <= 0)
            {
                IsAlive = false;
                StopStatDecay();
                PetDied?.Invoke(this, new PetEventArgs(this));
            }
        }

        public string GetStatusString()
        {
            return $"{Name} ({Type}) - Hunger: {Hunger}/100, Sleep: {Sleep}/100, Fun: {Fun}/100 {(IsAlive ? "🟢" : "💀")}";
        }
    }
}