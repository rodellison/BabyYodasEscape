using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Managers
{
    public class PlayableDirectorManager : MonoBehaviour
    {
        [SerializeField] private float restartAtSecondsMark;

        public void QueueRestartLevel()
        {
            Debug.Log("Requeuing Level PlayableDirector");
            PlayableDirector thisPD = GetComponent<PlayableDirector>();
            thisPD.initialTime = Convert.ToDouble(restartAtSecondsMark);
            thisPD.Play();
        }
    }
}