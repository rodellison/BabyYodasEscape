using UnityEngine;

namespace Base_Project._Scripts.Managers
{
	public static class PlayerPrefsManager
	{
		[RuntimeInitializeOnLoadMethod]
		static void OnRuntimeMethodLoad()
		{
			if (!PlayerPrefs.HasKey("MasterVol"))
			{
				PlayerPrefs.DeleteAll();
				Init();
			}
		}

		static void Init()
		{
			SetVolume("MasterVol", 0f);
			SetVolume("MusicVol", 0f);
			SetVolume("EffectsVol", 0f);
			SetVolume("InterfaceVol", 0f);
		}

		public static void Save()
		{
			Debug.Log("Saving Player Prefs");
			PlayerPrefs.Save();
		}

		public static void SetVolume(string volumeName, float vol)
		{
			PlayerPrefs.SetFloat(volumeName, vol);
		}

		public static float GetVolume(string volumeName)
		{
			var vol = PlayerPrefs.GetFloat(volumeName);
			return vol;
		}
	}
}
