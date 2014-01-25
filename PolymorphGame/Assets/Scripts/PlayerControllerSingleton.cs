using UnityEngine;
using System.Collections;

public class PlayerControllerSingleton : MonoBehaviour
{

		private static PlayerControllerSingleton instance;

		public GameObject playerController;

		private PlayerControllerSingleton ()
		{
		}
	
		public static PlayerControllerSingleton Instance {
				get {
						if (instance == null) {
								instance = new PlayerControllerSingleton ();
						}
						return instance;
				}
		}
}
