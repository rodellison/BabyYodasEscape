
using System.Collections;
using UnityEngine;
using Managers;
using UnityEngine.InputSystem;

namespace Systems
{
    public class PlayerShootSystem : MonoBehaviour, InputActionControls.IGameplayActions
    {
       //This is to have a reference to the cannon's respective transform positions
        //so we can originate the respective shot fired in the right starting location..
        public GameObject[] XWingCannons;
        private int shotIterator; //The XWing Has 4 cannons so they'll iterate 0 through 3
        private InputActionControls playerShootControl;
        public AudioSource LaserShot;
 
        private void OnEnable()
        {
            playerShootControl = new InputActionControls();
            playerShootControl.Gameplay.SetCallbacks(this);
            playerShootControl.Enable();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            //Nothing to do for this in this script. Movement is handled in the PlayerMoveSystem script
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (Keyboard.current[Key.Space].wasPressedThisFrame ||
                Gamepad.current.leftTrigger.wasPressedThisFrame ||
                Mouse.current.leftButton.wasPressedThisFrame)
                GetLaserAndShoot();
        }

        public void GetLaserAndShoot()
        {
            Debug.Log("Shooting Laser");
            GameObject thisShot = LaserObjectPooler.SharedInstance.GetPooledObject();
            if (thisShot != null)
            {
                thisShot.transform.position = XWingCannons[shotIterator].transform.position;
                thisShot.transform.rotation = Quaternion.Euler(90, 0, 0);
                StartCoroutine(LightUpCannon(shotIterator));
                thisShot.gameObject.SetActive(true);
                shotIterator += 1;
                if (shotIterator > 3)
                    shotIterator = 0;
            }
 
        }

        IEnumerator LightUpCannon(int CannonToFire)
        {
            LaserShot.Play();
            XWingCannons[CannonToFire].GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.15f);
            XWingCannons[CannonToFire].GetComponent<MeshRenderer>().enabled = false;
            
        }
    }
}