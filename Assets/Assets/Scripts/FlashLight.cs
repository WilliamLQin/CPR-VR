using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {

	public GameObject FlashingLight;
	public float DefaultFlashSpeed;
	public float DefaultFlashLength;

	public bool DebugFlash;

	private bool IsFlashing = false;

	void Start () {

		FlashingLight.SetActive (false);
		
	}

	void Update () {

		if (DebugFlash) {
			Flash ();
			DebugFlash = false;
		}

	}

	void LightOn () {
		FlashingLight.SetActive (true);
	}

	void LightOff () {
		FlashingLight.SetActive (false);
	}

	public void Flash (float flashSpeed = -1f, float flashLength = -1f) {
		if (IsFlashing)
			return;
		
		if (flashSpeed == -1f)
			flashSpeed = DefaultFlashSpeed;

		if (flashLength == -1f)
			flashLength = DefaultFlashLength;
	
		StartCoroutine (LightFlashing(flashSpeed, flashLength));
	}

	IEnumerator LightFlashing (float flashSpeed, float flashLength) {

		IsFlashing = true;

		for (float i = 0f; i < flashLength; i += flashSpeed) {

			LightOn ();
			yield return new WaitForSeconds (flashSpeed * 0.5f);

			LightOff ();
			yield return new WaitForSeconds (flashSpeed * 0.5f);

		}

		LightOff ();
		IsFlashing = false;

	}
}
