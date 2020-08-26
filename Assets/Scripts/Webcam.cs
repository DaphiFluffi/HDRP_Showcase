using System.Collections;
using UnityEngine;

public class Webcam : MonoBehaviour
{
    IEnumerator Start()
    {
        findWebCams();

        //asks the user if the application is allowed to access the webcam 
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //find webcam after authorization
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.Log("webcam found");
        }
        else
        {
            Debug.Log("webcam not found");
        }
        WebCamTexture webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    void findWebCams()
    {
        foreach (var device in WebCamTexture.devices)
        {
            Debug.Log("Name: " + device.name);
        }
    }
}
