using SillyGardening.Core.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyGardening.Core.Responce
{
    public class NewPlantResponce
    {
        public bool isSuccesful {  get; set; }

        public Plant Plant { get; set; }


        public NewPlantResponce(Plant plant, bool succes)
        {
            Plant = plant;
            isSuccesful = succes;
        }

    }
}
