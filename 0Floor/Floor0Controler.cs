using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Floor0Controler : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public GameObject Eq;

    public GameObject dead;
    public GameObject camera;
    public GameObject nextlevel;
    public GameObject klucze;

    public TextMeshProUGUI Bazy;
    public TextMeshProUGUI isp;
    public TextMeshProUGUI sieci;
    public TextMeshProUGUI drony;

    public GameObject task1;
    public GameObject task2;
    public GameObject task3;

    public GameObject oceny;

    public AudioSource myFx;
    public AudioClip passedFx;
    
    static bool isLevelCleared = false;
    bool klucz = false;
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
                if(Bazy.text == "3" && isp.text == "4" && sieci.text == "3" && drony.text == "911"){
                    level = 1;
                    spawnkey();
                }         
        }

        if(level==1){
            Ekwipunek ekwipunek = Eq.GetComponent<Ekwipunek>();
            klucz=ekwipunek.SprawdzPrzedmiotyWekwipunku(new string[] { "key"});
                if(klucz){
                    level = 2;
                   unlocknextlevel();
                }
        }   
    }

    void spawnkey(){
            SetActiveRandomObject(klucze,1);
            task1.SetActive(false);
            oceny.SetActive(false);
            task2.SetActive(true);
            myFx.PlayOneShot(passedFx);
        }

    void unlocknextlevel(){
            task2.SetActive(false);
            task3.SetActive(true);
            nextlevel.SetActive(true);
            myFx.PlayOneShot(passedFx);
    }

    public void SetActiveRandomObject(GameObject obj, int count)
    {
        // Sprawdzenie, czy podany obiekt nie jest nullem
        if (obj == null)
        {
            Debug.LogError("Obiekt jest pusty!");
            return;
        }

        // Pobranie liczby dzieci obiektu
        int childCount = obj.transform.childCount;

        // Upewnienie się, że liczba dzieci nie jest większa niż liczba dostępnych dzieci
        count = Mathf.Min(count, childCount);

        // Lista indeksów dzieci, które będą aktywowane
        var activeIndices = new HashSet<int>();

        // Losowe wybieranie indeksów dzieci do aktywacji
        while (activeIndices.Count < count)
        {
            int randomIndex = Random.Range(0, childCount);
            activeIndices.Add(randomIndex);
        }

        // Aktywacja wybranych dzieci
        for (int i = 0; i < childCount; i++)
        {
            bool isActive = activeIndices.Contains(i);
            obj.transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }
}
