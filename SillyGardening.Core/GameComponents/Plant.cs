using System;

namespace SillyGardening.Core.GameComponents
{
    public class Plant
    {
        public string Type { get; set; } = _types[new Random().Next(_types.Length)];
        public int Face { get; set; } = new Random().Next(1, 17);
        public int Id { get; set; } 
        public double Production { get; set; } = 5;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int ExistenceFinale { get; set; } = new Random().Next(15, 31);
        public int ExistenceCounter { get; set; } = 15;

        public double Happiness { get; set; } = 1;

        public Plant(int id)
        {
            ExistenceCounter = ExistenceFinale;
            Id = id;
        }

        private static string[] _types =  {"flower","cucumber", "snakegrass", "tree", "mailflower", "sunflower", "coolflower", "venus" };


        public double GetMoney()
        {
            if (Happiness > 0.1)
                Happiness -= 0.1;
            else
                Happiness = 0.1;
            ExistenceCounter--;
            return Production * (0.5+Happiness*2);
        }

    }
}