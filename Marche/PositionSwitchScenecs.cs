using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Marche
{
    class PositionSwitchScenecs
    {
        

        public Vector2 SwitchScene(int idTP)
        {
            Vector2 _SpawnPosition;
            // Marché
            switch (idTP)
            {
                case 4:
                    _SpawnPosition = new Vector2(60, 380);
                        break;
                default:
                    _SpawnPosition = new Vector2(400, 400);
                    break;
                    
            }
            return _SpawnPosition;
        }
    }
}
