using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImageBackground;

    [SerializeField]
    private AspectRatioFitter aspectRatioFitter;

    [SerializeField]
    private TextMeshProUGUI textOut;

    [SerializeField]
    private RectTransform scanZone;

    private bool isCamAvailable;
    private WebCamTexture cameraTexture;

    private bool isMobileCamera;

    // Start is called before the first frame update
    void Start()
    {
        SetupCamera();
        textOut.text = CardDataController.QRText;
        InvokeRepeating("Scan", 1f, 1f);  //1s delay, repeat every 1s
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }

    private void SetupCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            isCamAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; ++i)
        {
            //Debug.Log("" + devices[i].name);
            if (!devices[i].name.ToLower().Contains("virtual"))
                cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
            if (devices[i].isFrontFacing == false)
            {
                isMobileCamera = true;
                cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
                break;
            }
        }
        //Debug.Log("Camera texture name:" + cameraTexture.name);
        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvailable = true;
    }

    private void UpdateCameraRender()
    {
        if (isCamAvailable == false) return;
        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

        if (isMobileCamera) {
            rawImageBackground.rectTransform.localScale = new Vector3(1, -1, 1);
        }
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if (result != null)
            {
                Debug.Log("Got em");
                //textOut.text = result.Text;
                CardDataController.QRText = result.Text;
                SceneManager.LoadScene("SampleScene");
            }

            else {
                Debug.Log("No data detected");
                //textOut.text = "Failed to read QR Code";   
            }
        } catch (Exception e)
        {
            Debug.Log(e.Message);
            textOut.text = "Unexpected error occured";
        }
    }
}
