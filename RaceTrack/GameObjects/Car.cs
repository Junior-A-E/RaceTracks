using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Racetracks
{
    class Car : Body
    {
        private const float MaxSpeed = 300f;      // Set the maximum speed for the car
        private const float Acceleration = 50f;  // Set the acceleration for the car
        private const float Drag = 50f;          // Set the drag force for slowing down the car
        private const float TurnSpeed = 0.1f;       // Set the turning speed for the car

        private float currentSpeed = 0f;

        /// <summary>Creates a user controlled Car</summary>        
        public Car(Vector2 position) : base(position,"car")
        {
            this.Position = position;
            this.Origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            offsetDegrees = -90;
        }

        /// <summary>Updates this Car</summary>        
        public override void Update(GameTime gameTime)
        {
            // Calculate the time since the last frame
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Apply drag force to slow down the car
            if (currentSpeed > 0)
                currentSpeed = Math.Max(0, currentSpeed - Drag * deltaTime);
            else if (currentSpeed < 0)
                currentSpeed = Math.Min(0, currentSpeed + Drag * deltaTime);

            // Apply the position change based on the current speed and rotation
            Position += AngularDirection * currentSpeed * deltaTime;

            base.Update(gameTime);
        }

        /// <summary>Handle user input for this Car</summary>        
        public override void HandleInput(InputHelper inputHelper)
        {
            // Get the motor force based on user input
            float motorForce = 0f;
            if (inputHelper.IsKeyDown(Keys.Up))
                motorForce = Acceleration;
            else if (inputHelper.IsKeyDown(Keys.Down))
                motorForce = -Acceleration;
            else if (motorForce > 0)
            {
                motorForce -= Drag;
            }

            // Apply the motor force to change the current speed
            currentSpeed = MathHelper.Clamp(currentSpeed + motorForce, -MaxSpeed, MaxSpeed);

            // Calculate the turning force based on user input for left and right turns
            float turnForce = 0f;
            if (inputHelper.IsKeyDown(Keys.Left))
                turnForce = -TurnSpeed;
            else if (inputHelper.IsKeyDown(Keys.Right))
                turnForce = TurnSpeed;

            // Apply the turning force to rotate the car
            Angle += turnForce;

            // Normalize the rotation to keep it within the range [0, 360)
            if (Angle >= MathHelper.TwoPi)
                Angle -= MathHelper.TwoPi;
            else if (Angle < 0)
                Angle += MathHelper.TwoPi;
        }

        public bool CollidesBox(GameObject body)
        {
            return BoundingBox.Intersects(body.BoundingBox);
        }

    }
}


