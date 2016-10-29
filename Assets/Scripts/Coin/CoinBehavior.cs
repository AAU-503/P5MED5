using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour {


	void Update () {
		//Object rotates on Y-axis at a set speed, independent of frame rate.
		transform.Rotate(0, Time.deltaTime * 200, 0, Space.World);
		//Score is changed in PlayerController script
	}
}
