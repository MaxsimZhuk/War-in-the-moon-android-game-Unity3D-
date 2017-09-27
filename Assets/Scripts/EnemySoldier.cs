using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
       /* RaycastHit info;
        Quaternion rotatenionX = Quaternion.AngleAxis(0, Vector3.down);

        if (Physics.Raycast(transform.position, transform.forward, out info, 10000f))
        {
            if (info.collider.gameObject.CompareTag("Team1"))
            {
                
                Debug.Log(info.collider.gameObject);
            }
        }*/
    }
}
