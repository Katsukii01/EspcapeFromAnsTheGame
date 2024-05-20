using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DziekanatGame : MonoBehaviour
{
     public GameObject game;
     public GameObject[] objectsToDisable;
     public GameObject[] dailogi;
     public GameObject[] odpowiedzi;
     public GameObject affect;
     public int activeDialog=1;
     public int reputation=3;
     public int powiedziane=0;

    public AudioClip angryFx;
    public AudioClip blushFx;

     public GameObject l1;
     public GameObject l2;
     public GameObject l3;
     public GameObject zabierz;
     public GameObject camera;
     public GameObject zacznij;
     public GameObject ja;
     public GameObject Eq;
     public Animator animator;
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

public void changerep(){
    RectTransform affectRectTransform = affect.GetComponent<RectTransform>();
    float newScaleY = 0.1f * reputation; // Załóżmy, że reputation to zmienna przechowująca reputację
    Vector3 currentScale = affectRectTransform.localScale;
    affectRectTransform.localScale = new Vector3(currentScale.x, newScaleY, currentScale.z);
}


    public void answear(int nr){
        if(nr==1){
            reputation++;
            myFx.PlayOneShot(blushFx);
        }else if(nr==2){
            reputation--;
            myFx.PlayOneShot(angryFx);
        }
        changerep();
        odpowiedzi[activeDialog].SetActive(false);
        activeDialog++;
    }

    void talk(){
        animator.SetBool("talk", true);
        animator.SetBool("stand", false);
    }
    void stoptalk(){
        animator.SetBool("talk", false);
        animator.SetBool("stand", true);
    }
    IEnumerator DelayedStopTalk(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        stoptalk(); // Wywołujemy funkcję stoptalk() po opóźnieniu
         odpowiedzi[activeDialog].SetActive(true);
    }

    void Update()
        {
            if(reputation<0){
                Lose();
            }

            if(gra == 1 ){
                 Cursor.visible = true;
                if(activeDialog<dailogi.Length){
                    if(powiedziane<activeDialog){
                        dailogi[activeDialog-1].SetActive(false);
                        dailogi[activeDialog].SetActive(true);
                        talk();
                        switch(activeDialog){
                            case 1:
                                StartCoroutine(DelayedStopTalk(14));
                                break;
                            case 2:
                                StartCoroutine(DelayedStopTalk(5));
                                 break;
                            case 3:
                                StartCoroutine(DelayedStopTalk(6));
                                break;
                            case 4:
                                StartCoroutine(DelayedStopTalk(6));
                                break;
                            case 5:
                                StartCoroutine(DelayedStopTalk(6));
                                break;
                            case 6:
                                StartCoroutine(DelayedStopTalk(6));
                                break;
                            case 7:
                                StartCoroutine(DelayedStopTalk(6));
                                break;    
                        }
                        powiedziane++;
                    }
                }else{
                    Win();
                }
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
           changerep();
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
        Cursor.visible = false;
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
         Cursor.visible = false;
        Ekwipunek ekwipunek = Eq.GetComponent<Ekwipunek>();
        ekwipunek.ZniszczPrzedmiotyWekwipunku(new string[] { "cake"});
        gra=0;
        wyjdz();
        zacznij.SetActive(false);
        ja.SetActive(false);
        Floor2Control.dziekanat = true;
    }
}
