using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    public  GameObject ending1;
    public  GameObject ending2;
    public  GameObject zgon;
    public  GameObject dyplom;
    
    public  GameObject char1;
    public  GameObject char2;
    public  GameObject char3;
    public  GameObject police;
    public  GameObject cop;
    public  GameObject kraty;
    public  GameObject deport;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        if(FloorHellController.ending==1){
            ending1.SetActive(true);
            dyplom.SetActive(true);
        }else{
            ending2.SetActive(true);
            zgon.SetActive(true);
        }

        if(_CharacterPick.postac==1){
           char1.SetActive(true);
        }else if(_CharacterPick.postac==2){
            char2.SetActive(true);
        }else if(_CharacterPick.postac==3){
            char3.SetActive(true);
            police.SetActive(true);
            cop.SetActive(true);
            kraty.SetActive(true);
            deport.SetActive(true);
             StartFade();
        }

    }

    float fadeDuration = 0.6f; // Czas trwania efektu zanikania/wyświetlania w sekundach
    float minScale = 0.3f; // Minimalna skala obrazków
    float maxScale = 0.7f; // Maksymalna skala obrazków
    public Image image1; // Pierwszy obrazek
    public Image image2; // Drugi obrazek
    float minAlpha = 0.1f; // Minimalna wartość alfa
    float maxAlpha = 0.5f; // Maksymalna wartość alfa
    private float timer = 0f;
    private bool isFading = false;
    private bool fadeInImage1 = true; // Zmienna określająca, czy obrazek 1 ma się pojawiać (true) czy zanikać (false)

void Update()
    {
        if (isFading)
        {
            timer += Time.deltaTime;
            float alpha;
            float scale;

            if (fadeInImage1)
            {
                alpha = Mathf.Lerp(minAlpha, maxAlpha, timer / fadeDuration); // Pojawianie się obrazka 1
                scale = Mathf.Lerp(minScale, maxScale, timer / fadeDuration); // Zwiększanie skali obrazka 1
                image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, alpha);
                image1.transform.localScale = new Vector3(scale, scale, 1f);
                image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, maxAlpha - alpha); // Zanikanie obrazka 2
                scale = Mathf.Lerp(maxScale, minScale, timer / fadeDuration); // Pomniejszanie skali obrazka 2
                image2.transform.localScale = new Vector3(scale, scale, 1f);
            }
            else
            {
                alpha = Mathf.Lerp(minAlpha, maxAlpha, timer / fadeDuration); // Pojawianie się obrazka 2
                scale = Mathf.Lerp(minScale, maxScale, timer / fadeDuration); // Zwiększanie skali obrazka 2
                image2.color = new Color(image2.color.r, image2.color.g, image2.color.b, alpha);
                image2.transform.localScale = new Vector3(scale, scale, 1f);
                image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, maxAlpha - alpha); // Zanikanie obrazka 1
                scale = Mathf.Lerp(maxScale, minScale, timer / fadeDuration); // Pomniejszanie skali obrazka 1
                image1.transform.localScale = new Vector3(scale, scale, 1f);
            }

            if (timer >= fadeDuration)
            {
                isFading = false;
                timer = 0f;
                fadeInImage1 = !fadeInImage1; // Zmiana flagi pojawiania/zanikania obrazka
                StartFade();
            }
        }
    }

    public void StartFade()
    {
        timer = 0f;
        isFading = true;
    }
}
