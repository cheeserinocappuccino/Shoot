using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour,IWeapons {
    public Sprite icon;
    public Sprite Icon { get { return icon; } set { icon = value; } }

    public GameObject ObjOfrainbowLine;
    private LineRenderer rainbowLine;
    private Light gunlight;
    private AudioSource audiosource;
    public AudioClip nyanMusic;
    private bool dontDoTwice = false;
    private LayerMask hitAble;
    // Use this for initialization
    void Start () {
        rainbowLine = ObjOfrainbowLine.GetComponent<LineRenderer>();
        gunlight = GetComponent<Light>();
        hitAble = LayerMask.GetMask("Shootable");
        audiosource = GetComponent<AudioSource>();
        //audiosource.clip = nyanMusic;
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Fire1"))
        {

            
            gunlight.enabled = true;
            rainbowLine.enabled = true;
            rainbowLine.SetPosition(0, transform.position);
            rainbowLine.SetPosition(1, transform.forward * 100);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, hitAble))
            {
                EnemyHealth enemyhp = null;
                try
                {
                    enemyhp = hit.rigidbody.gameObject.GetComponent<EnemyHealth>();
                }
                catch { }
                rainbowLine.SetPosition(1, hit.point);
                if (enemyhp != null)
                {
                    enemyhp.TakeDamage(1,hit.point);
                  
                }

            }
            else
            {
                rainbowLine.SetPosition(1, transform.forward * 100f);

            }

        }
        else {
            DisableEffects();
        }


    }


    public void DisableEffects()
    {
        gunlight.enabled = false;
        rainbowLine.enabled = false;

    }
}
