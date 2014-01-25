using UnityEngine;
using System.Collections;

public interface IFocusable
{
	void OnFocus ();

	bool IsFocusable ();
}
