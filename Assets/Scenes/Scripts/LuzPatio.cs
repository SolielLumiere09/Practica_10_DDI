using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt.Messages;

public class LuzPatio : Topic
{
    private static Text _text;
    // Start is called before the first frame update

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string msg = Arduino.LastMsg;

        if (msg != null && msg.Contains("Luminocidad"))
        {
            Client.Publish(
                topicName, 
                System.Text.Encoding.UTF8.GetBytes(msg),
                MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        }
    }


    public static void setText(string text)
    {
        _text.text = text;
    }

}
