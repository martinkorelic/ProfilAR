using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using ZXing;
using ZXing.QrCode;
using System.Globalization;


public class generatorUI : MonoBehaviour
{
    public UIDocument uiDocument;
    public Button backButton;
    public Button nextButton;
    public Button plusButton;
    public Button minusButton;

    public VisualElement personalData;
    public VisualElement iconData;
    public VisualElement img;
    public VisualElement panelqr;

    public VisualElement qrShowcase;

    public Texture2D[] animals;
    public Texture2D[] osebnosti;
    public Texture2D[] social;
    public Texture2D[] sport;
    

    public int step = 0;

    public int curIndex = 0;

    public int indexAnimal = 0;
    public int indexOsebnost = 0;
    public int indexSocial = 0;
    public int indexSport = 0;

    public string name = "janez novak";
    public string date = "10/10/2020";
    public string nickname = "janezek";
    public string interesi = "meditacija";

    private Texture2D encodedTexture;

    // Start is called before the first frame update
    void Start()
    {        
        encodedTexture = new Texture2D(256, 256);
        if (uiDocument != null) {
            uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        var root = GetComponent<UIDocument>().rootVisualElement;
        backButton = root.Q<Button>("back-button");
        nextButton = root.Q<Button>("next-button");
        plusButton = root.Q<Button>("plus-button");
        minusButton = root.Q<Button>("minus-button");
        qrShowcase = root.Q<VisualElement>("qr");
        panelqr = root.Q<VisualElement>("panelqr");
        backButton.clicked += backButtonPressed;
        nextButton.clicked += nextStep;
        plusButton.clicked += nextImg;
        minusButton.clicked += prevImg;

        personalData = root.Q<VisualElement>("panelPersonal");
        iconData = root.Q<VisualElement>("panelIcon");
        img = root.Q<VisualElement>("icon");
    }

    void nextStep() {
        if (step == 1) {
            indexAnimal = curIndex;
        }
        else if (step == 2) {
            indexOsebnost = curIndex;
        }
        else if (step == 3) {
            indexSocial = curIndex;
        }
        else if (step == 4) {
            indexSport = curIndex;
        }
        curIndex = 0;

        if (step == 0) {
            personalData.style.display = DisplayStyle.None;
            iconData.style.display = DisplayStyle.Flex;
        }
        if (step == 4) {
            Debug.Log("generate");
            iconData.style.display = DisplayStyle.None;
            panelqr.style.display = DisplayStyle.Flex;
            nextButton.text = "Try AR";
            EncodeText();
        }

        if (step == 5) {
            SceneManager.LoadScene("SampleScene");
        }
        
        step++;
        drawIcon();
    }

    void nextImg() {
        curIndex = (curIndex + 1) % 12;
        Debug.Log(curIndex);
        drawIcon();
    }

    void prevImg() {
        curIndex = (curIndex - 1);
        if (curIndex < 0) {
            curIndex = 11;
        }
        Debug.Log(curIndex);
        drawIcon();
    }

    private Color32[] Encode(string text, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(text);
    }

    void drawIcon() {
        if (step == 1) {
            img.style.backgroundImage = animals[curIndex];
        }
        if (step == 2) {
            img.style.backgroundImage = osebnosti[curIndex];
        }
        if (step == 3) {
            img.style.backgroundImage = social[curIndex];
        }
        if (step == 4) {
            img.style.backgroundImage = sport[curIndex];
        }
    }

    void backButtonPressed() {
        SceneManager.LoadScene("StartSceneUI");
    }

    private void EncodeText()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;



        string nameText = root.Q<TextField>("name").text;
        Debug.Log(nameText);
        string ageText = root.Q<TextField>("date").text;;
        string userText = root.Q<TextField>("nick").text;;
        string interestsText = root.Q<TextField>("interes").text;;
        string colorText = "magenta";//color.options[color.value].text;
        string astroText = "virgin";//astrosign.options[astrosign.value].text;

        if (string.IsNullOrEmpty(nameText))
        {
            return;
        }
        if (string.IsNullOrEmpty(ageText))
        {
            return;
        }
        if (string.IsNullOrEmpty(userText))
        {
            return;
        }
        if (string.IsNullOrEmpty(interestsText))
        {
            return;
        }

        DateTime result;

        if (!DateTime.TryParseExact(ageText, new[] { "dd/MM/yyyy" }, null, DateTimeStyles.None, out result))
        {
            return;
        }

        string textWrite = $"{nameText};{result.ToShortDateString()};{userText};{interestsText};{colorText};{astroText};{indexAnimal};{indexOsebnost};{indexSocial};{indexSport}";

        Debug.Log(textWrite);

        CardDataController.QRText = textWrite;

        Color32[] convertToTexture = Encode(textWrite, encodedTexture.width, encodedTexture.height);
        encodedTexture.SetPixels32(convertToTexture);
        encodedTexture.Apply();

        qrShowcase.style.backgroundImage = encodedTexture;
    }
}
