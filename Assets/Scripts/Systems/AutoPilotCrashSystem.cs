using System;
using System.Collections;
using Base_Project._Scripts.GameData;
using UnityEngine;

namespace Systems
{
    public class AutoPilotCrashSystem : MonoBehaviour
    {
        private bool AutoPilotEnabled;
        private float secondsToLand = 2;

        private void OnEnable()
        {
            StartCoroutine(LandXWing());
        }
    
        IEnumerator LandXWing()
        {
            float startingHeight = transform.position.y;
            float rate = 1.0f / secondsToLand;
            Vector3 position = transform.position;

            for (float x = 0.0f; x <= 1.0f; x += Time.deltaTime * rate)
            {
                position.y = Mathf.Lerp(startingHeight, 0f, x);
                yield return null;
                transform.position = position;
            }      

        }
    }
}