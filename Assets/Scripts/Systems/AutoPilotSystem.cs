using System;
using Base_Project._Scripts.GameData;
using UnityEngine;

namespace Systems
{
    public class AutoPilotSystem : MonoBehaviour
    {
        public float FlyAwaySpeed;
        private bool AutoPilotEnabled;

        private void OnEnable()
        {
            AutoPilotEnabled = true;
            Debug.Log("AutoPilot enabled");
        }

        private void Update()
        {
            if (AutoPilotEnabled)
                transform.Translate(Vector3.forward * (FlyAwaySpeed * Time.deltaTime));
        }
    }
}