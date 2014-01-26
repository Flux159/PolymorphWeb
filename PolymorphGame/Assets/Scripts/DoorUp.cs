using UnityEngine;
using System.Collections;

public class DoorUp : MonoBehaviour, IFocusable
{
	public bool openDoor;
	public float smooth = 1;
	public float howFarUp = 4.0f;
	private Vector3 newPosition;
	private Transform thingToMove;

	void Awake ()
	{
		thingToMove = transform.parent;
		newPosition = thingToMove.position + Vector3.up * howFarUp;
	}

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