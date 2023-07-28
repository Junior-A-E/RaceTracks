using Microsoft.Xna.Framework;

namespace Racetracks
{
    class CarNPC : Body
    {
        private Waypoints waypoints;
        private float offset;
        private float speed;
        private int currentWaypointIndex = 0;

        /// <summary>Creates a waypoint-driven Car</summary>        
        public CarNPC(Vector2 position, float speed, float offset) : base(position,"car2")
        {
            this.position = position;
            offsetDegrees = -90;
            waypoints = new Waypoints();
            this.offset = offset;
            this.speed = speed * 50;
        }

        /// <summary>Updates this Car</summary>        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 target = waypoints.GetTarget(Position);
            target.Y += offset;

            // Calculate the vector from the car to the next waypoint
            Vector2 toNextWaypoint = target - Position;

            // Normalize the vector toNextWaypoint so that it has a length of 1
            toNextWaypoint.Normalize();

            // Calculate the desired velocity towards the next waypoint
            Vector2 desiredVelocity = toNextWaypoint * speed;

            // Calculate the steering force (desired velocity - current velocity)
            Vector2 steeringForce = desiredVelocity - Velocity;

            // Apply the steering force to adjust the velocity of the car
            Velocity += steeringForce * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Apply a constant drag to the car to slow it down gradually
            Velocity *= 0.99f; // Adjust the value as needed

            // Update the position of the car based on its velocity
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the car's rotation based on its movement direction
            AngularDirection = Velocity;
        }
    }
}



