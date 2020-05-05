using System.Collections;
using System.Collections.Generic;
using Base_Project._Scripts.Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace baseProject {

	// This class will link audio slider to AudioMixers in option menus
	public class VolumeSlider : MonoBehaviour
	{
		public AudioMixer audioMixer;
		public Slider slider;

		private void Awake()
		{
			slider = GetComponent<Slider>();

		}

		private void OnEnable()
		{
			CopyMixerValueToUi();
		}

		private void OnDisable()
		{
			CopyMixerValueToUi();
			CopyMixerValueToPlayerPrefs();
		}

		public void OnUpdateSlider()
		{
			audioMixer.SetFloat(gameObject.name, slider.value);
		}

		private void CopyMixerValueToUi()
		{
			float vol = 0f;
			audioMixer.GetFloat(gameObject.name, out vol);
			slider.value = vol;
		}

		private void CopyMixerValueToPlayerPrefs()
		{
			PlayerPrefsManager.SetVolume(gameObject.name, Mathf.RoundToInt(slider.value));
		}
	}
}