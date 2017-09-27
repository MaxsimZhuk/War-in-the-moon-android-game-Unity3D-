using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GUISkin MoneySkin;
    public GameObject Target;
    public GameObject Soldier;
    private GameObject Army2;
    private List<GameObject> Army = new List<GameObject>();
    private List<int> electArmy =  new List<int>();
    public GameObject myTarget;
    public GameObject MyMainCamera;
    private float xDeg;
    private float yDeg;
    private float lerpSpeed;
    private int timer;
    private int Resoursetimer;
    private int Gas;
    private int Water;
    private int Iron;


    // Use this for initialization
    void Start () {
        lerpSpeed = (float) 0.1;
        Gas = 50;
        Water = 50;
        Iron = 50;
        Resoursetimer = 0;
    }

    void FixedUpdate()
    {
        Resoursetimer++;
        if (Resoursetimer > 100)
        {
            Gas++;
            Water++;
            Iron++;
            Resoursetimer = 0;
        }
    }
    // Update is called once per frame
    void Update () {


       // GUI.TextField(new Rect(0, 200, 0, 200), @"\" + pos.x + "| " + pos.y);
	    if (Input.touchSupported)
	    {


	        if (Input.touchCount == 2)
	        {
	            Vector2 finger1 = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
	            Vector2 finger2 = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);

	            float finger1X = 0;
	            float finger2X = 0;
	            float finger1Y = 0;
	            float finger2Y = 0;

	            if (finger1.x < finger2.x)
	            {
	                finger1X = finger1.x;
	                finger2X = finger2.x;
	            }
	            if (finger1.x > finger2.x)
	            {
	                finger1X = finger2.x;
	                finger2X = finger1.x;
	            }
	            if (finger1.y < finger2.y)
	            {
	                finger1Y = finger1.y;
	                finger2Y = finger2.y;
	            }
	            if (finger1.y > finger2.y)
	            {
	                finger1Y = finger2.y;
	                finger2Y = finger1.y;
	            }
	            if (Army != null)
	            {
	                electArmy.Clear();

	                for (int i = 0; i < Army.Count; i++)
	                {
                        Army[i].BroadcastMessage("MyUnActive");
                        if (finger1X < Army[i].transform.position.x && Army[i].transform.position.x < finger2X)
	                    {
	                        if (finger1Y < Army[i].transform.position.y && Army[i].transform.position.y < finger2Y)
	                        {
	                            electArmy.Add(i);
                                Army[i].BroadcastMessage("MyActive");
                            }
	                    }
	                }
	            }
                timer =20;
	        }
	        
	            if (timer < 1)
	            {
	                if (Input.GetTouch(0).tapCount > 1)
	                {

	                    SoldierGo();
	                }
	            }
	            else
	            {
	                timer--;
	            }        
	    }


	    if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            SoldierGo();
        }


    }
    void SoldierGo()
    {
        if (Army != null)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Target.transform.position = pos;
            myTarget.transform.position = Target.transform.position;
            for (int i = 0; i < electArmy.Count; i++)
            {
                
                Army[electArmy[i]].BroadcastMessage("getTarget", myTarget);
                if ((i+1)%10 == 0)
                {
                    myTarget.transform.position = Target.transform.position;
                    myTarget.transform.position -= transform.up * (float)0.07*i;
                }
                else
                {
                    myTarget.transform.position += transform.right * (float)0.5;
                }
                
            }
        }
    }
    public void OnGUI()
    {
        
        if (GUI.Button(new Rect(0, Screen.height - 100, 300, 100), "Create"))
        {
            if (Iron > 10 && Water > 10 && Gas > 10)
            {
                Iron = Iron - 10;
                Water = Water - 10;
                Gas = Gas - 10;
                Army.Add(Instantiate(Soldier, transform.position, transform.rotation));
            }
        }
        if (GUI.RepeatButton(new Rect(Screen.width/2 - 200, 0, 400, 50), "UP"))
        {
            if (MyMainCamera.transform.position.y < 80) MyMainCamera.transform.position = MyMainCamera.transform.position + MyMainCamera.transform.up * lerpSpeed;
        }
        if (GUI.RepeatButton(new Rect(Screen.width/2 - 200, Screen.height - 50, 400, 50), "DOWN"))
        {
           if( MyMainCamera.transform.position.y> 0) MyMainCamera.transform.position = MyMainCamera.transform.position - MyMainCamera.transform.up * lerpSpeed;
        }
        if (GUI.RepeatButton(new Rect(0, Screen.height/2 - 200, 50,  400), "LEFT"))
        {
            if (MyMainCamera.transform.position.x > 0) MyMainCamera.transform.position = MyMainCamera.transform.position - MyMainCamera.transform.right * lerpSpeed;
        }
        if (GUI.RepeatButton(new Rect(Screen.width - 50, Screen.height / 2 - 200, 50,  400), "RIGHT"))
        {
            if (MyMainCamera.transform.position.x < 70) MyMainCamera.transform.position = MyMainCamera.transform.position + MyMainCamera.transform.right * lerpSpeed;
        }
        GUI.skin = MoneySkin;
        GUI.Label(new Rect(0, 0, 700, 70), "Gas= "+Gas);
        GUI.Label(new Rect(0, 70, 700, 70), "Water= " + Water);
        GUI.Label(new Rect(0, 140, 700, 70), "Iron= " + Iron);
    }
}
