using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Marche
{
    public class PositionSwitchScenecs
    {
        private Vector2 _PosToSpawn;
        public Vector2 SwitchScene(int idTP)
        {
            // Marché
            switch (idTP)
            {
                case 1:
                    _PosToSpawn = new Vector2(2850, 2912);
                    break;
                case 4:
                    _PosToSpawn = new Vector2(60, 380);
                        break;
                default:
                    _PosToSpawn = new Vector2(400, 2180);
                    break;              
            }

            return _PosToSpawn;
        }
    }
}
