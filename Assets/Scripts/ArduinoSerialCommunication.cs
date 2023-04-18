using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Linq;
using System;

public class ArduinoSerialCommunication : MonoBehaviour
{
    GameObject player;
    GameObject Camera;
    public GameObject UI;
    public Movement movement;
    public string portname;
    public SerialPort serial;
    public bool jumping;
    public bool speeding;
    public bool swapping;
    bool stop = false;
    bool stop2 = false;
    string[] ports = SerialPort.GetPortNames();
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        player = GameObject.Find("player");
        movement = player.GetComponent<Movement>();
        serial = new SerialPort();

        List<string> portOptions = new List<string>();


        for (int i = 0; i < ports.Length; ++i)
        {
            Debug.Log(ports[i]);
        }
        if (ports.Length > 0)
        {
            serial.BaudRate = 115200;
            serial.DataBits = 8;
            serial.Parity = Parity.None;
            serial.StopBits = StopBits.One;
            serial.PortName = ports[0];
            for (int i = 0; i < 4; ++i)
            {
                try
                {
                    serial.Open();

                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }            
        }
    }

    void Update()
    {
        if (player.transform.position.y < -7f && Camera.GetComponent<PlatformSpawn>().enabled == true && stop2 == false)
        {
            Camera.GetComponent<PlatformSpawn>().enabled = false;
            Instantiate(UI, new Vector3(player.transform.position.x, 0, 0), Quaternion.identity);
            player.GetComponent<Movement>().enabled = false;
            serial.WriteLine("Dead!");
            stop2 = true;
        }
        if (serial.IsOpen && serial.BytesToRead > 0)
        {
            string input = serial.ReadLine();
            Debug.Log(input);
            if (input == "jump")
            {
                jumping = true;
            }
            if (input == "jump not")
            {
                jumping = false;
            }
            if (input == "speed")
            {
                speeding = true;
            }
            if (input == "not speed")
            {
                speeding = false;
            }
            if (input == "swap")
            {
                swapping = true;
            }
            if (input == "swap not")
            {
                swapping = false;
            }
        }

        if (player.GetComponent<Movement>().isGrounded == false && stop == false)
        {
            serial.WriteLine("True");
            stop = true;
        }
        else if (stop == true && player.GetComponent<Movement>().isGrounded == true)
        {
            stop = false;
            serial.WriteLine("False");
        }
    }
}