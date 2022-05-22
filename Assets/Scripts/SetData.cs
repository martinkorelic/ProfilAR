using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.iOS;

public class SetData : MonoBehaviour
{

    public TextMesh ime;
    public TextMesh starost;
    public TextMesh userTag;
    public MeshFilter astro;
    //public TextMeshPro interesi;
    public TextMeshPro osebnost;
    public TextMeshPro sport;
    public TextMeshPro animal;
    public TextMeshPro social; 
    public Mesh[] astrologySigns;

    public TextMeshPro osebnostPopupText;
    public TextMeshPro sportPopupText;
    public TextMeshPro animalPopupText;
    public TextMeshPro socialPopopText;

    public Animator osebnostPopAnim;
    public Animator sportPopAnim;
    public Animator animalPopAnim;
    public Animator socialPopAnim;

    private string[] astrologies = {"aries", "taurus", "gemini", "cancer", "leo", "virgo", "libra", "scorpio", "sagittarius", "capricorn", "aquarius", "pisces"};

    // Start is called before the first frame update
    void Start()
    {
        getData();
    }

    // Update is called once per frame
    void Update()
    {
        getData();

        if (Input.GetMouseButtonUp(0)) {
            //Debug.Log("Button 0");
            //osebnostPopup.SetActive(true);
            //osebnostPopAnim.Play("Base Layer");
            osebnostPopAnim.SetTrigger("Active");
            sportPopAnim.SetTrigger("Active");
            animalPopAnim.SetTrigger("Active");
            socialPopAnim.SetTrigger("Active");

        }

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Debug.Log(touch.phase);
        }

        /*
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                // Trigger
                Debug.Log("idjwid");
            }
        }*/
    }

    public void Back()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void getData()
    {
        string qrText = CardDataController.QRText;
        if (qrText == "") return;
        string[] data = qrText.Split(';');
        if (data.Length < 10) return;
        string imeText = data[0];
        string birthday = data[1];
        string username = data[2];
        string interests = data[3];
        string color = data[4];
        string astrosign = data[5].ToLower();

        string animalText = $"<sprite=\"animal_spritesheet\" index={data[6]}>";
        string osebnostText = $"<sprite=\"osebnost_spritesheet\" index={data[7]}>";
        string socialText = $"<sprite=\"socials_spritesheet\" index={data[8]}>";
        string sportText = $"<sprite=\"sport_spritesheet\" index={data[9]}>";

        animal.text = animalText;
        osebnost.text = osebnostText;
        social.text = socialText;
        sport.text = sportText;

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

        // TODO: Set popup text

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

        int meshSign = 0; 
        for (int i = 0; i < astrologies.Length; i++) {
            if (astrosign.Equals(astrologies[i])) {
                meshSign = i;
                break;
            }
        }

        astro.mesh = astrologySigns[meshSign];

        ime.text = imeText;
        ime.color = textColor;

        starost.text = ageText;
        starost.color = textColor;

        userTag.text = tagText;
        userTag.color = textColor;

        //interesi.text = interestsText;
        //interesi.color = textColor;
    }
}
