using System.Collections;
using Base_Project._Scripts.GameData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ObstacleSpawnManager : MonoBehaviour
    {
        public FloatVariable PlayerSpeed;
        public FloatVariable MaxHorizontalSpawnArea;

        private bool readyToSpawnObstacle;
        public FloatVariable ObstacleSpawnTime;
 
        private ObstacleObjectPooler[] childrenWithObjectPools;

        private void OnEnable()
        {
            readyToSpawnObstacle = true;
            //We want to get all of the ObstacleObjectPoolers so we can randomly choose one at spawn time
            childrenWithObjectPools = GetComponentsInChildren<ObstacleObjectPooler>();
        }

        public void SetupForRestart()
        {
            GameObject[] currentObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject go in currentObstacles)
            {
                go.SetActive(false);
            }
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
            GameObject randomObstacle = childrenWithObjectPools[Random.Range(0, childrenWithObjectPools.Length)]
                .GetPooledObject();
            //Make sure we got an object before trying to manipulate it...
            if (randomObstacle != null)
            {
                randomObstacle.transform.position = positionToSpawn;

                //Trying to give equal chance of object being rotated 180 degrees for variation
                randomObstacle.transform.rotation = UnityEngine.Random.Range(0, 10) < 5
                    ? Quaternion.Euler(0, 0, 0)
                    : Quaternion.Euler(0, 180, 0);
                randomObstacle.SetActive(true);
            }

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

            float rate = 1f / timeToScale;

            for (float x = 0.0f; x <= 1.0f; x += Time.deltaTime * rate)
            {
                tempScale.x = Mathf.Lerp(0, targetScaleX, x);
                tempScale.y = Mathf.Lerp(0, targetScaleY, x);
                tempScale.z = Mathf.Lerp(0, targetScaleZ, x);

                theNewObstacle.transform.localScale = tempScale;
                yield return null;
            }

            theNewObstacle.transform.localScale = ObstacleScale;
        }
    }
}