using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TileControl : MonoBehaviour {

	public Material inactiveMaterial;
	public Material gazedAtMaterial;

	void Start() {
		SetGazedAt(false);
	}

	void LateUpdate() {
		GvrViewer.Instance.UpdateState();
		if (GvrViewer.Instance.BackButtonPressed) {
			Application.Quit();
		}
	}

	public void SetGazedAt(bool gazedAt) {
		if (inactiveMaterial != null && gazedAtMaterial != null) {
			GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
			return;
		}
		GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
	}
}