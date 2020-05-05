using System;
using UnityEngine;

namespace Base_Project._Scripts.GameData
{
	[Serializable]
	[CreateAssetMenu (menuName="ScriptableObject/GameData/IntVariable")]
	public class IntVariable : ScriptableObject
	{
		[SerializeField]
		private int _value;
		public int Value
		{
			get => _value;
			set => _value = value;
		}
	}
}
