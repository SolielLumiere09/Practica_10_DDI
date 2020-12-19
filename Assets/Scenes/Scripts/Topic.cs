using System;
using System.Net;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;

public class Topic : MonoBehaviour
{
    public string brokerIpAddress = "127.0.0.1";
    public int brokerPort = 1883;
    public string topicName;
    protected MqttClient Client;
    private string _lastMessage;
    
    // Start is called before the first frame update
    void Awake()
    {
        Client = new MqttClient(IPAddress.Parse(brokerIpAddress), brokerPort, false, null);
        string clientId = Guid.NewGuid().ToString(); 
		
        Client.Connect(clientId); 
        
    }
    
    protected void OnApplicationQuit()
    {
        Debug.Log("Disconected");
        Client.Disconnect();
    }
}
