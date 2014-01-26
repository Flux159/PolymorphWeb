using UnityEngine;
using System.Collections;

public class StartMenuButtonFocus : MonoBehaviour, IFocusable
{
	
	public virtual bool OnFocus ()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
		return false;
	}
	
	public virtual bool IsFocusable ()
	{
		return true;
	}
}
