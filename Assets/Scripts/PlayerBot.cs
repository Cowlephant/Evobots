using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBot : MonoBehaviour {

    GameObject chassis;
    Rigidbody botRigidBody;
    List<GameObject> wheels = new List<GameObject>();

	// Use this for initialization
    void Start()
    {
        Invoke("Spawn", 0.5F); // Spawn our bot after half a second
	}

    void Spawn() // Create a randomly sized box chassis and drop it in
    {
        chassis = GameObject.CreatePrimitive(PrimitiveType.Cube); // Set chassis to a cube
        chassis.transform.parent = this.gameObject.transform;
        chassis.layer = 8; // Set to PlayerBotChassis layer
        chassis.collider.material = (PhysicMaterial)Resources.Load("Materials/Metal"); // Set physics material to Metal
        chassis.transform.localScale = new Vector3(Random.Range(0.3F, 0.9F), Random.Range(0.3F, 0.8F), Random.Range(0.4F, 1.3F)); // Scale it randomly, tending toward a rectangular prism
        this.gameObject.transform.position = new Vector3(0, chassis.transform.localScale.y / 2 + 2F, 0); // Position bot 2 units above the ground
        botRigidBody = (Rigidbody)this.gameObject.GetComponent(typeof(Rigidbody));
        botRigidBody.mass = chassis.transform.localScale.x * chassis.transform.localScale.y * chassis.transform.localScale.z; // Set the mass to the volume
        AddWheels();
    }

    void AddWheels()
    {
        int numberOfWheels = 5;
        int wheelCount = 0;
        int wheelAttempts = 0;
        RaycastHit hit;
        Object wheel = Resources.Load("Prefabs/Wheel"); // Load wheel prefab
        while (wheelCount < numberOfWheels)
        {
            if (Physics.Linecast(this.gameObject.transform.position + Random.onUnitSphere * 2, this.gameObject.transform.position, out hit, 1 << 8)) // Raycast toward center to pick random spot on surface
            {
                GameObject thisWheel = (GameObject)GameObject.Instantiate(wheel, hit.point, Quaternion.identity); // Instantiate a wheel
                thisWheel.name = "Wheel_" + (wheelCount + 1); // Name it
                float wheelDiameter = Random.Range(0.2F, 0.3F); // Random diameter
                float wheelWidth = Random.Range(0.04F, 0.1F); // Random width
                thisWheel.transform.localScale = new Vector3(wheelDiameter, wheelDiameter, wheelWidth); // Apply width and diameter
                thisWheel.transform.LookAt(hit.point + hit.normal); // Align to face
                float wheelDistance = (wheelWidth / 2) + 0.02F; // Define distance from chassis
                thisWheel.transform.position = thisWheel.transform.position + hit.normal * wheelDistance; // Set distance
                // If wheel is colliding with another wheel, delete it and try again
                if (Physics.CheckSphere(thisWheel.transform.position, wheelDiameter, 1 << 9))
                {
                    Object.Destroy(thisWheel);
                    continue;
                }
                thisWheel.layer = 9;
                HingeJoint thisJoint = (HingeJoint)thisWheel.AddComponent<HingeJoint>(); // Create a hinge joint
                thisJoint.axis = new Vector3(0, 0, -1); // Set joint axis (towards chassis)
                thisJoint.anchor = new Vector3(0, 0, 0); // Set anchor to center of wheel // TODO: Change this to inside face of wheel
                thisJoint.connectedBody = this.gameObject.GetComponent<Rigidbody>(); // Connect to rigid body
                wheels.Add(thisWheel); // Add to wheels array
                wheelCount++; // Increment wheel count
            }
            wheelAttempts++;
            if (wheelAttempts > 1000) break;
        }

        botRigidBody.useGravity = true; // Activate gravity for bot (starts disabled)
        //Time.timeScale = 0; // Stop time
    }
	
	// FixedUpdate is called once per step
    void FixedUpdate()
    {

	}
}
