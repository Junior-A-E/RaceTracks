using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Racetracks {
  
    public class Racetracks : GameEnvironment {
        public Racetracks() {
            Content.RootDirectory = "Content";
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void LoadContent() {
            base.LoadContent();
            Screen = new Point(800, 600);
            ApplyResolutionSettings();

            // Add the game states and call the first one.
            GameStateManager.AddGameState("Play", new RaceState());
            GameStateManager.SwitchTo("Play");
        }

    }
}
