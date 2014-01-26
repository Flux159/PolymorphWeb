using UnityEngine;
using System.Collections;

public class StartMenuButtonFocus : MonoBehaviour, IFocusable
{
	
	public bool OnFocus ()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
		return false;
	}
	
	public bool IsFocusable ()
	{
		return true;
	}
}
