using UnityEngine;
using System.Collections;

public class DoorUp : MonoBehaviour, IFocusable
{
	public bool openDoor;
	private Vector3 newPosition;
	public float smooth = 1;
	public Transform thingToMove;

	void Awake ()
	{
		newPosition = thingToMove.position + Vector3.up * 4;
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

		thingToMove.position = Vector3.Lerp (thingToMove.position, newPosition, smooth * Time.deltaTime);
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