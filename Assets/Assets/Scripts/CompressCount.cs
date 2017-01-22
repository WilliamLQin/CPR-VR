using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompressCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}

	//call this when compressions start
	void startCompressions (){
		gameObject.SetActive (true);
		GetComponent<Text> ().text = "Compressions Done: \n" + 0;
	}

	//cal this and pass an int
	void updateCompressionsTest(int n){
		gameObject.SetActive (true);
		GetComponent<Text> ().text = "Compressions Done: \n" + n;
	}

	// Update is called once per frame
	void Update () {
	}
}
