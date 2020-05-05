using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace baseProject
{
	public class SettingsPausePanel : MonoBehaviour
	{

		// Click to exit the app entirely
		public void ClickQuitButton()
		{
			Debug.Log("QUITTING @ " + Time.timeSinceLevelLoad + " with title screen button");

#if UNITY_EDITOR || UNITY_EDITOR_64
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
			
		}
	}
}
