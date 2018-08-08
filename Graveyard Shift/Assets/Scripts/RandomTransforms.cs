using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransforms : MonoBehaviour
{
    //public bool RandomPosition = false;

    public float SpinAmount;
    public float SpinAngle;

    public bool RandomSpinX = false;
    public bool RandomSpinY = false;
    public bool RandomSpinZ = false;

    public bool RandomFlipX = false;
    public bool RandomFlipY = false;
    public bool RandomFlipZ = false;

    // Use this for initialization
    void Start()
    {
        if (RandomSpinX)
        {
            float angle = Random.Range(0, 360);

            if (angle > SpinAmount)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 2)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 3)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 4)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 5)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 6)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 7)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 8)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + SpinAmount, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }
        if (RandomSpinY)
        {
            float angle = Random.Range(0, 360);
            /*
            if (angle > 270)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 270, transform.localEulerAngles.z);
            }
            else if (angle > 180)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
            }
            else if (angle > 90)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 90, transform.localEulerAngles.z);
            }
            */
            if (angle > SpinAmount)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 2)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 3)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 4)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 5)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 6)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 7)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
            if (angle > SpinAmount * 8)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + SpinAmount, transform.localEulerAngles.z);
            }
        }
        if (RandomSpinZ)
        {
            float angle = Random.Range(0, 360);

            /*
            if (angle > 270)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 270);
            }
            else if (angle > 180)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 180);
            }
            else if (angle > 90)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 90);
            }
            */
            if (angle > SpinAmount)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
            if (angle > SpinAmount * 2)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
            if (angle > SpinAmount * 3)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
            if (angle > SpinAmount * 4)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
            if (angle > SpinAmount * 5)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
            if (angle > SpinAmount * 6)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
            if (angle > SpinAmount * 7)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
            if (angle > SpinAmount * 8)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + SpinAmount);
            }
        }

        if (RandomFlipX)
        {
            if (Random.Range(0.0f, 1.0f) >= 0.5f)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        if (RandomFlipY)
        {
            if (Random.Range(0.0f, 1.0f) >= 0.5f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            }
        }
        if (RandomFlipZ)
        {
            if (Random.Range(0.0f, 1.0f) >= 0.5f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
