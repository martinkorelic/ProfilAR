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

    // Start is called before the first frame update
    void Start()
    {
        SetupCamera();
        textOut.text = CardDataController.QRText;
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
                cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
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
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if (result != null)
            {
                textOut.text = result.Text;
                CardDataController.QRText = result.Text;
            }

            else textOut.text = "Failed to read QR Code";
        } catch
        {
            textOut.text = "Failed in try";
        }
    }

    public void OncClickScan()
    {
        Scan();
    }

    public void Back()
    {
        cameraTexture.Stop();
        SceneManager.LoadScene("StartScene");
    }

}
