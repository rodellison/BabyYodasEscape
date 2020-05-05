using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetLinkButton : MonoBehaviour
{
	// Open up an external URL, useful for social media
    public void OnClickLink(string url)
	{
		Application.OpenURL(url);
	}
}
