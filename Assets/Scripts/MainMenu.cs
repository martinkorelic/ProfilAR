using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public bool isCard;
    public bool isScanner;
    public bool isGenerator;
    public bool isExit;

    [SerializeField]
    public TextMeshProUGUI notification;

    void OnMouseUp()
    {
        if (isCard)
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (isScanner)
            SceneManager.LoadScene("QRScanner");
        if (isGenerator)
            SceneManager.LoadScene("QRGenerator");
        if (isExit)
            Application.Quit();
    }

    IEnumerator Notify(string text, int time)
    {
        notification.text = text;
        yield return new WaitForSeconds(time);
        notification.text = "";
    }

}
