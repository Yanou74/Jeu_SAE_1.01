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
                    _PosToSpawn = new Vector2(1792, 861);
                    break;
                case 4:
                    _PosToSpawn = new Vector2(60, 380);
                        break;
                case 5:
                    _PosToSpawn = new Vector2(210, 300);
                    break;
                case 6:
                    _PosToSpawn = new Vector2(512, 672);
                    break;
                default:
                    _PosToSpawn = new Vector2(512, 928);
                    break;              
            }

            return _PosToSpawn;
        }
    }
}
