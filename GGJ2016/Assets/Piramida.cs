using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piramida : MonoBehaviour {

    public static Piramida singleton;
    public Platforma[] platformy;

    Piramida()
    {
        singleton = this;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
