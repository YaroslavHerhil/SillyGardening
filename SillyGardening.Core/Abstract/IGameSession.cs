using SillyGardening.Core.GameComponents;
using SillyGardening.Core.Responce;

namespace SillyGardening.Core.Abstract
{
    public interface IGameSession
    {
        public NewPlantResponce PlantPlant(int id);
        public void Update();
        public void HugPlant(int id);
        public void RemovePlant(int id);
        public void ClearGameSession();
        public void LoadGameSession(GameSession session);
    }
}