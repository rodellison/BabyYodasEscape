using System;
using UnityEngine;

namespace Base_Project._Scripts.GameData
{
	[Serializable]
	[CreateAssetMenu (menuName="ScriptableObject/GameData/StringVariable")]
	public class StringVariable : ScriptableObject
	{
		[SerializeField]
		private string _value;
		public string Value
		{
			get => _value;
			set => _value = value;
		}
	}
}
