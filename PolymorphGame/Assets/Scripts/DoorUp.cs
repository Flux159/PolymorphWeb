using UnityEngine;
using System.Collections;

public class DoorUp : MonoBehaviour, IFocusable
{
	public bool openDoor;
	private Vector3 newPosition;
	public float smooth = 1;

	void Awake ()
	{
		newPosition = transform.position + Vector3.up * 3;
	}
	// Update is called once per frame
	void Update ()
	{
		if (openDoor) {
			PositionChanging ();
		}
	}
	void PositionChanging ()
	{

		transform.position = Vector3.Lerp (transform.position, newPosition, smooth * Time.deltaTime);
	}

	public bool OnFocus ()
	{
		openDoor = true;
		return false;
	}
	
	public bool IsFocusable ()
	{
		return ! openDoor;
	}
}