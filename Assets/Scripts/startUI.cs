using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class startUI : MonoBehaviour
{
    public UIDocument uiDocument;
     
    public Button scanButton;
    public Button generateButton;

    // Start is called before the first frame update
    void Start()
    {
        if (uiDocument != null) {
            uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        var root = GetComponent<UIDocument>().rootVisualElement;
        scanButton = root.Q<Button>("scan-button");
        generateButton = root.Q<Button>("generate-button");
        scanButton.clicked += scanButtonPressed;
        generateButton.clicked += generateButtonPressed;
    }

    void scanButtonPressed() {
        SceneManager.LoadScene("QRScannerUI");
    }

    void generateButtonPressed() {
        SceneManager.LoadScene("QRGeneratorUI");
    }
}
