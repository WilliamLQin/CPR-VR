using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVRHeadOverride : MonoBehaviour {

	public GameObject VRHead;
	private GvrHead VRHeadScript;

	IEnumerator Start () {

		yield return null;

		VRHeadScript = VRHead.GetComponent<GvrHead> ();
		VRHeadScript.alternativeHead = transform;

	}


}
