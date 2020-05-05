using System;
using System.Collections;
using Base_Project._Scripts.GameData;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace Systems
{
    public class ObstacleSpawnerSystem : MonoBehaviour
    {
        public FloatVariable PlayerSpeed;
        public FloatVariable MaxHorizontalSpawnArea;
        public GameObject[] prefabObstacles;
        private bool readyToSpawnObstacle;
        public FloatVariable ObstacleSpawnTime;


        private void OnEnable()
        {
            readyToSpawnObstacle = true;
        }

        private void Update()
        {
            if (PlayerSpeed.Value > 0)
            {
                if (readyToSpawnObstacle)
                    StartCoroutine(SpawnAnObstacle());
            }
        }

        IEnumerator SpawnAnObstacle()
        {
            readyToSpawnObstacle = false;
            Vector3 positionToSpawn = new Vector3(
                Random.Range(-MaxHorizontalSpawnArea.Value, MaxHorizontalSpawnArea.Value), 0,
                transform.position.z);
            GameObject randomObstacle = Instantiate(prefabObstacles[Random.Range(0, prefabObstacles.Length)],
                positionToSpawn, Quaternion.Euler(0, Random.Range(-30, 30), 0));

            StartCoroutine(ScaleUpSpawnedItem(randomObstacle));
            yield return new WaitForSeconds(ObstacleSpawnTime.Value);
            readyToSpawnObstacle = true;
        }

        IEnumerator ScaleUpSpawnedItem(GameObject theNewObstacle)
        {
            Vector3 ObstacleScale = theNewObstacle.transform.localScale;
            float targetScaleX = ObstacleScale.x;
            float targetScaleY = ObstacleScale.y;
            float targetScaleZ = ObstacleScale.z;

            Vector3 tempScale = Vector3.zero;
            theNewObstacle.transform.localScale = tempScale;
            float timeToScale = 0.1f;
            
            float rate = 1f/timeToScale; 

            for (float x = 0.0f; x <= 1.0f; x += Time.deltaTime * rate)
            {
                tempScale.x = Mathf.Lerp(0, targetScaleX, x);
                tempScale.y = Mathf.Lerp(0, targetScaleY, x);
                tempScale.z = Mathf.Lerp(0, targetScaleZ, x);

                theNewObstacle.transform.localScale = tempScale;
                yield return null;
            }
           
        }
    }
}