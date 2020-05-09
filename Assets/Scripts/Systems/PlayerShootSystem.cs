using System;
using System.Collections;
using Base_Project._Scripts.Managers;
using UnityEngine;
using Managers;
using UnityEngine.InputSystem;

namespace Systems
{
    public class PlayerShootSystem : MonoBehaviour, InputActionControls.IGameplayActions
    {
        //This is to have a reference to the cannon's respective transform positions
        //so we can originate the respective shot fired in the right starting location..
        //The XWing Has 4 cannons so they'll iterate 0 through 3
        public GameObject[] XWingCannons;
        private int shotIterator;

        private InputActionControls playerShootControl;

        private void OnEnable()
        {
            if (playerShootControl == null)
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
            //     Debug.Log("Shooting Laser");
            GameObject thisShot = LaserObjectPooler.SharedInstance.GetPooledObject();
            if (thisShot != null)
            {
                if (XWingCannons[shotIterator] == null)
                    return;
                
                thisShot.transform.position = XWingCannons[shotIterator].transform.position;
                thisShot.transform.rotation = Quaternion.Euler(90, 0, 0);
                thisShot.gameObject.SetActive(true);
                StartCoroutine(LightUpCannon(shotIterator));
                shotIterator += 1;
                if (shotIterator > 3)
                    shotIterator = 0;
            }
        }

        IEnumerator LightUpCannon(int CannonToFire)
        {
            if (XWingCannons[CannonToFire] != null && !XWingCannons[CannonToFire].GetComponent<AudioSource>().isPlaying)
                XWingCannons[CannonToFire].GetComponent<AudioSource>().Play();
            
            XWingCannons[CannonToFire].GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.15f);
            XWingCannons[CannonToFire].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}