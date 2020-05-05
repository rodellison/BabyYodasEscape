using System.Collections.Generic;
using UnityEngine;

namespace Base_Project._Scripts.Game_Events
{
	[CreateAssetMenu(menuName="ScriptableObject/GameEvent/GameEventWithBool")]
	public class GameEventWithBool : ScriptableObject
	{
		private List<GameEventWithBoolListener> _listeners = new List<GameEventWithBoolListener>();
	
		public void Raise(bool value)
		{
			for (int i = _listeners.Count - 1; i >= 0; i--)
			{
				_listeners[i].OnEventRaised(value);
			}
		}

	
		public void RegisterListener(GameEventWithBoolListener listener)
		{
			if (!_listeners.Contains(listener))
			{
				_listeners.Add(listener);
			}
			else
			{
				Debug.LogWarning("Listener re-registered to event");
			}
		}
	
		public void UnregisterListener(GameEventWithBoolListener listener)
		{
			_listeners.Remove(listener);
		}
	
	}
}
