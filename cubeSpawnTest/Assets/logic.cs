﻿using UnityEngine;
using System.Collections;

public class logic : MonoBehaviour {

	public GameObject go;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Instantiate (go);
	}
}
