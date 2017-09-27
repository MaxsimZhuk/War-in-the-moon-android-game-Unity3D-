using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialIntelligence : MonoBehaviour
{

    public GameObject Soldier;
    private List<GameObject> Army = new List<GameObject>();
    private int _timer;
    public List<GameObject> Checkpoints;
    // Use this for initialization
    void Start ()
    {
        _timer = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timer++;

        if (_timer > 1000)
        {
            _timer = 0;
            GameObject NewSoldier;
            NewSoldier = Instantiate(Soldier, transform.position, transform.rotation);
            NewSoldier.BroadcastMessage("getTarget", Checkpoints[Random.Range(0, 8)]);
            NewSoldier.BroadcastMessage("MyActive");
            Army.Add(NewSoldier);
        }
    }
}
