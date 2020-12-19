using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class GreenLed : Topic
{
    
    public void ToogleGreenLed()
    {
        
        Client.Publish(topicName, 
            System.Text.Encoding.UTF8.GetBytes("LED_GREEN"),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

    }
}
