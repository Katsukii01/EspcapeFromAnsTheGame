using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor2Control : MonoBehaviour
{   public GameObject l1;
    public GameObject l2;
    public GameObject l3;

    public GameObject[] objectsToDisable;
    public GameObject dead;
    public GameObject camera;
    public GameObject cookinggame;
    public GameObject dziekanatgame;
    public GameObject nextlevel;

    public GameObject Eq;
    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    public GameObject task4;
    public AudioSource myFx;
    public AudioClip passedFx;
    int level = 0;
    static bool isLevelCleared = false;
    bool alive = true;

    bool skladniki = false;
    bool ciasto = false;
    static public bool dziekanat = false;

    void Start(){
        dziekanat = false;
    }
    // Update is called once per frame
    void disable(){
       camera.SetActive(true);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        Ekwipunek ekwipunek = Eq.GetComponent<Ekwipunek>();

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
            skladniki = ekwipunek.SprawdzPrzedmiotyWekwipunku(new string[] { "salmon", "coffee", "salami", "cookie", "pizza" });
            if(skladniki){
                level = 1;
                unlockgotowanie();
            }
        }

        if(level==1){
            ciasto = ekwipunek.SprawdzPrzedmiotyWekwipunku(new string[] { "cake" });
            if (skladniki && ciasto){
                level=2;
                unlockdziekanat();
            }  
        }

        if(level==2){
            if(dziekanat){
                level=3;
                isLevelCleared=true;
                unlocknextlevel();
            }
        }      
    }

    void unlockdziekanat(){
            task2.SetActive(false);
            task3.SetActive(true);
            cookinggame.SetActive(false);
            dziekanatgame.SetActive(true);
            myFx.PlayOneShot(passedFx);
    }

    void unlocknextlevel(){
            task3.SetActive(false);
            task4.SetActive(true);
            dziekanatgame.SetActive(false);
            nextlevel.SetActive(true);
            myFx.PlayOneShot(passedFx);
    }

    void unlockgotowanie(){
            task1.SetActive(false);
            task2.SetActive(true);
            cookinggame.SetActive(true);
            myFx.PlayOneShot(passedFx);
    }
}
