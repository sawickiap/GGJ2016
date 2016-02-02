using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

	public void SetEclipse( float eclipsePercent ) {
			
		this.eclipsePercent = eclipsePercent;
	}

	public int level = 0;

	public Text progressText;
	public Text levelText;
	public Scrollbar scrollbar;

	public void increaseEclipse() {
		
		this.eclipsePercent += 0.1f;
		if(eclipsePercent>=1.0f){
			eclipsePercent = 1.0f;
			progressText.text = "You Lost";
		}
		else{
			progressText.text = "Eclipse: " + eclipsePercent + "%";
		}

		scrollbar.value = eclipsePercent;
	}

	public void decreaseEclipse() {
		
		this.eclipsePercent -= 0.1f;
		if(eclipsePercent<=0.0f){
			eclipsePercent = 0.0f;
			progressText.text = "Ritual succeeded";
		}
		else{
			progressText.text = "Eclipse: " + eclipsePercent + "%";
		}

		scrollbar.value = eclipsePercent;
	}

	float eclipsePercent = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
