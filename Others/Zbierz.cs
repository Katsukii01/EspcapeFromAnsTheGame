using UnityEngine;

public class Zbierz : MonoBehaviour
{
    public GameObject panel;
    public GameObject Eq;
    public GameObject PrzedmiotDoZebrania;
    private bool isPlayerInside = false;

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
            Ekwipunek ekwipunek = Eq.GetComponent<Ekwipunek>();
            if (ekwipunek != null)
            {
                ekwipunek.DodajPrzedmiot(PrzedmiotDoZebrania, gameObject);
            }

            panel.SetActive(false);
            isPlayerInside = false;
            
        }
    }
}
