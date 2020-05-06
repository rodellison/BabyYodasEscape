using System.Collections;
using Base_Project._Scripts.Game_Events;
using Base_Project._Scripts.GameData;
using Managers;
using UnityEngine;

namespace Base_Project._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        //GameManager should manage this if there are multiple scenes or levels available..
        public IntVariable SceneToLoad;
        public IntVariable levelToLoad;
        public GameEvent LevelLoaded;
        public static GameManager Instance { get; private set; }


        public void StartGame()
        {
            LoadScene(SceneToLoad.Value);
        }

        /// <summary>
        /// Separate LoadScene function so that it can invoked outside of a New Game type start, by way of event, etc.
        /// </summary>
        /// <param name="SceneToLoad"></param>
        public void LoadScene(int SceneToLoad)
        {
            GetComponent<SceneLoader>().LoadScene(SceneToLoad);
        }
        public void LoadLevel(int LevelToLoad)
        {
            //Provided as a separate method for getting/setting level related properties and data (e.g. Remote Config type stuff), just in case 
            //loading the Scene alone doesn't have all the components setup in the needed state for the particular level..

            //...do stuff, set values, then tell any components listening that the level is loaded

            LevelLoaded.Raise();
        }

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