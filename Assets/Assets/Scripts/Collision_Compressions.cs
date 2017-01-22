using UnityEngine;
using System.Collections;
using Leap;

public class Collision_Compressions : MonoBehaviour {

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
			alreadyDone = true;
			if (hand_model != null) {
				counter = counter + 1;
				return;			
			}
		} else {
			alreadyDone = true;
		}

	}

	void OnTriggerExit(Collider other){
		HandModel hand_model = GetHand (other);
		if (hand_model == null) {
			alreadyDone = false;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		print (counter);

	}
}