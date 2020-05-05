using System.Collections;
using Base_Project._Scripts.Game_Events;
using UnityEngine;
using Managers;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Base_Project._Scripts.Managers
{
    public class UIManager : MonoBehaviour, InputActionControls.IUIActions
    {
        public GameObject
            SettingsPausePanel; //treating this UI slightly differently as during it's presentation, we'll stop time

        public float buttonHoldTimeToQuit = 1f;
        public GameObject[] panelsToManage;
        public GameEvent QuitEvent;
        private bool isPaused;

        private InputActionControls pauseControls;

        private void OnEnable()
        {
            pauseControls = new InputActionControls();
            pauseControls.UI.SetCallbacks(this);
            pauseControls.Enable();
        }
        public void SwitchToPanel(string panelToPresent)
        {
            foreach (GameObject g in panelsToManage)
            {
                g.SetActive(g.gameObject.name.Equals(panelToPresent));
            }
        }

        public void SceneStarted()
        {
            foreach (GameObject g in panelsToManage)
            {
                g.SetActive(false);
                isPaused = false;
            }
        }

        public void SceneEnded()
        {
            Debug.Log("Unloading Scene");
            //Note, below doesn't work if there is no Scene that can be made active, and we're on the last scene..
            // Scene initialScene = SceneManager.GetSceneByBuildIndex(0);
            // SceneManager.SetActiveScene(initialScene);
            // SceneManager.UnloadSceneAsync(1);
        }

        private void Start()
        {
            SwitchToPanel("TitlePanel");
        }

  // Alternates between pause and unpaused statuses
        public void PauseAndUnpause()
        {
            isPaused = !isPaused;
            Debug.Log("Pause state: " + isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
            SettingsPausePanel.SetActive(isPaused);
            
        }

        public void InitiateExitGame()
        {
            SwitchToPanel("EndGameCreditsPanel");
            QuitEvent.Raise();
        }

        // Counts time while esc button is being held down, eventually resulting in app being exited.  Cancelled when esc is released early
        IEnumerator QuitGameTimer()
        {
            // Wait as the player holds down the esc key until quit time
           yield return new WaitForSecondsRealtime(buttonHoldTimeToQuit);
            Debug.Log("QUITTING @ " + Time.timeSinceLevelLoad + " with esc key");
            InitiateExitGame();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            Debug.Log(context);
            
            // // When the esc key is pressed
             if (Keyboard.current[Key.Backquote].wasPressedThisFrame ||
                 Gamepad.current[GamepadButton.North].wasPressedThisFrame)
             {
                 // Start counting to quit time
                 StartCoroutine("QuitGameTimer");
                 PauseAndUnpause();
             } // When the esc key is released
             else if (Keyboard.current[Key.Backquote].wasReleasedThisFrame||
                      Gamepad.current[GamepadButton.North].wasReleasedThisFrame)
             {
                 // Stop counting to quit time
                 StopCoroutine("QuitGameTimer");
             }
        }
    }
}