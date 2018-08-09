using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour,IWeapons {
    public Sprite icon;
    public Sprite Icon { get { return icon; } set { icon = value; } }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void DisableEffects()
    {
        Debug.Log("Laser.DisableEffects");
    }
}
