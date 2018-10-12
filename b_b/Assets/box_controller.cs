using UnityEngine;
using System.Collections;

public class box_controller : MonoBehaviour 
{

    public float speed = 10f;
    private float pos = 0f;

    private Vector3 position;

	// Use this for initialization
	void Start () 
    {
        position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
        pos = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //transform.position = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        transform.Translate(pos,0,0);
	}
}
