using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterLoad : MonoBehaviour
{
    public GameObject char1;
    public GameObject char2;
    public GameObject char3;
    public GameObject[] przedmioty;
    public GameObject jumpscary;
    public GameObject leczenie;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    // Start is called before the first frame update
    void Start()
    {
        if(_CharacterPick.postac==1){
           char1.SetActive(true);
           l1.SetActive(true);
        }else if(_CharacterPick.postac==2){
            char2.SetActive(true);
             l2.SetActive(true);
        }else if(_CharacterPick.postac==3){
            char3.SetActive(true);
             l3.SetActive(true);
        }

        foreach (GameObject obj in przedmioty){
            SetActiveRandomObject(obj,1);
        }

        SetActiveRandomObject(leczenie,1);
        SetActiveRandomObject(jumpscary,3);
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
