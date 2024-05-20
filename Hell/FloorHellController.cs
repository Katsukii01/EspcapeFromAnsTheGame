using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FloorHellController : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public static int ending=0;
    public GameObject dead;
    public GameObject camera;
    public GameObject boss;

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

    if(level == 0){
        if(BossController.currentHealth <= 0){
            level = 1;
        }         
    }

    if(level == 1){
        level=2;
        myFx.PlayOneShot(passedFx);
        _Menu.level = _Menu.level + 1;
        ending = 1;
        Invoke("LoadNextScene", 5f);
    }
}  

    void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(_Menu.level); 
    }  
}
