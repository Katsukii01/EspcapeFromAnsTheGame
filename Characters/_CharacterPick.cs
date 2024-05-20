using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _CharacterPick : MonoBehaviour
{
    static public int postac=0;
    public GameObject button1;
    public bool stanButton=false;
    public GameObject text1;
    public GameObject pick1; 
    public GameObject panel1;
    public GameObject pick2;
    public GameObject panel2;
    public GameObject pick3;
    public GameObject panel3;


	void Awake()
	{
		if(postac==0){
        panel1.SetActive(false);
        text1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        }
    }
    public void picked1()
    {
        postac=1;
        if(stanButton==false){
           button1.GetComponent<Button>().interactable = true; 
           stanButton=true;
        }
        // Change the color of the panel
        pick1.GetComponent<Image>().color = Color.gray;
        pick2.GetComponent<Image>().color = Color.red;
        pick3.GetComponent<Image>().color = Color.black;

        // Show or hide the panel
        panel1.SetActive(true);
        text1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }
    public void picked2()
    {
        postac=2;
        if(stanButton==false){
           button1.GetComponent<Button>().interactable = true; 
           stanButton=true;
        }
        // Change the color of the panel
        pick1.GetComponent<Image>().color = Color.red;
        pick2.GetComponent<Image>().color = Color.gray;
        pick3.GetComponent<Image>().color = Color.black;

        // Show or hide the panel
        panel1.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
        text1.SetActive(false);
    }
    public void picked3()
    {
        postac=3;
        if(stanButton==false){
           button1.GetComponent<Button>().interactable = true; 
           stanButton=true;
        }
        // Change the color of the panel
        pick1.GetComponent<Image>().color = Color.red;
        pick2.GetComponent<Image>().color = Color.red;
        pick3.GetComponent<Image>().color = Color.gray;

        // Show or hide the panel
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
        text1.SetActive(false);
    }

    public void GameStart()
    {
       SceneManager.LoadSceneAsync(_Menu.level);
        Cursor.visible = false;
    }
}
