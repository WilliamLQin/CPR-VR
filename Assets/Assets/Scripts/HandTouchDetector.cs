using UnityEngine;
using System.Collections;
using Leap;

public class HandTouchDetector : MonoBehaviour {

	public bool DetectHold;
	public bool ResetOnRelease;
	private bool Triggered;

	[HideInInspector] public int TouchCounter = 0;
	[HideInInspector] public float TouchDuration = 0f;

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
		if (hand_model == null)
			return;

		if (DetectHold) {
			TouchDuration += Time.deltaTime;
			return;
		}

		if (!Triggered) {
			Triggered = true;
			TouchCounter++;
			TouchDuration += Time.deltaTime;
		}

	}

	void OnTriggerExit(Collider other){

		HandModel hand_model = GetHand (other);

		if (hand_model != null) {
			Triggered = false;

			if (ResetOnRelease)
				TouchDuration = 0f;
		}

	}

	public void EnableObject(){
		gameObject.SetActive (true);
	}
	public void DisableObject(){
		gameObject.SetActive (false);
	}
	public void ResetObject(){
		TouchCounter = 0;
		TouchDuration = 0f;
	}
}