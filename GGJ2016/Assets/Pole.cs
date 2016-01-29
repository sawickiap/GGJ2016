using UnityEngine;
using System.Collections;

public class Pole : MonoBehaviour {

    public Platforma platforma;
    public Pole nastepnik;
    public bool naSkrzyzowaniu;
    public int nrSkrzyzowania;

    public Pole GetNastepnik()
    {
        if (this.naSkrzyzowaniu)
        {
            int myplatformID = this.platforma.ID;
            Platforma kolejnaPlatforma = Piramida.singleton.platformy[myplatformID - 1];
            if (kolejnaPlatforma.rotacja % 2 != nrSkrzyzowania % 2) return nastepnik;
            else
            {
                if (kolejnaPlatforma.rotacja == this.nrSkrzyzowania) return kolejnaPlatforma.schody1;
                else return kolejnaPlatforma.schody2;
            }

        }
        else return this.nastepnik;

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
