using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    GameObject Camera;
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
    }
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Write()
    {
        Camera.GetComponent<ArduinoSerialCommunication>().serial.WriteLine("Alive!");
    }
}
