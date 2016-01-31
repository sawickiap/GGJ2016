using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class GameController : MonoBehaviour
{

    public TowerFieldsHighlight towerFieldsHighlight;
    public Piramida pyramid;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        towerFieldsHighlight.GenerateTowers();

	}


    

}
