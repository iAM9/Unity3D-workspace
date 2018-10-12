using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {


    private Vector3 reflected_vec;

    private Vector3 initial_vel;
    public float speed;

    public Camera mainCamera;

	void Start () 
    {
        gameObject.GetComponent<Rigidbody>().AddForce(speed, gameObject.transform.position.y, speed, ForceMode.VelocityChange);
        initial_vel = new Vector3(speed, gameObject.transform.position.y, speed);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        //Debug.Log("X: " + transform.position.x);
        //Debug.Log("Z: " + transform.position.z);
        //reflected_vec = Vector3.Reflect(gameObject.transform.position, Vector3.right);
        
        //Debug.DrawRay(gameObject.transform.position, -gameObject.transform.position.normalized);
	}

    void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, mainCamera.transform.position.y, gameObject.transform.position.z - 5);

    }
    void OnCollisionEnter(Collision col)
    {
        reflected_vec = Vector3.Reflect(initial_vel.normalized, col.contacts[0].normal);
        
        gameObject.GetComponent<Rigidbody>().AddForce(reflected_vec * speed, ForceMode.VelocityChange);

        //Debug.Log("Cntct pt: " + col.contacts[0].point);
        //Debug.Log("CntctptN: " + col.contacts[0].normal);
        Debug.Log("GO vel  : " + initial_vel); //gameObject.GetComponent<Rigidbody>().velocity);
        Debug.Log("GO vel_n: " + initial_vel.normalized);//.GetComponent<Rigidbody>().velocity.normalized);
        

       
        initial_vel = reflected_vec * speed;
        Debug.Log("Refl vel: " + initial_vel);
    }
  
}













/*

// Vector3 temp1 = new Vector3(gameObject.transform.position.normalized.x, gameObject.transform.position.y, gameObject.transform.position.normalized.z);

        //Vector3 temp2 = new Vector3(col.contacts[0].point.normalized.x, 0, col.contacts[0].point.normalized.z);

        reflected_vec = Vector3.Reflect(gameObject.GetComponent<Rigidbody>().velocity.normalized, col.contacts[0].normal);

        //Debug.DrawLine(temp2, new Vector3(0,gameObject.transform.position.y,0), Color.green);

        //Debug.DrawLine(temp2, reflected_vec, Color.red);

        Debug.Log(reflected_vec);
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.magnitude);


        gameObject.GetComponent<Rigidbody>().AddForce(reflected_vec * speed, ForceMode.Impulse);


*/





//Debug.DrawRay(col.contacts[0].point, Vector3.zero);
/*
if (col.gameObject.name == "top")
{
    reflected_vec = Vector3.Reflect(transform.position.normalized, Vector3.forward);
            
   // gameObject.GetComponent<Rigidbody>().AddForce(reflected_vec * speed, ForceMode.VelocityChange);

    //Debug.DrawRay(gameObject.transform.position, reflected_vec * speed);
}

if (col.gameObject.name == "bottom")
{
    reflected_vec = Vector3.Reflect(transform.position.normalized, Vector3.back);
    //gameObject.GetComponent<Rigidbody>().AddForce(reflected_vec * speed, ForceMode.VelocityChange);
    //Debug.DrawRay(gameObject.transform.position, reflected_vec * speed);
}

//gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(transform.position.x, transform.position.y, -transform.position.z), ForceMode.VelocityChange);

if (col.gameObject.name == "right")
{
    reflected_vec = Vector3.Reflect(transform.position.normalized, Vector3.right);
   // gameObject.GetComponent<Rigidbody>().AddForce(reflected_vec * speed, ForceMode.VelocityChange);
   // Debug.DrawRay(gameObject.transform.position, reflected_vec * speed);
}

if (col.gameObject.name == "left")
{
    reflected_vec = Vector3.Reflect(transform.position.normalized, Vector3.left);
    //gameObject.GetComponent<Rigidbody>().AddForce(reflected_vec * speed, ForceMode.VelocityChange);
    //Debug.DrawRay(gameObject.transform.position, reflected_vec * speed);
}


//gameObject.GetComponent<Rigidbody>().AddForce(reflected_vec * speed, ForceMode.VelocityChange);

//gameObject.GetComponent<Rigidbody>().velocity = reflected_vec * speed;

Debug.DrawRay(gameObject.transform.position, reflected_vec * speed * speed);
//if (col.gameObject.name == "right" || col.gameObject.name == "left")
// gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-transform.position.x, transform.position.y, transform.position.z), ForceMode.VelocityChange);
*/