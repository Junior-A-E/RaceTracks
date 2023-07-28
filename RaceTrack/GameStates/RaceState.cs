using Microsoft.Xna.Framework;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Racetracks
{
    class RaceState : GameObjectList
    {
        private Car player;
        private GameObjectList bodies;

        /// <summary>
        /// RaceState constructor which adds the different gameobjects and lists in the correct order of drawing.
        /// </summary>
        public RaceState()
        {
            //create background
            Add(new SpriteGameObject("road"));

            //create a list for collidable objects
            Add(bodies = new GameObjectList());

            //create user controlled car
            bodies.Add(player = new Car(new Vector2(920, 1400))); //hardcoded position data from 'Tiled'

            //create four opponents
            for (int i = 0; i < 4; i++)
            {
                float x = 1300 - i * 130; //hardcoded position data from 'Tiled'
                float y = 1400 + i * 50;
                float speed = 6 - i * 0.5f;
                float offset = (i - 1.5f) * 20;
                bodies.Add(new CarNPC(new Vector2(x, y), speed, offset));
            }

            //load scenery
            new LevelLoader().LoadLevel(this);
        }

        /// <summary>
        /// Updates the RaceState.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            for (int j = 0; j < bodies.Children.Count; j++)
            {
                if (bodies.Children[j] is CarNPC npc && player.CollidesWith(npc))
                {
                    player.Collision(npc);
                }
            }

            for (int i = 0; i < bodies.Children.Count; i++)
            {
                if (bodies.Children[i] is CarNPC npc)
                {
                    for (int j = i + 1; j < bodies.Children.Count; j++)
                    {
                        if (bodies.Children[j] is CarNPC carnpc && carnpc.CollidesWith(npc))
                        {
                            npc.Collision(carnpc);
                        }
                    }
                }
            }

            for (int i = 0; i < this.Children.Count; i++)
            {
                if (this.Children[i] is Body body && body.CollidesWith(player))
                {
                    player.Collision(body);
                }
            }

            for(int i = 0; i < this.Children.Count; i++)
            {
                for (int j = i + 1; j < this.Children.Count; j++)
                {
                    if (this.Children[i] is Body body && this.Children[j] is Body body2)
                    {
                        if (body.CollidesWith(body2))
                        {
                            body.Collision(body2);
                        }
                    }
                }
            }

            for (int i = 0; i < this.bodies.Children.Count; i++)
            {
                for (int j = i + 1; j < this.Children.Count; j++)
                {
                    if (this.bodies.Children[i] is CarNPC carNpc && this.Children[j] is Body body2)
                    {
                        if (carNpc.CollidesWith(body2))
                        {
                            carNpc.Collision(body2);
                        }
                    }
                }
            }

            base.Update(gameTime);

            //reposition screen to follow the player with 'camera'
            position = GameEnvironment.Screen.ToVector2() * 0.5f - player.Position;
        }
    }
}
