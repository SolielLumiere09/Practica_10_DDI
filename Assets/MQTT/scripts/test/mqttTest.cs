using UnityEngine;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using UnityEngine.UI;

using System;
using UnityEngine.Serialization;

public class mqttTest : MonoBehaviour {
	public string brokerIpAddress = "192.168.0.113";
	public int brokerPort = 5001;
	private MqttClient client;
	string lastMessage;
	// Use this for initialization

	//custom topisc
	public string arduinoLuzPatio;
	private volatile Boolean isArduinoLuzPatio;

	public string arduinoTemperaturaCasa;
	private volatile Boolean isArduinoTemperaturaCasa;

	
	public string arduinoLuzRojo;
	public string arduinoLuzInterior;
	public string arduinoLuzVerde;
	public string arduinoLuzBlanca;
	public string arduinoMotor;

	
	void Start () {
		client = new MqttClient(IPAddress.Parse(brokerIpAddress), brokerPort, false, null);
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString(); 
		
		client.Connect(clientId);
		client.Subscribe(new string[] { arduinoLuzPatio }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });  
		client.Subscribe(new string[] { arduinoLuzRojo }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });  
		client.Subscribe(new string[] { arduinoLuzBlanca }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe(new string[] { arduinoLuzVerde }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });  
		client.Subscribe(new string[] { arduinoMotor }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
		client.Subscribe(new string[] { arduinoLuzInterior }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });  
		client.Subscribe(new string[] { arduinoTemperaturaCasa }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });  

		
	}
	void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 
		//Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
		lastMessage = System.Text.Encoding.UTF8.GetString(e.Message);
		
		if (e.Topic.Equals(arduinoLuzPatio))
		{
			isArduinoLuzPatio = true;

		}

		if (e.Topic.Equals(arduinoLuzRojo))
		{
			Arduino.WriteMsg(lastMessage);
			
		}
		if (e.Topic.Equals(arduinoLuzBlanca))
		{
			
			Arduino.WriteMsg(lastMessage);
		}

		if (e.Topic.Equals(arduinoLuzVerde))
		{
			Arduino.WriteMsg(lastMessage);
			
		}

		if (e.Topic.Equals(arduinoLuzInterior))
		{
			
			Arduino.WriteMsg(lastMessage);
		}
		
		if (e.Topic.Equals(arduinoMotor))
		{
			Arduino.WriteMsg(lastMessage);
		}

		if (e.Topic.Equals(arduinoTemperaturaCasa))
		{
			isArduinoTemperaturaCasa = true;
			
		}

	}

	void Update()
	{
		if (isArduinoLuzPatio)
		{
			LuzPatio.setText(lastMessage);
			isArduinoLuzPatio = false;
		}

		if (isArduinoTemperaturaCasa)
		{
			Temperatura.setText(lastMessage);
			isArduinoTemperaturaCasa = false;
		}

	}
	

	void OnApplicationQuit()
	{
		client.Disconnect();
	}
}
