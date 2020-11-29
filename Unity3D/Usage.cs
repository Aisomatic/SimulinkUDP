using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usage : MonoBehaviour
{
    private UdpConnection connection;
 
    void Start()
    {
        string sendIp = "127.0.0.1";
        int sendPort = 25001;
        int receivePort = 25002;
 
        connection = new UdpConnection();
        connection.StartConnection(sendIp, sendPort, receivePort);
    }
 
    void Update()
    {
        foreach (var message in connection.getMessages()) Debug.Log(message);
 
        connection.Send("Hi!");
    }
 
    void OnDestroy()
    {
        connection.Stop();
    }
}
