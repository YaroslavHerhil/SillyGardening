using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SillyGardening.Core.Abstract;
using SillyGardening.Core.Responce;
using SillyGardening.Web.Models;
using static System.Collections.Specialized.BitVector32;

namespace SillyGardening.Core.GameComponents
{
    public class GameSession : IGameSession
    {
        //Plants 
        public List<Plant> Plants { get; set; } = new List<Plant>();
        public double Money { get; set; } = 10000;
        public double AllProduction { get; set; } = 0;
        public int PlantCounter { get; set; } = 0;


        public void ClearGameSession()
        {
            Plants = new List<Plant>();
            Money = 1000;
            AllProduction = 0;
        }


        public void LoadGameSession(GameSession session)
        {
            Plants = session.Plants;
            Money = session.Money;
            AllProduction = session.AllProduction;
        }

        public NewPlantResponce PlantPlant(int id)
        {
            if (Money >= 100)
            {
                Money -= 100;
                var newPlant = new Plant(id);
                Plants.Add(newPlant);
                PlantCounter++;
                return new NewPlantResponce(newPlant ,true );
            }
            return new NewPlantResponce(null, false);
        }



        public void HugPlant(int id)
        {
            try
            {

                var plant = Plants.First(p => p.Id == id);

                plant.Happiness += 0.3;
                if (plant.Happiness > 1) { plant.Happiness = 1; }
            }
            catch
            {
                Console.WriteLine("Ouch");
            }
        }

        public void RemovePlant(int id)
        {
            Plants = Plants.Where(p => p.Id != id).ToList();
            Console.WriteLine("Remewing");
        }


        public void Update()
        {
            AllProduction = Plants.Sum(p => p.GetMoney());
            Money += AllProduction;
        }

    }
}
