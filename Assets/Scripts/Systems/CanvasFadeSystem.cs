
using System;
using System.Collections;
using UnityEngine;

namespace Systems
{
    public class CanvasFadeSystem : MonoBehaviour
    {
        private CanvasGroup cg;
        public float FadeTime = 0.5f;

        private void Start()
        {
            cg = GetComponent<CanvasGroup>();
        }

        public void FadeUpCanvas()
        {
            StartCoroutine(FadeUp());
        }

        public void FadeDownCanvas()
        {
            StartCoroutine(FadeDown());
        }

        IEnumerator FadeUp()
        {
            float startVal = 0;
            float rate = 1.0f / FadeTime;

            for (float x = 0.0f; x <= 1.0f; x += Time.deltaTime * rate)
            {
                cg.alpha = Mathf.Lerp(startVal, 1f, x);
                yield return null;
            }

            cg.alpha = 1.0f;
        }
        
        IEnumerator FadeDown()
        {
            float startVal = 1.0f;
            float targetVal = 0;
            float rate = 1.0f / FadeTime;

            for (float x = 0; x <= 1.0f; x += Time.deltaTime * rate)
            {
                cg.alpha = Mathf.Lerp(startVal, targetVal, x);
                yield return null;
            }
            cg.alpha = 0;
        }
    }
}