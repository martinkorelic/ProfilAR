using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SetData : MonoBehaviour
{

    public TextMesh ime;
    public TextMesh starost;
    public TextMesh userTag;
    public MeshFilter astro;
    public TextMeshPro interesi;


    // Start is called before the first frame update
    void Start()
    {
        getData();
    }

    // Update is called once per frame
    void Update()
    {
        getData();
    }

    private void getData()
    {
        string qrText = CardDataController.QRText;
        if (qrText == "") return;
        string[] data = qrText.Split(';');
        if (data.Length < 5) return;
        string imeText = data[0];
        string birthday = data[1];
        string username = data[2];
        string interests = data[3];
        string color = data[4];

        DateTime date;
        if (!DateTime.TryParse(birthday, out date)) return;
        int age = DateTime.Now.Year - date.Year;
        if (DateTime.Now.Month < date.Month) --age;
        else if (DateTime.Now.Month == date.Month)
            if (DateTime.Now.Day < date.Day) --age;

        string ageText = age.ToString() + " let";
        string tagText = "@" + username;
        string[] interestsArray = interests.Split(',');
        string interestsText = "Interesi:\n";
        for (int i = 0; i < interestsArray.Length; ++i)
        {
            string interes = interestsArray[i];
            interes = interes.Trim();
            interes = char.ToUpper(interes[0]) + interes.Substring(1) + "\n";
            interestsText += interes;
        }

        Color textColor;
        switch (color.ToLower())
        {
            case "black":
                textColor = Color.black;
                break;
            case "blue":
                textColor = Color.blue;
                break;
            case "cyan":
                textColor = Color.cyan;
                break;
            case "green":
                textColor = Color.green;
                break;
            case "red":
                textColor = Color.red;
                break;
            case "magenta":
                textColor = Color.magenta;
                break;
            default:
                textColor = Color.black;
                break;
        }

        ime.text = imeText;
        ime.color = textColor;

        starost.text = ageText;
        starost.color = textColor;

        userTag.text = tagText;
        userTag.color = textColor;

        interesi.text = interestsText;
        interesi.color = textColor;
    }
}