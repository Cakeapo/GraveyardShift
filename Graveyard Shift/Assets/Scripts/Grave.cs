using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public enum Owner { Empty, Player1, Player2, Player3 };
    public Owner OwnerSel;

    public bool Claimed = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GM.instance.Graves.Contains(this.gameObject) == false && Claimed == false)
        {
            GM.instance.Graves.Add(this.gameObject);
        }

        if (OwnerSel == Owner.Player1)
        {
            if (GM.instance.Grave1 == this.gameObject)
            {

            }
            else
            {
                OwnerSel = Owner.Empty;
            }
        }
        
        if (OwnerSel == Owner.Player2)
        {
            if (GM.instance.Grave2 == this.gameObject)
            {

            }
            else
            {
                OwnerSel = Owner.Empty;
            }
        }

        if (OwnerSel == Owner.Player3)
        {
            if (GM.instance.Grave3 == this.gameObject)
            {

            }
            else
            {
                OwnerSel = Owner.Empty;
            }
        }
    }
}
