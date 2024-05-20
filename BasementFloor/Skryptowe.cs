using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Skryptowe : MonoBehaviour
{
public GameObject[] objectsToDisable;
    public GameObject camera;

    public RawImage winimage;
    public RawImage rawImage;
    
    float moveSpeed = 300f;
    public RectTransform[] movementPanels; 
    public RectTransform[] collisionPanels;

    public GameObject l1;
    public GameObject l2;
    public GameObject l3;

    public AudioSource myFx;
    public AudioClip lostFx;
    public AudioClip winFx;

    public GameObject to;
    public GameObject panel;
    public GameObject zabierz;

    public GameObject game;
   
    public TextMeshProUGUI textMeshPro;
    private bool isPlayerInside = false;
    bool gra=false;
    bool wygrana=false;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(true);
            isPlayerInside = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(false);
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            isPlayerInside = false; 
            panel.SetActive(false);
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
            camera.SetActive(true);
            game.SetActive(true);
            gra=true;
        }else if(gra==true){
            // Pobieranie wejścia z klawiatury
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Obliczanie nowej pozycji obrazu
            Vector3 newPosition = rawImage.rectTransform.position;
            newPosition.x += horizontalInput * moveSpeed * Time.deltaTime;
            newPosition.y += verticalInput * moveSpeed * Time.deltaTime;

            // Pobieranie rozmiarów obrazu
            Vector2 imageSize = rawImage.rectTransform.sizeDelta;

            // Sprawdzanie, czy którykolwiek piksel obrazu znajduje się w panelu kolizyjnym
            // Sprawdzanie, czy nowa pozycja nachodzi na panel kolizyjny
            bool isCollision = false;
            foreach (RectTransform panel in collisionPanels)
            {
                Vector3 panelPosition = panel.position;
                Vector2 panelSize = panel.sizeDelta;

                // Obliczanie krawędzi obrazu
                float imageLeft = newPosition.x - imageSize.x / 2;
                float imageRight = newPosition.x + imageSize.x / 2;
                float imageTop = newPosition.y + imageSize.y / 2;
                float imageBottom = newPosition.y - imageSize.y / 2;

                imageLeft = imageLeft -40;
                imageRight = imageRight +40;
                imageTop = imageTop +40;
                imageBottom = imageBottom -40;

                // Obliczanie krawędzi panelu
                float panelLeft = panelPosition.x - panelSize.x / 2;
                float panelRight = panelPosition.x + panelSize.x / 2;
                float panelTop = panelPosition.y + panelSize.y / 2;
                float panelBottom = panelPosition.y - panelSize.y / 2;

                // Sprawdzanie kolizji
                if (imageRight > panelLeft && imageLeft < panelRight &&
                    imageTop > panelBottom && imageBottom < panelTop)
                {
                    isCollision = true;
                    break;
                }

            }

                // Obliczanie krawędzi obrazu
                float imageLeft2 = newPosition.x - imageSize.x / 2;
                float imageRight2 = newPosition.x + imageSize.x / 2;
                float imageTop2 = newPosition.y + imageSize.y / 2;
                float imageBottom2 = newPosition.y - imageSize.y / 2;

                imageLeft2 = imageLeft2 +8;
                imageRight2 = imageRight2 -8;
                imageTop2 = imageTop2 -8;
                imageBottom2 = imageBottom2 +8;


            // Sprawdzanie kolizji dla obrazka "winimage"
            Vector3 winImagePosition = winimage.rectTransform.position;
            Vector2 winImageSize = winimage.rectTransform.sizeDelta;

            bool isWinCollision = false;

            // Obliczanie krawędzi obrazu "winimage"
            float winImageLeft = winImagePosition.x - winImageSize.x / 2;
            float winImageRight = winImagePosition.x + winImageSize.x / 2;
            float winImageTop = winImagePosition.y + winImageSize.y / 2;
            float winImageBottom = winImagePosition.y - winImageSize.y / 2;

            if (imageRight2 > winImageLeft && imageLeft2 < winImageRight &&
                    imageTop2 > winImageBottom && imageBottom2 < winImageTop)
            {
                isWinCollision = true;
            }
    

            // Aktualizacja pozycji obrazu, jeśli jest wewnątrz panelu ruchu i nie występuje kolizja
            if (isWinCollision)
            {      
                wygrana=true;
                endgame();  
        
            }else if(!isCollision){
                rawImage.rectTransform.position = newPosition;
            }
            else // Wywołanie funkcji EndGame, jeśli wyjdzie poza panele lub wystąpi kolizja
            {
                endgame();
            }
        }
    }

   public void endgame(){
        gra=false;
        camera.SetActive(false);
        game.SetActive(false);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
        
        if (wygrana==true)
        {
            win();
        }
        else
        {
            lost();
        }
    }

    void win(){ 
        wygrana=false;
        textMeshPro.color = Color.green;
        textMeshPro.text = "3";
        panel.SetActive(false);
        to.SetActive(false);
        myFx.PlayOneShot(winFx);
    }

    void lost(){
        wygrana=false;
         zycieZabierz();
        zabierz.SetActive(true);
        StartCoroutine(WylaczPoCzasie(zabierz, 2f));
         myFx.PlayOneShot(lostFx);
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

    IEnumerator WylaczPoCzasie(GameObject obj, float czas)
    {
        yield return new WaitForSeconds(czas);
        obj.SetActive(false);
    } 
}
