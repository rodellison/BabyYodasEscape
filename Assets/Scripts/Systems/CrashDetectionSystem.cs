using Base_Project._Scripts.Game_Events;
using UnityEngine;

namespace Systems
{
    public class CrashDetectionSystem : MonoBehaviour
    {
        public GameEvent PlayerCrashed;
        public GameObject Explosion;
        public GameObject LargeFire;
        public bool enableEventRaising = false;

        private void OnTriggerEnter(Collider other)
        {
     //       Debug.Log("Player has trigger crashed into an Obstacle");
            Explosion.SetActive(true);
            LargeFire.SetActive(true);
            if (enableEventRaising)
                PlayerCrashed.Raise();
        }
 
    }
}