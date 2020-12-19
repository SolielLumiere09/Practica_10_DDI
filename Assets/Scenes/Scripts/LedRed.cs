using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class LedRed : Topic
{
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    public void toogleRedLed()
    {
        
        Client.Publish(topicName, 
            System.Text.Encoding.UTF8.GetBytes("LED_RED"),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

    }

}
