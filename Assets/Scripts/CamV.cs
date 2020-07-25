using UnityEngine;
using UnityEngine.UI;
public class CamV : MonoBehaviour
{

    static WebCamTexture backCam;
    RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing) {

                if (backCam == null)
                    backCam = new WebCamTexture(devices[i].name);

                rawImage.texture = backCam;

                if (!backCam.isPlaying)
                    backCam.Play();
            }
        }

       

    }
}

