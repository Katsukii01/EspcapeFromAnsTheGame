using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ekwipunek : MonoBehaviour
{
    public GameObject zaDuzo;
    public GameObject nieJ;
    public GameObject nie;
    public GameObject l1;
    public GameObject l2;
    public GameObject niema;
    public GameObject healed;
    public GameObject blad; 
    public AudioSource myFx;
    public AudioClip collectFx;
    public AudioClip healFx;
    public AudioClip sadFx;
    public GameObject[] sloty; // Tablica slotów w ekwipunku
    public int aktualnySlotIndex = 0; // Indeks aktualnie wybranego slotu
    public KeyCode[] numeryEkwipunkuKeys; // Klawisze cyfr do zmiany na konkretny numer ekwipunku
    public KeyCode uzyjPrzedmiotuKey = KeyCode.Return;
    public Color kolorZwykly = new Color(0.937f, 0.729f, 0.157f, 1f); // Kolor zwykły (EFBA28)
    public Color kolorPrzezroczysty = new Color(1f, 1f, 1f, 0f); // Kolor przezroczysty

    void Update()
    {
        // Obsługa zmiany numeru ekwipunku po naciśnięciu odpowiedniej cyfry
        for (int i = 0; i < numeryEkwipunkuKeys.Length; i++)
        {
            if (Input.GetKeyDown(numeryEkwipunkuKeys[i]))
            {
                ZmienEkwipunek(i);
            }
        }

        // Obsługa przewijania slotów za pomocą kółka myszy
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            PrzewinSloty((int)Mathf.Sign(scrollDelta));
        }

        if (Input.GetKeyDown(uzyjPrzedmiotuKey))
        {
            UzyjPrzedmiotu();
        }
    }

    void PrzewinSloty(int direction)
    {
        int nowyIndeks = aktualnySlotIndex + direction;
        if (nowyIndeks >= 0 && nowyIndeks < sloty.Length)
        {
            ZmienKolorSlotu(sloty[aktualnySlotIndex],kolorPrzezroczysty); // Wyłącz obecny slot
            aktualnySlotIndex = nowyIndeks;
            ZmienKolorSlotu(sloty[aktualnySlotIndex],kolorZwykly); // Włącz nowy slot
        }
    }

    void ZmienEkwipunek(int newIndex)
    {
        if (newIndex >= 0 && newIndex < sloty.Length)
        {
            ZmienKolorSlotu(sloty[aktualnySlotIndex],kolorPrzezroczysty); // Wyłącz obecny slot
            aktualnySlotIndex = newIndex;
            ZmienKolorSlotu(sloty[aktualnySlotIndex],kolorZwykly); // Włącz nowy slot
        }
    }

    void ZmienKolorSlotu(GameObject slot, Color kolor)
    {
        Image obrazekSlotu = slot.GetComponent<Image>();
        if (obrazekSlotu != null)
        {
            obrazekSlotu.color = kolor;
        }
    }

void UzyjPrzedmiotu()
{
    GameObject aktualnySlot = sloty[aktualnySlotIndex];
    if (aktualnySlot.transform.childCount > 0)
    {
        GameObject przedmiot = aktualnySlot.transform.GetChild(0).gameObject;
        string nazwaPrzedmiotu = przedmiot.name;

        // Sprawdź nazwę przedmiotu i wykonaj odpowiednie działanie
        if (nazwaPrzedmiotu == "heal")
        {
            if(_CharacterPick.postac==1){
                   if(l1.transform.childCount<4){
                        // Pobierz ostatnie dziecko z transformacji "l1"
                        Transform ostatnieDziecko = l1.transform.GetChild(l1.transform.childCount - 1);

                        // Skopiuj ostatnie dziecko
                        GameObject noweDziecko = Instantiate(ostatnieDziecko.gameObject);

                        // Ustaw nowe dziecko na wierzchu
                        noweDziecko.transform.SetParent(l1.transform);
                        noweDziecko.transform.SetAsLastSibling();

                        // Ustaw pozycję nowego dziecka na prawo od ostatniego dziecka
                        Vector3 pozycja = ostatnieDziecko.localPosition;
                        pozycja.x += 95f;
                        noweDziecko.transform.localPosition = pozycja;
                        // Skopiuj skalę oryginalnego dziecka
                        noweDziecko.transform.localScale = ostatnieDziecko.localScale;
                        myFx.PlayOneShot(healFx);
                        healed.SetActive(true);
                        StartCoroutine(WylaczPoCzasie(healed, 1f));
                        Destroy(przedmiot);
                   }else{
                       zaDuzo.SetActive(true);
                       StartCoroutine(WylaczPoCzasie(zaDuzo, 1f));
                   }
            }else if(_CharacterPick.postac==2){
                   if(l2.transform.childCount<4){
                           // Pobierz ostatnie dziecko z transformacji "l2"
                        Transform ostatnieDziecko = l2.transform.GetChild(l2.transform.childCount - 1);

                        // Skopiuj ostatnie dziecko
                        GameObject noweDziecko = Instantiate(ostatnieDziecko.gameObject);

                        // Ustaw nowe dziecko na wierzchu
                        noweDziecko.transform.SetParent(l2.transform);
                        noweDziecko.transform.SetAsLastSibling();
                        
                        // Skopiuj skalę oryginalnego dziecka
                        noweDziecko.transform.localScale = ostatnieDziecko.localScale;
                        // Ustaw pozycję nowego dziecka na prawo od ostatniego dziecka
                        Vector3 pozycja = ostatnieDziecko.localPosition;
                        pozycja.x += 95f;
                        noweDziecko.transform.localPosition = pozycja;
                        myFx.PlayOneShot(healFx);
                        healed.SetActive(true);
                        StartCoroutine(WylaczPoCzasie(healed, 1f));
                        Destroy(przedmiot);
                   }else{
                       zaDuzo.SetActive(true);
                       StartCoroutine(WylaczPoCzasie(zaDuzo, 1f));
                   }

            }else if(_CharacterPick.postac==3){  
                nieJ.SetActive(true);
                myFx.PlayOneShot(sadFx);
                StartCoroutine(WylaczPoCzasie(nieJ, 3f)); 
                Destroy(przedmiot);  
            }
                
        }
        else if (nazwaPrzedmiotu == "Damage")
        {
              BossController bossController = FindObjectOfType<BossController>();
            if(bossController != null)
            {
                bossController.TakeDamage(1);
            }
            Destroy(przedmiot);
        }
        else
        {
            nie.SetActive(true);
            StartCoroutine(WylaczPoCzasie(nie, 1f));      
        }
    }else{
            niema.SetActive(true);
            StartCoroutine(WylaczPoCzasie(niema, 1f));   
    }
}


public void DodajPrzedmiot(GameObject przedmiot, GameObject zniszcz )
{
    if (sloty[aktualnySlotIndex] != null)
    {
        // Sprawdzenie, czy slot nie jest już zajęty
        if (sloty[aktualnySlotIndex].transform.childCount == 0)
        {
            // Ustawianie rodzica dla przedmiotu
            przedmiot.transform.parent = sloty[aktualnySlotIndex].transform;
            
            // Resetowanie pozycji, rotacji i skali
            przedmiot.transform.localPosition = Vector3.zero;
            przedmiot.transform.localRotation = Quaternion.identity;
            przedmiot.transform.localScale = Vector3.one;

            // Ustawianie przedmiotu na wierzchu w hierarchii rodzica
            przedmiot.transform.SetAsLastSibling();
            Destroy(zniszcz);
            myFx.PlayOneShot(collectFx);
        }
        else
        {
        // Wywołanie funkcji do aktywacji błędu
        blad.SetActive(true);

        // Uruchomienie korutyny, aby po 3 sekundach wyłączyć obiekt "blad"
        StartCoroutine(WylaczPoCzasie(blad, 1f));
        }
    }
}

IEnumerator WylaczPoCzasie(GameObject obj, float czas)
{
    yield return new WaitForSeconds(czas);
    obj.SetActive(false);
}

public bool SprawdzPrzedmiotyWekwipunku(string[] listaNazwPrzedmiotow)
    {
        foreach (string nazwaPrzedmiotu in listaNazwPrzedmiotow)
        {
            bool znalezionoPrzedmiot = false;

            foreach (GameObject slot in sloty)
            {
                if (slot.transform.childCount > 0)
                {
                    GameObject przedmiotWslotcie = slot.transform.GetChild(0).gameObject;
        
                    if (przedmiotWslotcie.name == nazwaPrzedmiotu)
                    {
                        znalezionoPrzedmiot = true;
    
                        break;
                    }
                }
            }

            // Jeśli dla któregoś przedmiotu z listy nie znaleziono odpowiedniego slotu, zwróć false
            if (!znalezionoPrzedmiot)
            {
                return false;
            }
        }

        // Jeśli wszystkie przedmioty z listy zostały znalezione w ekwipunku, zwróć true
        return true;
    }
public void ZniszczPrzedmiotyWekwipunku(string[] listaNazwPrzedmiotow)
    {
        foreach (string nazwaPrzedmiotu in listaNazwPrzedmiotow)
        {
            bool znalezionoPrzedmiot = false;

            foreach (GameObject slot in sloty)
            {
                if (slot.transform.childCount > 0)
                {
                    GameObject przedmiotWslotcie = slot.transform.GetChild(0).gameObject;
        
                    if (przedmiotWslotcie.name == nazwaPrzedmiotu)
                    {
                        Destroy(przedmiotWslotcie);
                    }
                }
            }
        }
    }
}
