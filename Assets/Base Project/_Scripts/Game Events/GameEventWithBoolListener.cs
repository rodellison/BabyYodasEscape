using System;
using UnityEngine;
using UnityEngine.Events;

namespace Base_Project._Scripts.Game_Events
{
    [Serializable]
    public class UnityEventBool : UnityEvent<bool>
    {
    }

    public class GameEventWithBoolListener : MonoBehaviour
    {
        public GameEventWithBool @event;
        public UnityEventBool @response;

        private void OnEnable()
        {
            @event.RegisterListener(this);
        }

        private void OnDisable()
        {
            @event.UnregisterListener(this);
        }

        public void OnEventRaised(bool value)
        {
            @response.Invoke(value);
        }
    }
}