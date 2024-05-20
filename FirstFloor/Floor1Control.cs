using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Floor1Control : MonoBehaviour
{   
    public GameObject[] objectsToDisable;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;

    public GameObject dead;
    public GameObject camera;
    public GameObject nextlevel;

    public TextMeshProUGUI SO;
    public TextMeshProUGUI GRAFIKA;
    public TextMeshProUGUI ALGEBRA;
    public TextMeshProUGUI LOGIKA;

    public GameObject task1;
    public GameObject task2;
    public GameObject oceny;

    public AudioSource myFx;
    public AudioClip passedFx;
    
    static bool isLevelCleared = false;

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
                if(SO.text == "3" && GRAFIKA.text == "5" && ALGEBRA.text == "3" && LOGIKA.text == "3"){
                    level = 1;
                    unlocknextlevel();
                }
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
