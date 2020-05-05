using System.Collections.Generic;
using UnityEngine;

namespace Base_Project._Scripts.Game_Events
{
	[CreateAssetMenu(menuName="ScriptableObject/GameEvent/GameEvent")]
	public class GameEvent : ScriptableObject
	{
		private List<GameEventListener> listeners = new List<GameEventListener>();

		public void Raise()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
			{
				listeners[i].OnEventRaised();
			}
		}

		public void RegisterListener(GameEventListener listener)
		{
			if (!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
			else
			{
				Debug.LogWarning("Listener reregistered to event");
			}
		}

		public void UnregisterListener(GameEventListener listener)
		{
			listeners.Remove(listener);
		}

	}
}
