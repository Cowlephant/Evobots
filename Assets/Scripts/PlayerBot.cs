using UnityEngine;
using System.Collections;

public class PlayerBot : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        GameObject chassis = GameObject.CreatePrimitive(PrimitiveType.Cube); // Set chassis to a cube
        chassis.transform.localScale = new Vector3(Random.Range(4, 11), Random.Range(4, 11), Random.Range(4, 11)); // Scale it randomly between 4 and 11 WxHxD
        chassis.transform.position = new Vector3(0, chassis.transform.localScale.y/2 + 5, 0); // Position it 5 units above the ground
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
