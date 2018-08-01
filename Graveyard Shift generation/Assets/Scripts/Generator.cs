using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public Renderer plot;

    public Vector3[] verts;
    public GameObject[] gravebeds;

    private int gridX;
    private int gridZ;

    public float sizeX;
    public float sizeZ;

    private float oldX;
    private float oldZ;

    private Vector3 corner;

    private Vector3 currentloc;

    public int madeVerts;
    private int t = 0;
    private int vertCount;
    

    // Use this for initialization
    void Start ()
    {
        plot = GetComponent<Renderer>();

        gravebeds = Resources.LoadAll<GameObject>("Gravebeds");

        BuildVerts();
	}
	
	// Update is called once per frame
	void Update ()
    {


    }

    void BuildVerts()
    {
        corner = plot.bounds.min; // get bottom left corner of plot
        currentloc = corner; // set currentloc to the corner

        sizeX = plot.bounds.max.x - plot.bounds.min.x; // get the size of the plot
        sizeZ = plot.bounds.max.z - plot.bounds.min.z;

        gridX = Mathf.FloorToInt(transform.localScale.x / 0.5f); // get the amount of graves that can spawn along X and Z
        gridZ = Mathf.FloorToInt(transform.localScale.z / 1.0f);

        int totalpoints = gridX * gridZ; // got total amount of spawn points

        verts = new Vector3[totalpoints]; // set array to size of total points

        currentloc.x = plot.bounds.min.x + ((sizeX / gridX) / 2); // set the first spawn point adjusted to give a border
        currentloc.z = plot.bounds.min.z + ((sizeZ / gridZ) / 2);

        while (madeVerts < verts.Length)
        {
            for (int t = 0; t < gridX; t++)
            {
                verts[t + vertCount] = currentloc;
                currentloc.x = currentloc.x + (sizeX / gridX);
                madeVerts++;
            }

            currentloc.z = currentloc.z + (sizeZ / gridZ) ;
            currentloc.x = plot.bounds.min.x + ((sizeX / gridX) / 2);
            vertCount += gridX;
            t = 0;
        }

        PlaceGraves();
    }

    void PlaceGraves()
    {
        foreach (Vector3 vert in verts)
        {
            int chance = Random.Range(0, 100); //Get chance to spawn a bed
            int randombed = Random.Range(0, gravebeds.Length); // choose a random bed from all beds
            GameObject bed = gravebeds[randombed]; // assign random bed as the gameobject
            float SpawnPosX = vert.x;// * transform.localScale.x) + transform.position.x; // Get the x location of the spawn point
            float SpawnPosZ = vert.z;// * transform.localScale.z) + transform.position.z; // get the z location of the spawn point
            Vector3 SpawnPos = new Vector3(SpawnPosX, vert.y, SpawnPosZ); // assign the spawn point as a vector3

            float rot = Random.Range(-6, 6);
            Quaternion SpawnRot = Quaternion.Euler(-90, 0, rot);

            if (chance < 100) // spawn bed if chance is good
            {
                GameObject gb = Instantiate(bed, SpawnPos, SpawnRot);
                gb.transform.parent = this.gameObject.transform;
            }
        }
    }
}
