using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

[Serializable]
public class Outfitter : MonoBehaviour 
{
	
	CharacterDemoController ac;
	int oldWeaponIndex;
	[SerializeField]
	public List<WeaponSlot> weapons;
	
	// Use this for initialization
	void Start () 
	{
		ac = GetComponentInChildren<CharacterDemoController>();

        int weaponStateInt = (int)ac.characterWeaponState;

		for(int i = 0;i<weapons.Count;i++)
		{
			for(int model=0;model<weapons[i].models.Count;model++)
			{
				weapons[i].models[model].enabled = false;
			}
		}
        for (int model = 0; model < weapons[weaponStateInt].models.Count; model++)
		{
            weapons[weaponStateInt].models[model].enabled = true;
		}
        oldWeaponIndex = weaponStateInt;
	}
	
	// Update is called once per frame
	void Update () 
	{

        int weaponStateInt = (int)ac.characterWeaponState;

        if (weaponStateInt != oldWeaponIndex)
		{
			for(int model=0;model<weapons[oldWeaponIndex].models.Count;model++)
			{
				weapons[oldWeaponIndex].models[model].enabled = false;
			}
            for (int model = 0; model < weapons[weaponStateInt].models.Count; model++)
			{
                weapons[weaponStateInt].models[model].enabled = true;
			}
            oldWeaponIndex = weaponStateInt;
		}
	}
}
[Serializable]
public class WeaponSlot
{
	[SerializeField]
	public List<Renderer> models = new List<Renderer>();
}
