using UnityEngine;
using System.Collections;

public class PlayerBot : MonoBehaviour {

    GameObject chassis;

	// Use this for initialization
    void Start()
    {
        Invoke("Spawn", 1F); // Spawn our bot after 1 second
	}

    void Spawn() // Create a randomly sized box chassis and drop it in
    {
        chassis = GameObject.CreatePrimitive(PrimitiveType.Cube); // Set chassis to a cube
        chassis.collider.material = (PhysicMaterial)Resources.Load("Materials/Metal"); // Set physics material to Metal
        chassis.transform.localScale = new Vector3(Random.Range(0.3F, 1F), Random.Range(0.3F, 1F), Random.Range(0.3F, 1F)); // Scale it randomly between 0.3 and 1 WxHxD
        chassis.transform.position = new Vector3(0, chassis.transform.localScale.y / 2 + 0.5F, 0); // Position it 0.5 units above the ground
        Rigidbody chassisRigidBody = chassis.AddComponent<Rigidbody>(); // Add the rigid body
        chassisRigidBody.mass = chassis.transform.localScale.x * chassis.transform.localScale.y * chassis.transform.localScale.z; // Set the mass to the volume
    }
	
	// Update is called once per frame
	void Update () {

	}
}
