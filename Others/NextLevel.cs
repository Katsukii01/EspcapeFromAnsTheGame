using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _nextLevel : MonoBehaviour
{
    public GameObject panel;
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
            _Menu.level=_Menu.level+1;
            if(_Menu.level<7){
                _Menu.levels_complete[_Menu.level-2] = true;
                _Menu.SaveProgress();
            }
            SceneManager.LoadSceneAsync(_Menu.level);  
        }
    }
}
