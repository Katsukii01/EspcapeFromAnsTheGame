using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BasementController : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;

    public GameObject dead;
    public GameObject camera;
    public GameObject nextlevel;

    public TextMeshProUGUI formalne;
    public TextMeshProUGUI statystyka;
    public TextMeshProUGUI skryptowe;

    public GameObject task1;
    public GameObject task2;

    public GameObject oceny;

    public AudioSource myFx;
    public AudioClip passedFx;
    
    int level=0;

    void disable(){
       camera.SetActive(true);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
         if(_CharacterPick.postac==1){
                   if(l1.transform.childCount==0){
                        dead.SetActive(true);
                        disable();
                        Cursor.visible = true;
                   }
         }else if(_CharacterPick.postac==2){
                   if(l2.transform.childCount==0){
                        dead.SetActive(true);
                         disable();
                         Cursor.visible = true;
                   }
         }else if(_CharacterPick.postac==3){
                    if(l3.transform.childCount==0){
                        dead.SetActive(true);
                         disable();
                         Cursor.visible = true;
                   }
         }

        if(level==0){
                if(formalne.text == "3" && statystyka.text == "3" && skryptowe.text == "3"){
                    level = 1;
                }         
        }

        if(level==1){
                level = 2;
                unlocknextlevel();
            }   
    }

    void unlocknextlevel(){
            task1.SetActive(false);
            oceny.SetActive(false);
            task2.SetActive(true);
            nextlevel.SetActive(true);
            myFx.PlayOneShot(passedFx);
    }
}
