using Microsoft.Xna.Framework;
using System;

namespace Racetracks
{
    class Body : RotatingSpriteGameObject
    {
        protected float radius;
        private Vector2 acceleration = Vector2.Zero;
        private float invMass = 1.0f; //set indirectly by setting 'mass'
        private const float bounceFactor = 0.9f;

        /// <summary>Creates a physics body</summary>
        public Body(Vector2 position, string assetName) : base(assetName)
        {
            Vector2 size = new Vector2(Width, Height); //circle collider will fit either width or height
            radius = Math.Max(Width, Height) / 2.0f;
            Mass = (radius * radius); //mass approx.
            origin = Center;
            this.position = position;
        }

        /// <summary>Updates this Body</summary>        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>Returns closest point on this shape</summary>        
        public Vector2 GetClosestPoint(Vector2 point)
        {
            Vector2 delta = point - position;
            if (delta.LengthSquared() > radius * radius)
            {
                delta.Normalize();
                delta *= radius;
            }
            return position + delta;
        }

        /// <summary>Sets the invMass to 0.0</summary>        
        protected void MakeStatic()
        {
            invMass = 0.0f; //making mass infinite
        }

        /// <summary>Get or set the invMass through setting the mass</summary>        
        private float _mass = 1.0f;
        public float Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                _mass = value;
                invMass = 1.0f / value; //note: float div. by zero will give infinite invMass
            }
        }

        /// <summary>Get or set the angle by getting/setting the forward Vec2</summary>
        public Vector2 Forward
        {
            get
            {
                return new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)); //angle to polar
            }

            set
            {
                Angle = (float)Math.Atan2(value.Y, value.X); //polar to angle
            }
        }

        /// <summary>Get or set the angle by getting/setting the right Vec2</summary>
        public Vector2 Right
        {
            get
            {
                return new Vector2((float)-Math.Sin(Angle), (float)Math.Cos(Angle)); //angle to polar
            }

            set
            {
                Angle = (float)Math.Atan2(-value.X, value.Y); //polar to angle
            }
        }

        /*
        public virtual void HandleCollision(Body other)
        {
            Vector2 collisionNormal = Vector2.Normalize(other.Position - Position);
            float overlap = radius + other.radius - Vector2.Distance(Position, other.Position);

            if (overlap > 0)
            {
                Position -= collisionNormal * (overlap / 2);
                other.Position += collisionNormal * (overlap / 2);

                // Calculate the relative velocity of the two bodies
                Vector2 relativeVelocity = other.Velocity - Velocity;

                // Calculate the impulse magnitude using the dot product of relative velocity and collision normal
                float impulseMagnitude = 2 * Vector2.Dot(relativeVelocity, collisionNormal);

                // Calculate the impulse vector
                Vector2 impulse = collisionNormal * impulseMagnitude;

                // Update the velocities of the two bodies based on the impulse
                Velocity += impulse / 2;
                other.Velocity -= impulse / 4;
            }
        }*/

        public void Collision(Body body)
        {
            Vector2 collisionNormal = Vector2.Normalize(body.Position - this.Position);
            float componentFactor = Vector2.Dot(this.Velocity - body.Velocity, collisionNormal);

            float overlap = radius + body.radius - Vector2.Distance(Position, body.Position);

            Position -= collisionNormal * (overlap / 2);
            body.Position += collisionNormal * (overlap / 2);

            Vector2 changeVelocity = (1 + bounceFactor) * componentFactor * collisionNormal;

            float totalInverMass = this.invMass + body.invMass;

            changeVelocity /= totalInverMass;

            this.velocity = (this.velocity - changeVelocity * this.invMass) /2;
            body.velocity = (body.velocity + changeVelocity * body.invMass) /4;
        }
    }
}
