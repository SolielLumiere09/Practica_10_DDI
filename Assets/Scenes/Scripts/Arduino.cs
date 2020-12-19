using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;

public class Arduino : MonoBehaviour
{
    private static SerialPort _serialPort;
    public string port;
    public int baudRate;
    private static volatile string _lastMsg;
    private volatile Boolean _running = false;

    private Thread _thread;
    // Start is called before the first frame update
    void Start()
    {
        _serialPort = new SerialPort(port);
        _serialPort.BaudRate = baudRate;
        _serialPort.StopBits = StopBits.One;
        _serialPort.Parity = Parity.None;
        _serialPort.DataBits = 8;
        _serialPort.Handshake = Handshake.None;
        _thread = new Thread(UpdatePort);
        
        _serialPort.ReadTimeout = 1000;
        
        _serialPort.Open();

        _running = true;
        _thread.Start();
    }

    // Update is called once per frame
    private void UpdatePort()
    {
        while (_running)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    _lastMsg = _serialPort.ReadLine();
                }
                catch(Exception e)
                {
                    _lastMsg = "NONE";

                }
            }
        }

        Debug.Log("Thread end");
    }

    private void OnApplicationQuit()
    {
        _running = false;
        
    }

    public static void WriteMsg(string msg)
    {
        
        _serialPort.WriteLine(msg);
    }
    
    public static string LastMsg => _lastMsg;
}
