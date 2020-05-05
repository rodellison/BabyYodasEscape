using UnityEngine;
using UnityEngine.Audio;

namespace Base_Project._Scripts.Managers
{
	public class AudioManager : MonoBehaviour
	{
		public AudioMixer audioMixer;

		void Start()
		{
			// Immediately fetch previously saved audio levels from preferences
			audioMixer.SetFloat("MasterVol", PlayerPrefsManager.GetVolume("MasterVol"));
			audioMixer.SetFloat("MusicVol", PlayerPrefsManager.GetVolume("MusicVol"));
			audioMixer.SetFloat("EffectsVol", PlayerPrefsManager.GetVolume("EffectsVol"));
			audioMixer.SetFloat("InterfaceVol", PlayerPrefsManager.GetVolume("InterfaceVol"));
		}

		private void OnDisable()
		{
			float vol = 0f;
			audioMixer.GetFloat("MasterVol", out vol);
			PlayerPrefsManager.SetVolume("MasterVol", vol);
			audioMixer.GetFloat("MusicVol", out vol);
			PlayerPrefsManager.SetVolume("MusicVol", vol);
			audioMixer.GetFloat("EffectsVol", out vol);
			PlayerPrefsManager.SetVolume("EffectsVol", vol);
			audioMixer.GetFloat("InterfaceVol", out vol);
			PlayerPrefsManager.SetVolume("InterfaceVol", vol);
			
			PlayerPrefsManager.Save();
		}
	}
}
