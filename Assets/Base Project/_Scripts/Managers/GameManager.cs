using System.Collections;
using Base_Project._Scripts.GameData;
using UnityEngine;

namespace Base_Project._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
  
        public IntVariable SceneToLoad;  //GameManager should manage this if there are multiple scenes available..
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            // If a second version is created, delete it immediately
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;
            // Make the singleton persist between scenes
            DontDestroyOnLoad(this.gameObject);
        }


        // Click to exit the app entirely
        public void QuitApplication()
        {
            StartCoroutine(QuitTheApplication());
        }

        IEnumerator QuitTheApplication()
        {
            //Using WaitForSecondsRealtime (instead of WaitForSeconds), as the user
            //may have initiated Exit by holding the Pause key, which sets Time.timeScale = 0
            yield return new WaitForSecondsRealtime(3f);
            Debug.Log("Exiting Game");

#if UNITY_EDITOR || UNITY_EDITOR_64
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
#else
        			Application.Quit();
#endif
        }
    }
}