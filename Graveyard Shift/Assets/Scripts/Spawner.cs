using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector3 AreaMax;
    public Vector3 AreaMin;
    private Vector3 RandomPos;

    public GameObject SpawnObject;
    public GameObject SpawnParent;
    public int SpawnNumber = 20;
    public int SpawnCount = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        RandomPos = new Vector3(Random.Range(AreaMin.x, AreaMax.x), Random.Range(AreaMin.y, AreaMax.y), Random.Range(AreaMin.z, AreaMax.z));

		if (SpawnCount < SpawnNumber)
        {
            SpawnCount += 1;
            GameObject Spawn;
            Spawn = Instantiate(SpawnObject, RandomPos, Quaternion.identity);
            Spawn.transform.parent = SpawnParent.transform;
        }
	}
}
