using UnityEngine;
using System.Collections;
using Leap;

public class Phone_Collision : MonoBehaviour {

	int counter = 0;
	bool alreadyDone = false;

	private HandModel GetHand(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel>())
			return other.transform.parent.parent.GetComponent<HandModel>();
		else
			return null;
	}

	void OnTriggerEnter(Collider other)
	{
		HandModel hand_model = GetHand (other);
		if (!alreadyDone) {
			if (hand_model != null) {
				alreadyDone = true;
				counter = counter + 1;
				//play sound
				return;			
			}
		} else {
		}

	}

	void OnTriggerExit(Collider other){

	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		print (counter);
	}
}