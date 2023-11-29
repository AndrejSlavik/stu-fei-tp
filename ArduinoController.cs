using UnityEngine;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public class ArduinoController : MonoBehaviour
{
    public string portName = "COM7";  // Change this to your Arduino's port
    public int baudRate = 9600;

    SerialPort serialPort;

    public float Xinput { get; private set; }
    public float Yinput { get; private set; }
    public float Zinput { get; private set; }

    public float moveSpeed = 300f;

    public string[] datas;

    public string data;

    void Start()
    {
        OpenConnection();
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                // Read the data from the serial port
                data = serialPort.ReadLine();

                // Debug log to see received data in the Unity console
                Debug.Log("Received data: " + data);

                // Split the received data by commas
                string[] datas = data.Split(',');

                // Check if there are enough elements in the array
                if (datas.Length == 3)
                {
                    // Attempt to parse the values as floats
                    if (float.TryParse(datas[0], out float x) &&
                        float.TryParse(datas[1], out float y) &&
                        float.TryParse(datas[2], out float z))
                    {
                        Xinput = x;
                        Yinput = y;
                        Zinput = z;
                    }
                    else
                    {
                        Debug.LogError("Failed to parse values as float");
                    }
                }
                else
                {
                    Debug.LogError("Invalid data structure");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading data: " + e.Message);
            }
        }

        Vector2 movement = new Vector2(Xinput, Yinput);

        // Normalize the movement vector to ensure consistent speed in all directions
        movement.Normalize();

        // Update the position of the sprite based on input and speed
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }


    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    void OpenConnection()
    {
        serialPort = new SerialPort(portName, baudRate);

        try
        {
            serialPort.Open();
            Debug.Log("Serial port opened successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error opening serial port: " + e.Message);
        }
    }
}
