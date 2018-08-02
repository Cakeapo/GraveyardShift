using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeadstoneSpawn : MonoBehaviour {

    public Object[] headstones;
    private Transform headstoneSpawn;
    private GameObject hs;

	// Use this for initialization
	void Start ()
    {
        headstoneSpawn = transform.GetChild(0).transform;

        headstones = Resources.LoadAll("HeadStones", typeof(GameObject));

        hs = Instantiate(headstones[Random.Range(0, headstones.Length)], headstoneSpawn.position, headstoneSpawn.rotation) as GameObject;

        hs.transform.parent = this.gameObject.transform;
        
	}
	
}
