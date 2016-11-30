using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour {

	public Animator animator;
	

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		
	}

	
	// Update is called once per frame
	void Update () {
		Animate();
		
	}

	public void setText(int score){

		if (score > 0) {
			GetComponent<TextMesh>().color = Color.green; 
	    	GetComponent<TextMesh>().text = "+" + score + " pts";

		} else {
			GetComponent<TextMesh>().color = Color.red; 
			GetComponent<TextMesh>().text = score + " pts";
		}
	}

	public void Animate(){
	
	if (animator.GetAnimatorTransitionInfo(0).nameHash == 3705433) {
		Destroy(gameObject.transform.parent.gameObject);
	}

	}
}
