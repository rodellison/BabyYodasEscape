using Base_Project._Scripts.GameData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems
{
    public class DynamicBoundarySetup : MonoBehaviour
    {
        public GameObject[] prefabsToUse;
        public FloatVariable zBehindLocation;
        public FloatVariable startingForwardZ;
        public FloatVariable boundarySpeed;
        public float minScale;
        public float maxScale;

        //Assuming center of play area remains at 0, half of distance will be forward of 0, half will be behind;
        public float distanceSideX;
        public float XDistanceVariation;
        public float maxDistanceToCover;
        public bool mirrorSetup = true;
        public int maxPrefabsToInstantiate;

        private void OnEnable()
        {
            SetupBoundaryObjects();
            //This will be used in another System for determining if boundary objects have moved
            //past the location and need to be recycled/moved back to the front...
            zBehindLocation.Value = startingForwardZ.Value - maxDistanceToCover;
        }

        private void SetupBoundaryObjects()
        {
            for (int i = 0; i < maxPrefabsToInstantiate; i++)
            {
                float zLocation = startingForwardZ.Value -
                                maxDistanceToCover / maxPrefabsToInstantiate * i;
                Vector3 placement = new Vector3(distanceSideX - Random.Range(0, XDistanceVariation), 0, zLocation);
                GameObject thisGO = Instantiate( prefabsToUse[Random.Range(0, prefabsToUse.Length)],
                    placement, 
                    Quaternion.Euler(0, Random.Range(-45, 45), 0), gameObject.transform);
                var thisItemScale = Random.Range(minScale, maxScale);
                thisGO.transform.localScale = new Vector3(thisItemScale, thisItemScale, thisItemScale);
                thisGO.GetComponent<BoundaryMoveSystem>().BoundaryMoveSpeed = boundarySpeed.Value;
                if (mirrorSetup)
                {
                    placement = new Vector3(- distanceSideX + Random.Range(0, XDistanceVariation), 0, zLocation);
                    thisItemScale = Random.Range(minScale, maxScale);
                    thisGO = Instantiate(prefabsToUse[Random.Range(0, prefabsToUse.Length)],
                        placement, 
                        Quaternion.Euler(0, Random.Range(-45, 45), 0), gameObject.transform);
                    thisGO.transform.localScale = new Vector3(thisItemScale, thisItemScale, thisItemScale);
                    thisGO.GetComponent<BoundaryMoveSystem>().BoundaryMoveSpeed = boundarySpeed.Value;
                  
                }
            }
        }
    }
}