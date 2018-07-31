using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public Vector3[] verts;
    public GameObject[] gravebeds;

	// Use this for initialization
	void Start ()
    {
        verts = GetComponent<MeshFilter>().mesh.vertices;
        gravebeds = Resources.LoadAll<GameObject>("Gravebeds");

        //int randomvert = Random.Range(0, verts.Length);
        //int randombed = Random.Range(0, gravebeds.Length);
        //GameObject bed = gravebeds[randombed];
        //Instantiate(bed, verts[randomvert], bed.transform.rotation);

        foreach(Vector3 vert in verts)
        {
            int chance = Random.Range(0, 100); //Get chance to spawn a bed
            int randombed = Random.Range(0, gravebeds.Length); // choose a random bed from all beds
            GameObject bed = gravebeds[randombed]; // assign random bed as the gameobject
            float SpawnPosX = (vert.x * transform.localScale.x) + transform.position.x; // Get the x location of the spawn point
            float SpawnPosZ = (vert.z * transform.localScale.z) + transform.position.z; // get the z location of the spawn point
            Vector3 SpawnPos = new Vector3(SpawnPosX, vert.y, SpawnPosZ); // assign the spawn point as a vector3
            if (chance < 30) // spawn bed if chance is good
            {
                Instantiate(bed, SpawnPos, bed.transform.rotation);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
