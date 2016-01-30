using UnityEngine;
using System.Collections;

public class Pole : MonoBehaviour {

    public Platforma platforma;
	public bool onPyramid = true;

    public Pole nastepnik;
    public bool naSkrzyzowaniu;
    public int nrSkrzyzowania;
    public bool poleNaWieze;
    public bool isSchody;

    public Pole GetNastepnik()
    {
		if(onPyramid){
	        if (this.poleNaWieze) return null;
	        else if (this.naSkrzyzowaniu)//platformy bez shodow i nie zerowa
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
			else if (this.platforma.ID % 2 == 1 || this.platforma.ID == 0) return this.nastepnik; //platformy bez schodow
	        else
	        {
	            int nastepnePoleCentralneID = 0;
	            //jesli jestem schodkiem 1
	            if (this == this.platforma.schody1)
	                nastepnePoleCentralneID = this.platforma.rotacja;
	            //jesli jestem schodkiem 2
	            else if (this == this.platforma.schody2)
	                nastepnePoleCentralneID = (this.platforma.rotacja + 2) % 4;

	            return Piramida.singleton.platformy[this.platforma.ID - 1].polaCentralneScian[nastepnePoleCentralneID];
	        }
		}
		else return nastepnik;

    }


	// Use this for initialization
	void Start () {
        this.platforma = transform.parent.GetComponent<Platforma>();
        Debug.Assert(this.platforma != null);
	}
	
	// Update is called once per frame
	void Update () {
        Pole localNastepnik = GetNastepnik();
        if (localNastepnik)
        {
            Debug.DrawLine(
                transform.position,
                localNastepnik.transform.position,
                Color.blue,
                0f,
                false);
        }
	}



}
