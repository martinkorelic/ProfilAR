using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().color = Color.black;
    }

    void OnMouseEnter() {
        GetComponent<TextMesh>().color = Color.red;
    }

    void OnMouseExit() {
        GetComponent<TextMesh>().color = Color.black;
    }
}
