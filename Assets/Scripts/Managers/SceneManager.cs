using System;
using System.Collections;
using Systems;
using Base_Project._Scripts.Game_Events;
using Base_Project._Scripts.GameData;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        public FloatVariable PlayerSpeed;
        public FloatVariable MaxPlayerSpeed;
        public FloatVariable DistanceCovered;

        public float DestinationDistance;
        public float LevelTime;

        public float accelerateDuration = 2f;

        public GameEvent WonGame;
        public GameEvent LostGame;
        public bool GameInMotion;
        public GameEvent AutoPilotSuccess;
        public GameEvent AutoPilotFail;

        //These elements for display in the HUD while playing
        public StringVariable DistanceLeft;
        private float CurrentTime;
        private bool PlayerCrashRecorded;
        private bool PlayerSuccessRecorded;

        //We need a reference to this in the Scene Manager component
        //because after a Win or Lose, this component is turned off so that AutoPilot scripts can run.
        //If we're restarting the level, we need to be able to turn this component back on again..

        public GameObject thePD;

        private void Start()
        {
            PlayerSpeed.Value = 0;
        }

        public void RestartLevel()
        {
            GameObject[] currentObstaclesOnPlayarea = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject go in currentObstaclesOnPlayarea)
            {
                Destroy(go);
            }

            thePD.SetActive(true);
            thePD.GetComponent<PlayableDirectorManager>().QueueRestartLevel();
            GameObject.FindWithTag("XWing").GetComponent<XWingManager>().SetupForRestart();
            DistanceCovered.Value = 0;
            PlayerCrashRecorded = false;
            PlayerSuccessRecorded = false;
        }

        public void ScaleUpSpeed()
        {
            //This trigger is the Level Start
            StartCoroutine(ScaleSpeedUp());

            //TODO - These should be RemoteConfig values that can be fetched for each level
            DistanceLeft.Value = DestinationDistance.ToString();
        }

        public void ScaleDownSpeed()
        {
            StartCoroutine(ScaleSpeedDown());
        }

        public void HandlePlayerCrashed()
        {
            if (!PlayerCrashRecorded && !PlayerSuccessRecorded)
            {
                PlayerCrashRecorded = true;
                StartCoroutine(PlayerCrashed());
                ScaleDownSpeed();
            }
        }

        IEnumerator PlayerCrashed()
        {
            Debug.Log("IEnumerator PlayerCrash AutoPilot started");

            AutoPilotFail.Raise();
            yield return new WaitForSeconds(3f);
            LostGame.Raise();
        }

        private void Update()
        {
            if (GameInMotion && PlayerSpeed.Value >= 0.0f)
            {
                if (DistanceCovered.Value >= DestinationDistance)
                {
                    if (!PlayerCrashRecorded) //Could happen if user crashed just before getting to the end..
                    {
                        PlayerSuccessRecorded = true;
                        StartCoroutine(LevelFinale(true));
                        ScaleDownSpeed();
                        GameInMotion = false;
                    }
                }

                DistanceLeft.Value = (DestinationDistance - DistanceCovered.Value).ToString("F1");
            }

//            TimeLeft.Value = UITimeFormatter.FloatToTime(TimeLeft.Value - Time.deltaTime, "00:0");
        }

        IEnumerator LevelFinale(bool wonGame)
        {
            Debug.Log("IENumerator LevelFinale started");
            AutoPilotSuccess.Raise();
            yield return new WaitForSeconds(3f);
            WonGame.Raise();
        }

        IEnumerator ScaleSpeedUp()
        {
            GameInMotion = true;
            float SecondsToAccelerate = accelerateDuration;
            float startVal = 0;
            float rate = 1.0f / SecondsToAccelerate;

            for (float x = 0.0f; x <= 1.0f; x += Time.deltaTime * rate)
            {
                PlayerSpeed.Value = Mathf.Lerp(startVal, MaxPlayerSpeed.Value, x);
                yield return null;
            }

            PlayerSpeed.Value = MaxPlayerSpeed.Value;
        }

        IEnumerator ScaleSpeedDown()
        {
            float SecondsToDecelerate = 2f;
            float startVal = PlayerSpeed.Value;
            float targetVal = 0;
            float rate = 1.0f / SecondsToDecelerate;

            for (float x = 0; x <= 1.0f; x += Time.deltaTime * rate)
            {
                PlayerSpeed.Value = Mathf.Lerp(startVal, targetVal, x);
                yield return null;
            }

            PlayerSpeed.Value = 0;
            GameInMotion = false;
        }
    }
}