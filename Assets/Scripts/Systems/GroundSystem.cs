using System;
using Base_Project._Scripts.GameData;
using UnityEngine;

namespace Systems
{
    public class GroundSystem : MonoBehaviour
    {
        public FloatVariable DistanceCovered;
        public Material GroundMaterial;
        public FloatVariable PlayerSpeed;
        private float TilingOffsetValue;
        private float OffsetSpeed;
        private bool atTargetSpeed;


        private void Start()
        {
            SetupLevelTilingPosition();
        }

        public void SetupLevelTilingPosition()
        {
            TilingOffsetValue = 0;
            GroundMaterial.SetFloat("_TilingYOffset", 0f);
            DistanceCovered.Value = 0;
        }

        private void Update()
        {
            if (PlayerSpeed.Value > 0.01f)
            {
                UpdateTextureScroll();
            }
            else
            {
                TilingOffsetValue = GroundMaterial.GetFloat("_TilingYOffset");
            }
        }

        private void UpdateTextureScroll()
        {
            TilingOffsetValue = TilingOffsetValue - Time.deltaTime * PlayerSpeed.Value;
            GroundMaterial.SetFloat("_TilingYOffset", TilingOffsetValue);
            DistanceCovered.Value = Math.Abs(TilingOffsetValue);
        }
    }
}