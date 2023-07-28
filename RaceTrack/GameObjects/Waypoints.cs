using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Racetracks
{
    class Waypoints
    {
        const float WaypointRadius = 100f;

        private List<Vector2> waypoints = new List<Vector2>();
        private int currentIndex = 0;

        /// <summary>Initializes waypoint data</summary>
        public Waypoints()
        {
            CreateWayPoint(1600, 1472); //data exported from 'Tiled'
            CreateWayPoint(1728, 1344);
            CreateWayPoint(1728, 832);
            CreateWayPoint(1856, 704);
            CreateWayPoint(3008, 704);
            CreateWayPoint(3136, 832);
            CreateWayPoint(3136, 1152);
            CreateWayPoint(3264, 1280);
            CreateWayPoint(4288, 1280);
            CreateWayPoint(4416, 1408);
            CreateWayPoint(4416, 2112);
            CreateWayPoint(4288, 2240);
            CreateWayPoint(3200, 2240);
            CreateWayPoint(3072, 1920);
            CreateWayPoint(1792, 1920);
            CreateWayPoint(1664, 2048);
            CreateWayPoint(1664, 2368);
            CreateWayPoint(1536, 2432);
            CreateWayPoint(768, 2432);
            CreateWayPoint(704, 2304);
            CreateWayPoint(704, 1536);
            CreateWayPoint(832, 1472);
        }

        /// <summary>Create Waypoint and add to list</summary>
        private void CreateWayPoint(int x, int y)
        {
            waypoints.Add(new Vector2(x, y));
        }

        /// <summary>Gets current waypoint (non-protected read)</summary>
        private Vector2 GetCurrentWaypoint()
        {
            return waypoints[currentIndex];
        }

        /// <summary>Get current waypoint based on given position</summary>
        public Vector2 GetTarget(Vector2 position)
        {
            Vector2 current = GetCurrentWaypoint();
            if ((position - current).Length() < WaypointRadius)
            {
                ProgressIndex();
                current = GetCurrentWaypoint();
            }
            return current;
        }

        /// <summary>Select next waypoint index</summary>
        private void ProgressIndex()
        {
            currentIndex = (currentIndex + 1) % waypoints.Count;
        }
        /// <summary>Get the count of waypoints</summary>
        public int GetCount()
        {
            return waypoints.Count;
        }
    }
}
