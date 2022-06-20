using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class arUI : MonoBehaviour
{
    public UIDocument uiDocument;
    public Button backButton;
    // Start is called before the first frame update
    void Start()
    {        
        if (uiDocument != null) {
            uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        var root = GetComponent<UIDocument>().rootVisualElement;
        backButton = root.Q<Button>("back-button");
        backButton.clicked += backButtonPressed;
    }

    void backButtonPressed() {
        SceneManager.LoadScene("StartSceneUI");
    }
}
