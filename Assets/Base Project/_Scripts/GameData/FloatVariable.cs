using System;
using UnityEngine;

namespace Base_Project._Scripts.GameData
{
	[Serializable]
	[CreateAssetMenu (menuName="ScriptableObject/GameData/FloatVariable")]
	public class FloatVariable : ScriptableObject
	{
		[SerializeField]
		private float _value;
		public float Value
		{
			get => _value;
			set => _value = value;
		}
	}
}
