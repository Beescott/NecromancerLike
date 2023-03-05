using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.WaypointPatrol
{
    public class WaypointPatroller : MonoBehaviour
    {
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private WaypointPatrolBehaviour behaviour;

        [SerializeField] private float waypointWidth;
        [SerializeField] private float stopTime;

        public bool IsInitialized { get; private set; }
        
        private Transform _currentWaypoint;
        private int _currentWaypointIndex;

        private bool _reverse = false;
        private float _lastStop;

        private bool _waitForStop;

        public void Initialize()
        {
            _currentWaypoint = waypoints[0];
            _currentWaypointIndex = 0;
            IsInitialized = true;
            _lastStop = Time.time;
        }

        public void OnWaypointReached()
        {
            if (_waitForStop)
                return;
            
            _waitForStop = true;
            _lastStop = Time.time;
        }
        
        public Vector3 GetNextWaypoint()
        {
            if (_lastStop + stopTime > Time.time)
            {
                return transform.position;
            }

            _waitForStop = false;
            if (behaviour == WaypointPatrolBehaviour.Loopback)
                return GetNextWaypointLoop();

            return GetNextWaypointPingPong();
        }

        private Vector3 GetNextWaypointLoop()
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= waypoints.Count)
            {
                _currentWaypointIndex = 0;
            }
            _currentWaypoint = waypoints[_currentWaypointIndex];
            return GetRandomPointOnWaypoint(_currentWaypoint);
        }
        
        private Vector3 GetNextWaypointPingPong()
        {
            if (_reverse == false && _currentWaypointIndex == waypoints.Count - 1)
                _reverse = true;
            
            if (_reverse == false)
                return GetNextWaypointLoop();
            
            _currentWaypointIndex--;
            if (_currentWaypointIndex < 0)
            {
                _currentWaypointIndex = 1;
                _reverse = false;
            }

            _currentWaypoint = waypoints[_currentWaypointIndex];
            return GetRandomPointOnWaypoint(_currentWaypoint);
        }
        
        private enum WaypointPatrolBehaviour
        {
            Loopback,
            PingPong
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < waypoints.Count - 1; i++)
            {
                Transform waypoint = waypoints[i];
                GetLeftAndRight(waypoint, out Vector3 right, out Vector3 left);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(left, right);
                DrawGizmosSideLines(waypoint, waypoints[i + 1]);
            }

            if (waypoints.Count <= 1)
                return;
            
            Transform lastWaypoint = waypoints[^1];
            GetLeftAndRight(lastWaypoint, out Vector3 lastRight, out Vector3 lastLeft);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(lastLeft, lastRight);
            
            if (behaviour == WaypointPatrolBehaviour.Loopback)
                DrawGizmosSideLines(lastWaypoint, waypoints[0]);
        }

        private void DrawGizmosSideLines(Transform from, Transform to)
        {
            Vector3 fromPosition = from.position;
            Vector3 toPosition = to.position;
            
            Vector3 leftFrom = fromPosition - from.right * waypointWidth / 2.0f;
            Vector3 leftTo = toPosition - to.right * waypointWidth / 2.0f;
            
            Vector3 rightFrom = fromPosition + from.right * waypointWidth / 2.0f;
            Vector3 rightTo = toPosition + to.right * waypointWidth / 2.0f;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(leftFrom, leftTo);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(rightFrom, rightTo);
        }

        private void GetLeftAndRight(Transform waypoint, out Vector3 right, out Vector3 left)
        {
            Vector3 waypointPosition = waypoint.position;
            right = waypointPosition + waypoint.right * waypointWidth / 2.0f;
            left = waypointPosition - waypoint.right * waypointWidth / 2.0f;
        }

        private Vector3 GetRandomPointOnWaypoint(Transform waypoint)
        {
            Vector3 waypointPosition = waypoint.position;
            Vector3 right = waypointPosition + waypoint.right * waypointWidth / 2.0f;
            Vector3 left = waypointPosition - waypoint.right * waypointWidth / 2.0f;

            Vector3 normalized = (right - left).normalized;
            return left + normalized * UnityEngine.Random.Range(0, waypointWidth);
        }
    }
}
