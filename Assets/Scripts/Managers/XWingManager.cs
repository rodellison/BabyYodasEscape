
using Systems;
using UnityEngine;

namespace Managers
{
    public class XWingManager : MonoBehaviour
    {

        public GameObject Explosion;
        public GameObject Fireball;
        public Vector3 StartingGamePositionAfterIntro;
        public void TurnOffAnimator()
        {
            GetComponent<Animator>().enabled = false;
        }

        public void SetupForRestart()
        {
            Explosion.SetActive(false);
            Fireball.SetActive(false);
            transform.position = StartingGamePositionAfterIntro;
            GetComponent<PlayerMoveSystem>().enabled = true;
            GetComponent<PlayerShootSystem>().enabled = true;
            GetComponent<AudioSource>().Play();
            GetComponent<AutoPilotSystem>().enabled = false;
            GetComponent<AutoPilotCrashSystem>().enabled = false;
        }
        
    }
}