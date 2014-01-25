using UnityEngine;
using System.Collections;

public class MonsterFocus : MonoBehaviour, IFocusable
{
	public bool alive = true;

	public GameObject playerController;


	public bool OnFocus ()
	{

		GameObject monster = transform.gameObject;
		print ("monster name:" + monster.name);
		GameObject newPlayerController = null;
		newPlayerController = (GameObject)Instantiate (playerController,
		     monster.transform.position, monster.transform.rotation);

		GameObject.Destroy (this);

		return true;
	}

	public bool IsFocusable ()
	{
		return alive;
	}
}
