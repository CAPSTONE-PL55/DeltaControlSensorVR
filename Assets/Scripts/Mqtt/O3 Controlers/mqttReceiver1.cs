using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using System.Text.RegularExpressions;

[System.Serializable]
public class JsonClass
{
    public double Present_Value { get; set; }
    public string Units { get; set; }
    public string updated { get; set; }
    public int status { get; set; }

    public static JsonClass CreateFromJson(string json)
    {
        return JsonUtility.FromJson<JsonClass>(json);
    }
}

// could potentially add this hubreading in the base M2MqttUnityClient class if all hubs have same defined readings
public struct HubReading
{
    public double temperature;
    public double humidity;
    public double light;
    public double sound;
    public double occupancy;

    public HubReading(double temperature, double humidity, double light, double sound, double occupancy)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.light = light;
        this.sound = sound;
        this.occupancy = occupancy;
    }
}

[System.Serializable]
public class mqttReceiver1 : M2MqttUnityClient
{
    [Header("MQTT topics")]
    [Tooltip("Set the topic to subscribe. !!!ATTENTION!!! multi-level wildcard # subscribes to all topics")]
    // could also potentially move this topic list to base class as well
    public string[] topics = {
        "events/object/irTemperature",
        "events/object/occupantHumidity",
        "events/object/lightLevel",
        "events/object/soundLevel",
        "events/object/combinedOccupancy"
    };
    public string topicSubscribe = "test1"; // topic to subscribe. !!! The multi-level wildcard # is used to subscribe to all the topics. Attention i if #, subscribe to all topics. Attention if MQTT is on data plan
    [Tooltip("Set the topic to publish (optional)")]
    public string topicPublish = ""; // topic to publish
    public string messagePublish = ""; // message to publish

    [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
    public bool autoTest = false;

    //using C# Property GET/SET and event listener to reduce Update overhead in the controlled objects
    private string m_msg;

    public string msg
    {
        get
        {
            return m_msg;
        }
        set
        {
            if (m_msg == value) return;
            m_msg = value;
            if (OnMessageArrived != null)
            {
                OnMessageArrived(m_msg);
            }
        }
    }

    public event OnMessageArrivedDelegate OnMessageArrived;
    public delegate void OnMessageArrivedDelegate(string newMsg);

    //using C# Property GET/SET and event listener to expose the connection status
    private bool m_isConnected;

    public bool isConnected
    {
        get
        {
            return m_isConnected;
        }
        set
        {
            if (m_isConnected == value) return;
            m_isConnected = value;
            if (OnConnectionSucceeded != null)
            {
                OnConnectionSucceeded(isConnected);
            }
        }
    }
    public event OnConnectionSucceededDelegate OnConnectionSucceeded;
    public delegate void OnConnectionSucceededDelegate(bool isConnected);

    // a list to store the messages
    private List<string> eventMessages = new List<string>();

    private HubReading readings = new HubReading(0, 0, 0, 0, 0);

    public void Publish()
    {
        client.Publish(topicPublish, System.Text.Encoding.UTF8.GetBytes(messagePublish), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }

    public void SetEncrypted(bool isEncrypted)
    {
        this.isEncrypted = isEncrypted;
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        isConnected = true;

        if (autoTest)
        {
            Publish();
        }
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("CONNECTION FAILED! " + errorMessage);
    }

    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected.");
        isConnected = false;
    }

    protected override void OnConnectionLost()
    {
        Debug.Log("CONNECTION LOST!");
    }

    protected override void SubscribeTopics()
    {
        foreach (string topic in topics)
        {
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
    }

    protected override void UnsubscribeTopics()
    {
        foreach (string topic in topics)
        {
            client.Unsubscribe(new string[] { topic });
        }
    }

    protected override void Start()
    {
        base.Start();
    }
    
    [System.Serializable]
    public class SensorObject
    {
        public float Present_Value;
        public string Units;
        public string updated;
        public int status;

    }


    protected override void DecodeMessage(string topic, byte[] message)
    {
       
        string jsonString = System.Text.Encoding.Default.GetString(message);
        SensorObject reading = JsonUtility.FromJson<SensorObject>(jsonString);
        double presentValue = reading.Present_Value;

        Debug.Log($"Json string: {jsonString} | Current Value: {presentValue}");
        


        switch (topic)
        {
            case "events/object/irTemperature":
                readings.temperature = presentValue;
                break;
            case "events/object/occupantHumidity":
                readings.humidity = presentValue;
                break;
            case "events/object/lightLevel":
                readings.light = presentValue;
                break;
            case "events/object/soundLevel":
                readings.sound = presentValue;
                break;
            case "events/object/combinedOccupancy":
                readings.occupancy = presentValue;
                break;
            default:
                break;
        }

        msg = string.Format("Temp: {0} \n" + "Humidity: {1} \n" + "Light: {2} \n" + "Sound: {3} \n" + "Occupancy: {4} \n",
            readings.temperature.ToString(),
            readings.humidity.ToString(),
            readings.light.ToString(),
            readings.sound.ToString(),
            readings.occupancy.ToString());

        StoreMessage(msg);
    }

    private void StoreMessage(string eventMsg)
    {
        if (eventMessages.Count > 50)
        {
            eventMessages.Clear();
        }
        eventMessages.Add(eventMsg);
    }

    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()

    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
        if (autoTest)
        {
            autoConnect = true;
        }
    }


}
