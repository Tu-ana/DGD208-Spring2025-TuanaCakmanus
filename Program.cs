using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PetSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var game = new Game();
            await game.StartAsync();
        }
    }
}