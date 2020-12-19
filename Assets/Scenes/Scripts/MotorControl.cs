using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt.Messages;

public class MotorControl : Topic
{

    public Slider motorSpeed;
    
    public void OnClick()
    {
        int dutyCicle = (int)motorSpeed.value;
        string command = "MOTOR_SPEED " + dutyCicle;
        
        Client.Publish(topicName, 
            System.Text.Encoding.UTF8.GetBytes(command),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);

    }
}
