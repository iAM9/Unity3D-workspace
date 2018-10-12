using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePlay : MonoBehaviour 
{
	private int numberSelected;
	public Text inputField;
	public Text output;

	// Use this for initialization
	void Start () 
	{
		numberSelected = Randomizer();
		Debug.Log("Number selected: " + numberSelected);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.Return) || (Input.GetKeyUp(KeyCode.KeypadEnter)))
		{
			if (inputField.text == numberSelected.ToString())
				output.text = "Correct!";
			if (int.Parse(inputField.text) > numberSelected)
				output.text = "No... the number is lower than that.";
			if (int.Parse(inputField.text) < numberSelected)
				output.text = "No... it's a bigger number.";
		}
	}

	private int Randomizer()
	{
		return Random.Range(1,99);
	}
}
