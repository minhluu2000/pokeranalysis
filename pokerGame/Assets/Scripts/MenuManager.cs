using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;

    void Awake(){
        Debug.Log("Awake");
        Instance = this;
    }

    public void OpenMenu(string menuName){
        Debug.Log("Open String: " + menuName);
        for (int i = 0; i < menus.Length; i++){
            if (menus[i].menuName == menuName){
                menus[i].Open();
            }else if(menus[i].open){
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu){
        Debug.Log("Open Menu");
        for (int i = 0; i < menus.Length; i++){
            if(menus[i].open){
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu){
        Debug.Log("Close");
        menu.Close();
    }
}
