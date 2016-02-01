using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

    public TowerFieldsHighlight towerFieldsHighlight;
    public Pyramid pyramid;

    public LinkedList<Enemy> enemies = new LinkedList<Enemy>();

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        if( Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        towerFieldsHighlight.GenerateTowers();

	}


    

}
