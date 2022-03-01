using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRules : MonoBehaviour
{
    bool showing = false;
    public GameObject rules;
    public GameObject Game;
    GameObject rulesImage;

    public void showRules(){
        if (showing){
            Destroy(rulesImage);
            showing = false;
        }else if(!showing){
            rulesImage = Instantiate(rules, new Vector3(0, 0, 0), Quaternion.identity);
            rulesImage.transform.SetParent(Game.transform, false);
            showing = true;
        }
    }
}
