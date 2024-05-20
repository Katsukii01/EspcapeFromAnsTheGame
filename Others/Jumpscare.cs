using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jumpscare : MonoBehaviour
{
    public GameObject containerObject; // Obiekt zawierający elementy, które chcemy wybrać
    private List<GameObject> objectsInContainer = new List<GameObject>();
    private GameObject displayedObject;
    private bool aktywny = false;

    void Start()
    {
        // Pobierz wszystkie obiekty wewnątrz kontenera i dodaj je do listy
        foreach (Transform child in containerObject.transform)
        {
            objectsInContainer.Add(child.gameObject);
        }
    }

    void DisplayRandomObject()
    {
        aktywny=true;
        // Upewnij się, że istnieją jakieś obiekty w kontenerze
        if (objectsInContainer.Count > 0)
        {
            // Wybierz losowy obiekt
            int randomIndex = Random.Range(0, objectsInContainer.Count);
            GameObject randomObject = objectsInContainer[randomIndex];

            // Wyświetl wybrany obiekt
            randomObject.SetActive(true);
            displayedObject = randomObject;

            // Uruchom funkcję zanikania po 2 sekundach
            StartCoroutine(FadeOutObject());
        }
        else
        {
            Debug.LogWarning("Brak obiektów w kontenerze.");
        }
    }

    IEnumerator FadeOutObject()
    {
        // Poczekaj 2 sekundy
        yield return new WaitForSeconds(2f);

        // Upewnij się, że obiekt nadal istnieje (nie został usunięty w międzyczasie)
        if (displayedObject != null)
        {
            // Wygaszenie obiektu
            displayedObject.SetActive(false);
            Destroy(gameObject);
            aktywny=false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(aktywny==false){
                DisplayRandomObject();
            }
        }
    }
}
