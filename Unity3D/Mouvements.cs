using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Mouvements : MonoBehaviour{

	public GameObject Cube, Capsule, Sphere;
	private UdpConnection connection;
	public Text q1, q2, q3, q4;
	public int i=1, Imax = 100;
	public float X, Y, Z, Theta;

    void Start() {
        string sendIp = "127.0.0.1";
        int sendPort = 25005;
        int receivePort = 25002;
 
        connection = new UdpConnection();
        connection.StartConnection(sendIp, sendPort, receivePort);
    }

    void Update() {
        foreach (var message in connection.getMessages()){
    		// Debug.Log(message);
    		float[] Data = Array.ConvertAll(message.Split(' '), float.Parse);
    		//Debug.Log(Data[0]);

    		Cube.transform.localScale = new Vector3(1, 2+Data[0], 1);
    		Sphere.transform.position = new Vector3(0, 0, Data[1]);
    		//Capsule.transform.position = new Vector3(3+Data[3], 0, 0);
    		Capsule.transform.eulerAngles = new Vector3(90*Data[3], 0, 0);

    		if(i%Imax==0){
    			q1.text = Data[0].ToString();
	    		q2.text = Data[1].ToString();
	    		q3.text = Data[2].ToString();
	    		q4.text = Data[3].ToString();
	    		i=0;
	    		X = UnityEngine.Random.Range(-10.0f, 10.0f);
		    	Y = UnityEngine.Random.Range(-10.0f, 10.0f);
		    	Z = UnityEngine.Random.Range(-10.0f, 10.0f);
		    	Theta = UnityEngine.Random.Range(-10.0f, 10.0f);
	    		connection.Send(X.ToString()+ " " + Y.ToString()+ " " + Z.ToString()+ " " + Theta.ToString());
	    		Debug.Log(X.ToString()+ " " + Y.ToString()+ " " + Z.ToString()+ " " + Theta.ToString());
    		}
    		i++;
    	} 
    }

    void OnDestroy() {
        connection.Stop();
    }
}
