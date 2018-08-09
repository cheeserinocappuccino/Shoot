using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavTest : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent nav;
    Transform target;
	// Use this for initialization
	void Start () {
       nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(target.position);
	}
}
