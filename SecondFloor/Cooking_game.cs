using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking_game : MonoBehaviour
{
     public GameObject game;
     public GameObject[] objectsToDisable;
     public GameObject l1;
     public GameObject l2;
     public GameObject l3;
     public GameObject ciasto;
     public GameObject zabierz;
     public GameObject camera;
     public GameObject zacznij;
     public GameObject ja;
     public GameObject Eq;
     public int gra=0;
     public int zniszczone=0;
     bool isPlayerInside = false;
    public AudioSource myFx;
    public AudioClip lostFx;

    public GameObject[] imageObjects1; // Tablica obiektów zawierających RawImage
    public GameObject[] imageObjects2;
    public GameObject[] imageObjects3;
    public GameObject[] imageObjects;
    public float minActiveTime; // Minimalny czas wyświetlania obrazu aktywnego
    public float maxActiveTime;  // Maksymalny czas wyświetlania obrazu aktywnego
    public int activeImageIndex = -1;


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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (activeImageIndex !=-1 && imageObjects[activeImageIndex].activeSelf)
            {
                // Usuwanie aktywnego obrazu
                imageObjects[activeImageIndex].SetActive(false);
                zniszczone++;
                activeImageIndex = -1;
            }
            else
            {
              gra=0;
              Lose();
            }
        }

        // Jeśli wszystkie obiekty zostały zniszczone, wygrana
        if (zniszczone == imageObjects.Length)
        {
            gra=0;
            Win();
        }
    }else{
        if(isPlayerInside && Input.GetKeyDown(KeyCode.E)){
            startgame();
        }
    }
}

void play()
{
    if(_CharacterPick.postac==1){
        imageObjects = imageObjects2;
    }else if(_CharacterPick.postac==2){
        imageObjects = imageObjects1;
    }else if(_CharacterPick.postac==3){
          imageObjects = imageObjects3;
    }
    foreach (GameObject obj in imageObjects)
        {
            obj.SetActive(false);
        }
    zniszczone=0;
    gra = 1; 
    StartCoroutine(ActivateRandomImage());
}

IEnumerator ActivateRandomImage()
{ 
     if(gra == 1 ){
         int i=0;
        foreach (var imageObj in imageObjects)
        {   
            if (imageObj != null)
            {
                yield return new WaitForSeconds(Random.Range(minActiveTime, maxActiveTime));
                activeImageIndex=i;
                imageObj.SetActive(true);
                yield return new WaitForSeconds(maxActiveTime);
                     // Poczekaj jeszcze trochę
                    if (imageObj.activeSelf)
                    {
                    if(gra == 1 ){
                        Lose();
                        } 
                        yield break;
                    }
                // Ukryj obraz
                
                imageObj.SetActive(false);
            }
            i++;
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
        foreach (GameObject obj in imageObjects)
        {
            obj.SetActive(false);
        }
    }

    IEnumerator WylaczPoCzasie(GameObject obj, float czas)
{
    yield return new WaitForSeconds(czas);
    obj.SetActive(false);
} 

    void Win(){
        Ekwipunek ekwipunek = Eq.GetComponent<Ekwipunek>();
        ekwipunek.ZniszczPrzedmiotyWekwipunku(new string[] { "salmon", "coffee", "salami", "cookie", "pizza" });
        gra=0;
        ciasto.SetActive(true);
        wyjdz();
        zacznij.SetActive(false);
        ja.SetActive(false);
    }
}
