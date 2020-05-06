using System.Collections;
using Base_Project._Scripts.Game_Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base_Project._Scripts.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        //SceneLoader isn't concerned with managing which scene is next, etc. type logic - that should be handled
        //by the GameManager.. This class just handles loading and unloading of Scenes..

        bool isLoading = false;
        private AsyncOperation async;
        public GameEvent SceneLoaded;

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log("Loaded Scene: " + arg0.name + ", index: " + arg0.buildIndex);
            //Set Parms, etc. as a result of the new scene finished loaded and is being presented...
            SceneLoaded.Raise();
        }

        // When the button is clicked, the new button will be loaded
        public void LoadScene(int SceneToLoad)
        {
            if (!isLoading)
            {
                StartCoroutine(AsyncSceneLoader(SceneToLoad));
            }
        }

        // New level is loaded asynchronously. Can add loading effects here.
        IEnumerator AsyncSceneLoader(int level)
        {
            isLoading = true;
            async = SceneManager.LoadSceneAsync(level);
            //async.allowSceneActivation (in combination with some code in the while loop below)
            //allows fine grained control for when to activate the scene.. if not needed, just keep this stuff commented out
            //async.allowSceneActivation = false;

            while (!async.isDone)
            {
                yield return new WaitForSeconds(0.1f);
                // Check if the load has finished
                if (async.progress >= 0.9f)
                {
                    //Use an IEnumerator or an Input 
                    //yield return doSomethingLoop();
                    //if (Input.GetKeyDown(KeyCode.Space))
                    //to control when to Activate the Scene
                    async.allowSceneActivation = true;
                }
            }

            isLoading = false;
        }

        IEnumerator doSomethingLoop()
        {
            //Just before presenting the Asynchronously loaded new scene, do something?
            yield return new WaitForSeconds(1f);
        }
    }
}