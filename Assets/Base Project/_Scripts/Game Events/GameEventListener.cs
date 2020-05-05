using UnityEngine;
using UnityEngine.Events;

namespace Base_Project._Scripts.Game_Events
{
	public class GameEventListener : MonoBehaviour
	{

		public GameEvent Event;
		public UnityEvent Response;

		private void OnEnable()
		{
			Event.RegisterListener(this);
		}

		private void OnDisable()
		{
			Event.UnregisterListener(this);
		}

		public void OnEventRaised()
		{
			Response.Invoke();
		}

	}
}
