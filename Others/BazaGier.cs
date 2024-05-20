using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazaGier : MonoBehaviour
{
     public GameObject game;
     public GameObject[] objectsToDisable;
     public GameObject l1;
     public GameObject l2;
     public GameObject l3;
     public GameObject zabierz;
     public GameObject camera;
     public GameObject zacznij;
     public GameObject ja;
     public GameObject Eq;
     public int gra=0;
     bool isPlayerInside = false;
    public AudioSource myFx;
    public AudioClip lostFx;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           zacznij.SetActive(true);
            isPlayerInside = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            zacznij.SetActive(false);
            isPlayerInside = false;
        }
    }

    void Update()
        {
            if(gra == 1 ){
                    Win();
            }else{
                if(isPlayerInside && Input.GetKeyDown(KeyCode.E)){
                    startgame();
                }
            }
        }

    void startgame(){
       game.SetActive(true);
       camera.SetActive(true);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
        play();
    }

    void play(){
           gra=1;
    }

    void wyjdz(){
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
         camera.SetActive(false);
         game.SetActive(false);
    }

        void zycieZabierz(){
        if(_CharacterPick.postac==1){
            Transform ostatnieDziecko = l1.transform.GetChild(l1.transform.childCount - 1);
                    Destroy(ostatnieDziecko.gameObject);
        }else if(_CharacterPick.postac==2){
            Transform ostatnieDziecko = l2.transform.GetChild(l2.transform.childCount - 1);
                    Destroy(ostatnieDziecko.gameObject);
        }else if(_CharacterPick.postac==3){
            Transform ostatnieDziecko = l3.transform.GetChild(l3.transform.childCount - 1);
                    Destroy(ostatnieDziecko.gameObject);
        }
    }
    
    void Lose(){
        gra=0;
        zycieZabierz();
        wyjdz();
        myFx.PlayOneShot(lostFx);
        zabierz.SetActive(true);
        StartCoroutine(WylaczPoCzasie(zabierz, 2f));
    }

    IEnumerator WylaczPoCzasie(GameObject obj, float czas)
{
    yield return new WaitForSeconds(czas);
    obj.SetActive(false);
} 

    void Win(){
        Ekwipunek ekwipunek = Eq.GetComponent<Ekwipunek>();
        ekwipunek.ZniszczPrzedmiotyWekwipunku(new string[] { "cake"});
        gra=0;
        wyjdz();
        zacznij.SetActive(false);
        ja.SetActive(false);
        Floor2Control.dziekanat = true;
    }
}
