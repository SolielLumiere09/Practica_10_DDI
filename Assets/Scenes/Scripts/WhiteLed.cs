using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class WhiteLed : Topic
{
    
    public void ToogleWhiteLed()
    {
        
        Client.Publish(topicName, 
            System.Text.Encoding.UTF8.GetBytes("LED_WHITE"),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

    }
}
