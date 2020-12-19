using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

public class LuzInterior : Topic
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public void OnClickButton()
    {
        Client.Publish(topicName, 
            System.Text.Encoding.UTF8.GetBytes("ROOM_LIGHT"),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

        
    }
}
