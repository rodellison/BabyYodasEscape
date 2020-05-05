using System;
using Base_Project._Scripts.GameData;
using UnityEngine;

namespace Systems
{
    public class BoundaryMoveSystem : MonoBehaviour
    {
        public FloatVariable PlayerSpeed;
        public float BoundaryMoveSpeed;
        public FloatVariable ZBoundaryBehind;
        public FloatVariable StartingForwardZ;
        private Vector3 newPosition;


        private void Update()
        {
            if (PlayerSpeed.Value > 0)
                MoveBoundary();
        }

        void MoveBoundary()
        {
            newPosition = transform.position;
            newPosition.z -= BoundaryMoveSpeed * PlayerSpeed.Value * Time.deltaTime;

            if (newPosition.z < ZBoundaryBehind.Value)
            {
                newPosition.z = StartingForwardZ.Value;
            }

            transform.position = newPosition;
        }
    }
}