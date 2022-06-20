using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;
using UnityEngine.SceneManagement;

public class QRCodeGenerator : MonoBehaviour
{

    public RawImage rawImage;
    public TMP_InputField fullName;
    public TMP_InputField age;
    public TMP_InputField username;
    public TMP_InputField interests;
    public TMP_Dropdown color;
    public TMP_Dropdown astrosign;
    public TMP_Dropdown animal;
    public TMP_Dropdown osebnost;
    public TMP_Dropdown social;
    public TMP_Dropdown sport;

    public TMP_Text notification;

    private Texture2D encodedTexture;

    // Start is called before the first frame update
    void Start()
    {
        encodedTexture = new Texture2D(256, 256);
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

    public void OnClickEncode()
    {
        EncodeText();
    }

    public void Back()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void EncodeText()
    {

        string nameText = fullName.text;
        string ageText = age.text;
        string userText = username.text;
        string interestsText = interests.text;
        string colorText = color.options[color.value].text;
        string astroText = astrosign.options[astrosign.value].text;

        if (string.IsNullOrEmpty(nameText))
        {
            StartCoroutine(Notify("Name must not be empty", 3));
            return;
        }
        if (string.IsNullOrEmpty(ageText))
        {
            StartCoroutine(Notify("Age must not be empty", 3));
            return;
        }
        if (string.IsNullOrEmpty(userText))
        {
            StartCoroutine(Notify("Username must not be empty", 3));
            return;
        }
        if (string.IsNullOrEmpty(interestsText))
        {
            StartCoroutine(Notify("Interests must not be empty", 3));
            return;
        }

        DateTime result;

        if (!DateTime.TryParseExact(ageText, new[] { "dd/MM/yyyy" }, null, DateTimeStyles.None, out result))
        {
            StartCoroutine(Notify("Wrong date format", 3));
            return;
        }

        string textWrite = $"{nameText};{result.ToShortDateString()};{userText};{interestsText};{colorText};{astroText};{animal.value};{osebnost.value};{social.value};{sport.value}";

        CardDataController.QRText = textWrite;

        Color32[] convertToTexture = Encode(textWrite, encodedTexture.width, encodedTexture.height);
        encodedTexture.SetPixels32(convertToTexture);
        encodedTexture.Apply();

        rawImage.texture = encodedTexture;
    }

    IEnumerator Notify(string text, int time)
    {
        notification.text = text;
        yield return new WaitForSeconds(time);
        notification.text = "";
    }

}
