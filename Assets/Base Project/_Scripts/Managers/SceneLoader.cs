using System.Collections;
using Base_Project._Scripts.Game_Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base_Project._Scripts.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        bool isLoading = false;
        private AsyncOperation async;
        public GameEvent StartScene;

        private void Start()
        {
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        }

        private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Debug.Log("Loaded Scene: " + arg0.name + ", index: " + arg0.buildIndex);
            //Set Parms, etc. as a result of the new scene finished loaded and is being presented...
            StartScene.Raise();
        }

        // When the button is clicked, the new button will be loaded
        public void StartSceneLoad()
        {
            if (!isLoading)
            {
                StartCoroutine(LoadScene(GameManager.Instance.SceneToLoad.Value));
            }
        }

        // New level is loaded asynchronously in case it's a very large level. Can add loading effects here.
        IEnumerator LoadScene(int level)
        {
            isLoading = true;
            async = SceneManager.LoadSceneAsync(level);
            //This allows a lot of control, but have to be careful in it's use..
            async.allowSceneActivation = false;

            while (!async.isDone)
            {
                yield return new WaitForSeconds(0.1f);
                // Check if the load has finished
                if (async.progress >= 0.9f)
                {
                    //Wait to you press the space key to activate the Scene
                    yield return doSomethingLoop();
                    //if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
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