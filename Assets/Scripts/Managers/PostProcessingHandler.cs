using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Managers
{
    public class PostProcessingHandler : MonoBehaviour
    {
        private Camera theMainCamera;

   
        public void EnablePostProcessing(bool isEnabled)
        {
            //Getting MainCamera on the Fly, as at Startup.. if using the Base System Manager, there MAY not be a camera
            //live yet..
            theMainCamera = Camera.main;
            if (theMainCamera == null)
                return;

            //If we get into this method, then that means there must be an active Main Camera available..
            theMainCamera.GetComponent<Volume>().enabled = isEnabled;
        }
    }
}