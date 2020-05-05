
using System;
using System.Collections;
using Base_Project._Scripts.GameData;
using UnityEngine;

namespace Systems
{
    public class LaserSystem : MonoBehaviour
    {

        public FloatVariable MaxLaserShotTime;
        public FloatVariable LaserSpeed;
        private bool collidedWithSomething;
        private void OnEnable()
        {
            StartCoroutine(AutoDestroy());
        }

        private void Update()
        {
            if (!collidedWithSomething)
                transform.Translate(Vector3.up * LaserSpeed.Value * Time.deltaTime );
        }

        IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(MaxLaserShotTime.Value);
            if (gameObject.activeSelf)
                gameObject.SetActive(false);   //This will return the object to the Laser pool
        }
    }
}