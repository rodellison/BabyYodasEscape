
using System;
using Systems;
using UnityEngine;

namespace Managers
{
    public class XWingManager : MonoBehaviour
    {

        public GameObject Explosion;
        public GameObject Fireball;
        public Vector3 StartingGamePositionAfterIntro;

        private void Start()
        {
            StartingGamePositionAfterIntro = new Vector3(0,5,0);
        }

        public void TurnOffAnimator()
        {
            GetComponent<Animator>().enabled = false;
        }

        public void SetupForRestart()
        {
            Explosion.SetActive(false);
            Fireball.SetActive(false);
            transform.position = StartingGamePositionAfterIntro;
            transform.eulerAngles = new Vector3(0,0,0);
            GetComponent<PlayerMoveSystem>().currentEulerAngles = Vector3.zero;
            GetComponent<PlayerMoveSystem>().enabled = true;
            GetComponent<PlayerShootSystem>().enabled = true;
            GetComponent<AudioSource>().Play();
            GetComponent<AutoPilotSystem>().enabled = false;
            GetComponent<AutoPilotCrashSystem>().enabled = false;
        }
        
    }
}