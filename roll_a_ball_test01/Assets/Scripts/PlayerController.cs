using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;



	// Use this for initialization
	void Start () 
	{
	
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText(); 


	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate()
	{
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVer = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHor, 0.0f, moveVer);

		rb.AddForce (movement * speed);
	}



	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count++;
			SetCountText();
		}

	}


	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12)
			winText.text = "You WIN!!!";
		else
			winText.text = "";
	}

}



