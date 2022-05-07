using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public bool isCard;
    public bool isScanner;
    public bool isGenerator;
    public bool isExit;

    void OnMouseUp() {
        if (isCard)
            SceneManager.LoadScene("SampleScene");
        if (isScanner)
            SceneManager.LoadScene("QRScanner");
        if (isGenerator)
            SceneManager.LoadScene("QRGenerator");
        if (isExit)
            Application.Quit();
    }

}
