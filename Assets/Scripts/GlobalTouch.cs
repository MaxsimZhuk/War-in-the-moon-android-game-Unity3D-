using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTouch : MonoBehaviour {
   
    public Transform clone;
    
    public Texture2D ButtonMenu;
    // Use this for initialization


    void  Update()
    {

        //clone.position = Input.mousePosition;
       // Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       // clone.position = pos;
        



    }
    public void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 100, 300, 100), ButtonMenu))
        {
            Application.Quit();
        }
        //GUI.DrawTexture(new Rect(Screen.width  - 400, Screen.height   - 100, 400, 100), ButtonMenu);
    }
}
