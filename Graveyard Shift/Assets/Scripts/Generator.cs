using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Generator : MonoBehaviour {

    public Renderer plot;

    //public Vector3[] verts;
    public GameObject[] gravebeds;
    public GameObject[] crypts;

    public List<Vector3> verts;

    public float spaceX;
    public float spaceZ;

    public int spawnChance;

    private int gridX;
    private int gridZ;

    private float sizeX;
    private float sizeZ;

    private float oldX;
    private float oldZ;

    private Vector3 corner;

    private Vector3 currentloc;

    private int madeVerts;
    private int t = 0;
    private int vertCount;

    private int totalpoints;

    private int chosenvert;
    

    // Use this for initialization
    void Start ()
    {
        plot = GetComponent<Renderer>();

        gravebeds = Resources.LoadAll<GameObject>("Gravebeds");
        crypts = Resources.LoadAll<GameObject>("Crypts");

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

        gridX = Mathf.FloorToInt(transform.localScale.x / spaceX); // get the amount of graves that can spawn along X and Z
        gridZ = Mathf.FloorToInt(transform.localScale.z / spaceZ);

        totalpoints = gridX * gridZ; // got total amount of spawn points

        //verts = new Vector3[totalpoints]; // set array to size of total points

        currentloc.x = plot.bounds.min.x + ((sizeX / gridX) / 2); // set the first spawn point adjusted to give a border
        currentloc.z = plot.bounds.min.z + ((sizeZ / gridZ) / 2);

        while (madeVerts < totalpoints)//verts.Length)
        {
            for (int t = 0; t < gridX; t++)
            {
                //verts[t + vertCount] = currentloc;
                verts.Add(currentloc);
                currentloc.x = currentloc.x + (sizeX / gridX);
                madeVerts++;
            }

            currentloc.z = currentloc.z + (sizeZ / gridZ) ;
            currentloc.x = plot.bounds.min.x + ((sizeX / gridX) / 2);
            vertCount += gridX;
            t = 0;
        }

        PlaceCrypts();
    }

    void PlaceCrypts()
    {
        if(totalpoints > 6)
        {
            chosenvert = Random.Range(0, verts.Count);
            if ((chosenvert + 1) % gridX == 0)
            {
                chosenvert--;
            }
            float cryptspawnX = verts[chosenvert].x + (verts[chosenvert + 1].x - verts[chosenvert].x) / 2;
            Vector3 cryptspawn = new Vector3(cryptspawnX, verts[chosenvert].y, verts[chosenvert].z);
            Quaternion SpawnRot = Quaternion.Euler(0, 0, 0);
            GameObject choseCrypt = Instantiate(crypts[Random.Range(0, crypts.Length)], cryptspawn, SpawnRot);

            verts.Remove(verts[chosenvert]);
            verts.Remove(verts[chosenvert]);

            PlaceGraves();
        }
        else
        {
            PlaceGraves();
        }
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
            Quaternion SpawnRot = Quaternion.Euler(0, rot, 0);

            if (chance < spawnChance) // spawn bed if chance is good
            {
                GameObject gb = Instantiate(bed, SpawnPos, SpawnRot);
                gb.transform.parent = this.gameObject.transform;
            }
        }
    }
}
