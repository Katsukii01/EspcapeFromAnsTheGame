using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Latarka : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject latarka;
    public GameObject glow;
    public bool enabled;

    void Start()
    {
        enabled = true;
    }

    void Update()
    {
        // Jeśli naciśnięto klawisz F1, przełączamy się między kamerami
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Sprawdzamy, która kamera jest obecnie włączona i zmieniamy na drugą
            if (enabled)
            {
                latarka.SetActive(false);
                glow.SetActive(false);
                enabled = false;
            }
            else
            {
                latarka.SetActive(true);
                glow.SetActive(true);
                enabled = true;
            }
        }
    }
}
