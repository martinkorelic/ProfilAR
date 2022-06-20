using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
//using UnityEngine.iOS;

public class SetData : MonoBehaviour
{

    public TextMesh ime;
    public TextMesh starost;
    public TextMesh userTag;
    public MeshFilter astro;

    public TextMeshPro osebnost;
    public TextMeshPro sport;
    public TextMeshPro animal;
    public TextMeshPro social;

    public TextMeshPro opis;

    public Mesh[] astrologySigns;

    public Material[] coloring;

    public TextMeshPro[] popupTexts;

    /*
    public TextMeshPro osebnostPopupText;
    public TextMeshPro sportPopupText;
    public TextMeshPro animalPopupText;
    public TextMeshPro socialPopopText;
    */

    public Animator osebnostPopAnim;
    public Animator sportPopAnim;
    public Animator animalPopAnim;
    public Animator socialPopAnim;

    private string[] astrologies = {"aries", "taurus", "gemini", "cancer", "leo", "virgo", "libra", "scorpio", "sagittarius", "capricorn", "aquarius", "pisces"};

    // Napiši texts
    private string[] osebnostTM = {"Priden", "Porednež", "Kulski", "Nor", "Flirty", "Sramežljiv", "Zaspan", "Quirky", "Pohlepen", "Pametnjakovič", "Kavboj", "Jezen"};
    private string[] sportTM = {"Gaming", "Biljard", "Tenis", "Pingpong", "Smučanje", "Košarka", "Rugby", "Karate", "Fitnes", "Odbojka", "Hokej", "Šah"};
    private string[] animalTM = {"Čebela", "Miš", "Krava", "Volk", "Mačka", "Konj", "Opica", "Medved", "Pes", "Prašič", "Žaba", "Panda"};
    private string[] socialTM = {"Discord", "Facebook", "Github", "Twitter", "Linkedin", "Pinterest", "Reddit", "Whatsapp", "Telegram", "Tiktok", "Tinder", "Youtube"};

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

        /*
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
        //string astrosign = data[5].ToLower();

        string animalText = $"<sprite=\"animal_spritesheet\" index={data[6]}>";
        string osebnostText = $"<sprite=\"osebnost_spritesheet\" index={data[7]}>";
        string socialText = $"<sprite=\"socials_spritesheet\" index={data[8]}>";
        string sportText = $"<sprite=\"sport_spritesheet\" index={data[9]}>";

        // Nastavi sprites
        animal.text = animalText;
        osebnost.text = osebnostText;
        social.text = socialText;
        sport.text = sportText;

        // Nastavi popup texte
        
        popupTexts[2].text = animalTM[int.Parse(data[6])];
        popupTexts[0].text = osebnostTM[int.Parse(data[7])];
        popupTexts[3].text = socialTM[int.Parse(data[8])];
        popupTexts[1].text = sportTM[int.Parse(data[9])];

        DateTime date;
        if (!DateTime.TryParse(birthday, out date)) return;
        int age = DateTime.Now.Year - date.Year;
        if (DateTime.Now.Month < date.Month) --age;
        else if (DateTime.Now.Month == date.Month)
            if (DateTime.Now.Day < date.Day) --age;

        // Astrology
        string sign = "";

        switch (date.Month)
        {
            case 1:
                if (date.Day > 20) sign = "aquarius";
                else sign = "capricorn";
                break;
            case 2:
                if (date.Day > 19) sign = "pisces";
                else sign = "aquarius";
                break;
            case 3:
                if (date.Day > 21) sign = "aries";
                else sign = "pisces";
                break;
            case 4:
                if (date.Day > 20) sign = "taurus";
                else sign = "pisces";
                break;
            case 5: 
                if (date.Day > 21) sign = "gemini";
                else sign = "taurus";
                break;
            case 6:
                if (date.Day > 21) sign = "cancer";
                else sign = "gemini";
                break;
            case 7:
                if (date.Day > 23) sign = "leo";
                else sign = "cancer";
                break;
            case 8: 
                if (date.Day > 23) sign = "virgo";
                else sign = "leo";
                break;
            case 9:
                if (date.Day > 23) sign = "libra";
                else sign = "virgo";
                break;
            case 10:
                if (date.Day > 23) sign = "scorpio";
                else sign = "libra";
                break;
            case 11:
                if (date.Day > 22) sign = "sagittarius";
                else sign = "scorpio";
                break;
            case 12:
                if (date.Day > 22) sign = "capricorn";
                else sign = "sagittarius";
                break;
            default:
                sign = "ERROR";
                break;
        }

        string ageText = age.ToString() + " let";
        string tagText = "@" + username;

        /*
        string[] interestsArray = interests.Split(',');
        string interestsText = "Interesi:\n";
        for (int i = 0; i < interestsArray.Length; ++i)
        {
            string interes = interestsArray[i];
            interes = interes.Trim();
            interes = char.ToUpper(interes[0]) + interes.Substring(1) + "\n";
            interestsText += interes;
        }*/

        int meshSign = 0; 
        for (int i = 0; i < astrologies.Length; i++) {
            if (sign.Equals(astrologies[i])) {
                meshSign = i;
                break;
            }
        }

        astro.mesh = astrologySigns[meshSign];

        switch (color.ToLower())
        {
            /*
            Looks horrible

            case "black":
                astro.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Black");
                break;*/
            case "blue":
                astro.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Blue");
                break;
            case "cyan":
                astro.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Cyan");
                break;
            case "green":
                astro.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Green");
                break;
            case "red":
                astro.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Red");
                break;
            case "magenta":
                astro.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Magenta");
                break;
            default:
                astro.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Black");
                break;
        }

        // Texts
        ime.text = imeText;

        starost.text = ageText;

        userTag.text = tagText;

        opis.text = interests;
    }
}
