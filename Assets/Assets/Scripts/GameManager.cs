using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text OverlayText;
	public Text PromptText;
	public FlashLight PromptLight;
	public float TotalCPRTime;

	[Header("Setup Stage")]
	public string SetupMessage;

	public GameObject HandController;

	[Header("Briefing Stage")]
	public string BriefingMessage;

	public float BriefingMessageDuration;

	[Header("Emergency Call Stage")]
	public string CallingMessage;

	public HandTouchDetector CallingTarget;

	[Header("Compression Stage")]
	public string CompressionMessage;

	public int CompressionTargetCount = 30;
	public HandTouchDetector CompressionTarget;

	[Header("Breath Stage")]
	public string BreathMessage;

	public Transform Head;
	public Vector3 BreathPosition = new Vector3 (0.5f, -0.6f, -1f);

	public float BreathTargetDuration;
	public float BreathDuration;
	public HandTouchDetector BreathTarget;

	[Header("End Stage")]
	public string VictoryMessage;

	public float VictoryDuration;

	[Header("Debug")]
	public bool TriggerNext;



	private float ElapsedTime;

	void Start () {

		CompressionTarget.DisableObject ();
		CompressionTarget.ResetObject ();
		BreathTarget.DisableObject ();
		BreathTarget.ResetObject ();
		CallingTarget.DisableObject ();
		CallingTarget.ResetObject ();

		OverlayText.text = "";
		PromptText.text = "";

		ElapsedTime = 0f;

		StartCoroutine (GameLoop ());

	}
		
	IEnumerator GameLoop () {

		while (true) {

			yield return StartCoroutine (Setup ());
			yield return StartCoroutine (Briefing ());
			yield return StartCoroutine (EmergencyCall ());

			while (ElapsedTime <= TotalCPRTime) {
				yield return StartCoroutine (Compressions ());
				yield return StartCoroutine (Breaths ());
			}

			yield return StartCoroutine (Ending ());

			Reset ();

		}

	}

	IEnumerator Setup() {

		PromptText.text = SetupMessage;
		PromptLight.Flash ();

		while (true) { // break once both hands appear in scene

			if (HandController.GetComponentsInChildren<HandModel>().Length >= 3) {

				break;

			}

			if (TriggerNext) {
				TriggerNext = false;
				yield break;
			}

			yield return null;

		}

	}

	IEnumerator Briefing () {

		PromptText.text = BriefingMessage;
		PromptLight.Flash ();

		float count = 0f;

		while (count <= BriefingMessageDuration) { // display message for set amount of time

			if (TriggerNext) {
				TriggerNext = false;
				yield break;
			}

			count += Time.deltaTime;
			yield return null;

		}

	}

	IEnumerator EmergencyCall () {

		PromptText.text = CallingMessage;
		PromptLight.Flash ();

		CallingTarget.EnableObject ();
		OverlayText.text = "Grab the phone!";

		while (CallingTarget.TouchCounter < 1) {

			if (TriggerNext) {
				TriggerNext = false;
				yield break;
			}

			yield return null;

		}

		OverlayText.text = "Done calling!";

		CallingTarget.DisableObject ();
		CallingTarget.ResetObject ();

	}

	IEnumerator Compressions () {

		PromptText.text = CompressionMessage;
		PromptLight.Flash ();

		CompressionTarget.EnableObject ();
		OverlayText.text = "Compresses: " + CompressionTarget.TouchCounter;

		while (CompressionTarget.TouchCounter < CompressionTargetCount) { // wait until compression target is reached

			OverlayText.text = "Compresses: " + CompressionTarget.TouchCounter;

			if (TriggerNext) {
				TriggerNext = false;
				yield break;
			}

			ElapsedTime += Time.deltaTime;
			yield return null;

		}

		OverlayText.text = "Complete!";

		CompressionTarget.DisableObject ();
		CompressionTarget.ResetObject ();

	}

	IEnumerator Breaths () {

		PromptText.text = BreathMessage;
		PromptLight.Flash ();

		BreathTarget.EnableObject ();

		while (BreathTarget.TouchDuration < BreathTargetDuration) { // wait until head is grasped

			if (TriggerNext) {
				TriggerNext = false;
				break;
			}

			ElapsedTime += Time.deltaTime;
			yield return null;

		}

		OverlayText.text = "Checking for Breathing...";
		yield return StartCoroutine (MovingCamera2Head ());

		float count = 0f;

		while (count <= BreathDuration) { // wait until "breathing" is done

			if (count <= BreathDuration * 0.25f)
				OverlayText.text = "Breathe!";
			else if (count > BreathDuration * 0.25f && count <= BreathDuration * 0.5f)
				OverlayText.text = "Retilt Head...";
			else if (count > BreathDuration * 0.5f && count <= BreathDuration * 0.75f)
				OverlayText.text = "Breathe!";
			else
				OverlayText.text = "";

			count += Time.deltaTime;

			yield return null;

		}

		yield return StartCoroutine (MovingCameraBackFromHead ());

		BreathTarget.DisableObject ();
		BreathTarget.ResetObject ();

	}

	IEnumerator MovingCamera2Head () {

		float count = 0f;

		while (Head.localPosition != BreathPosition && count < 1.3f) {

			Vector3 diff = BreathPosition - Head.localPosition;
			Head.localPosition += diff.normalized * Time.deltaTime;

			count += Time.deltaTime;

			yield return null;

		}

		Head.localPosition = BreathPosition;

	}

	IEnumerator MovingCameraBackFromHead () {

		float count = 0f;

		Vector3 targetPos = new Vector3 (0.0f, 0.1f, 0.0f);

		while (Head.localPosition != targetPos && count < 1.3f) {

			Vector3 diff = targetPos - Head.localPosition;
			Head.localPosition += diff.normalized * Time.deltaTime;

			count += Time.deltaTime;

			yield return null;

		}

		Head.localPosition = targetPos;

	}
		
	IEnumerator Ending () {

		PromptText.text = VictoryMessage;
		PromptLight.Flash ();

		float count = 0f;

		while (count <= VictoryDuration) {

			if (TriggerNext) {
				TriggerNext = false;
				yield break;
			}

			count += Time.deltaTime;
			yield return null;

		}

	}

	void Reset () {

		// reset all for new game

		UnityEngine.SceneManagement.SceneManager.LoadScene (0);

	}

}
