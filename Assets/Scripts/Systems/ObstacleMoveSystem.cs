
using Base_Project._Scripts.GameData;
using UnityEngine;

namespace Systems
{
    public class ObstacleMoveSystem : MonoBehaviour
    {
        public FloatVariable PlayerSpeed;
        public FloatVariable ObstacleMoveSpeed;
        public FloatVariable ZBoundaryBehind;
        private bool gameObjectDestroyed;


        private void Update()
        {
            if (PlayerSpeed.Value > 0)
                MoveObstacle();
        }

        void MoveObstacle()
        {
            Vector3 newPosition = transform.position;
            newPosition.z -= ObstacleMoveSpeed.Value * PlayerSpeed.Value * Time.deltaTime;

            if (newPosition.z < ZBoundaryBehind.Value)
            {
               gameObject.SetActive(false);  //return it to the pool
            }
            transform.position = newPosition;
        }
    }
}