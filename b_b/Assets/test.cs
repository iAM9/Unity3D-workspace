using UnityEngine;
using System.Collections;

public class test : MonoBehaviour 
{

    private GameObject sphere;
    private GameObject cube;

    private Vector3 reflection;
	// Use this for initialization
	void Start () 
    {
        sphere = GameObject.Find("s");
        cube = GameObject.Find("c");
	}
	
	// Update is called once per frame
	void Update () 
    {
        reflection = Vector3.Reflect(cube.transform.position, Vector3.right);
        sphere.transform.position = reflection;
        Debug.Log("Reflection    : " + reflection);
        Debug.Log("Normalized dir: " + reflection.normalized);
        Debug.DrawRay(cube.transform.position, -cube.transform.position.normalized);
        Debug.DrawRay(Vector3.zero, reflection.normalized);
	}
}
