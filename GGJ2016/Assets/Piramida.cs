﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piramida : MonoBehaviour {

    public static Piramida singleton;
    public Platforma[] platformy;

	// Use this for initialization
	void Start () {
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}