using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base_Project._Scripts.Game_Events
{

	[CreateAssetMenu(menuName="ScriptableObject/GameEvent/GameEventWithString")]
	public class GameEventWithString : ScriptableObject
	{
		private List<GameEventWithStringListener> _listeners = new List<GameEventWithStringListener>();
	
		public void Raise(String value)
		{
			for (int i = _listeners.Count - 1; i >= 0; i--)
			{
				_listeners[i].OnEventRaised(value);
			}
		}

	
		public void RegisterListener(GameEventWithStringListener listener)
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
	
		public void UnregisterListener(GameEventWithStringListener listener)
		{
			_listeners.Remove(listener);
		}
	
	}
}
