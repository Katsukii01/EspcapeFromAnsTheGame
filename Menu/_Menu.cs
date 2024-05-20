using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class _Menu : MonoBehaviour
{
    public GameObject przyciskiLevel;
    public GameObject[] lockers;

    public GameObject przyciskiMenu;
    static public int level = 2;
    static public bool[] levels_complete = new bool[5];

    public void Start()
    {
        Cursor.visible = true;
        LoadProgress();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        level = 2;
        SceneManager.LoadSceneAsync(1);
       
    }

    public void ShowLevels()
    {
        int i = 0;
        levels_complete[0] = true;
        foreach (bool b in levels_complete)
        {
            if (b == true)
            {
                lockers[i].SetActive(false);
                przyciskiLevel.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }
            i++;
        }
        przyciskiLevel.SetActive(true);
        przyciskiMenu.SetActive(false);
    }

    public void HideLevels()
    {
        przyciskiLevel.SetActive(false);
        przyciskiMenu.SetActive(true);
    }

    public void LoadLevel(int l)
    {
        level = l;
        SceneManager.LoadSceneAsync(1);
       
    }

    static public void SaveProgress()
    {
        string path = Application.dataPath + "/progress.txt";
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (bool level in levels_complete)
            {
                writer.WriteLine(level);
            }
        }
    }

    public void LoadProgress()
    {
        string path = Application.dataPath + "/progress.txt";
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                int index = 0;
                string line;
                while ((line = reader.ReadLine()) != null && index < levels_complete.Length)
                {
                    levels_complete[index] = bool.Parse(line);
                    index++;
                }
            }
        }
        else
        {
            // Initialize the levels_complete array if the file does not exist
            for (int i = 0; i < levels_complete.Length; i++)
            {
                levels_complete[i] = false;
            }
            // Create the file to save initial progress
            SaveProgress();
        }
    }
}
