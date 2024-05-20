using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class grafika : MonoBehaviour
{
    public GameObject to;
    public GameObject panel;
    public TextMeshProUGUI textMeshPro;
    private bool isPlayerInside = false;
    public AudioSource myFx;
    public AudioClip winFx;

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
            myFx.PlayOneShot(winFx);
            textMeshPro.color = Color.green;
            textMeshPro.text = "5";
            panel.SetActive(false);
            to.SetActive(false);
        }
    }
}
