using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;


public class gamePlay : MonoBehaviour 
{
	public Text underlines;
	public Text word_characters;
	public Text notification;
	public Text used_words_ui;

	private StreamReader file_r;
	private StreamWriter file_w;

	private string words_from_file;

	public GameObject canvas;

	private GameObject textual;
	private List<GameObject> alphabets;

	public Image hangman_graphic;

	
	private TextAsset words_file;

	private KeyCode keyPressed;

	private List<string> words;
	private List<string> used_words;
	private string word = "";
	private int count;
	private int winCount = 0;
	private bool gameOver;
	private bool win;

	private DirectoryInfo info;
	private string words_db_path;
	private int result;

	void Awake()
	{
		words = new List<string>();
		alphabets = new List<GameObject>();
		used_words = new List<string>();
		count = 0;
		gameOver = false;
		win = false;

		words_db_path = Application.persistentDataPath+"/Words Database";

		info = new DirectoryInfo(words_db_path);
		if (info.Exists)
		{
			if(File.Exists(words_db_path+"/words.txt"))
			   file_r = File.OpenText(words_db_path+"/words.txt");
			else
			{
				words_file = Resources.Load(@"WordDatabase/words") as TextAsset;
				file_w = File.CreateText(words_db_path+"/words.txt");
				file_w.Write(words_file.text);
				file_w.Close();
				file_r = File.OpenText(words_db_path+"/words.txt");
			}
		}
		else if(!info.Exists)
		{
			info.CreateSubdirectory(words_db_path);

			if(File.Exists(words_db_path+"/words.txt"))
				file_r = File.OpenText(words_db_path+"/words.txt");
			else
			{
				words_file = Resources.Load(@"WordDatabase/words") as TextAsset;
				file_w = File.CreateText(words_db_path+"/words.txt");
				file_w.Write(words_file.text);
				file_w.Close();
				file_r = File.OpenText(words_db_path+"/words.txt");
			}
		}

		words_from_file = file_r.ReadToEnd();

		
		for (int i = 0; i <= words_from_file.Length; i++) 
		{
			if ((i == words_from_file.Length) || (words_from_file[i].ToString() == " "))
			{
				words.Add(word);
				word = "";
				continue;
			}

			
			word = word + words_from_file[i].ToString();
			
		}
		
		foreach (string wordika in words) 
		{
			Debug.Log(wordika);	
		}
	}


	void Start () 
	{

		word = words[Random.Range(0,words.Count)];

		Debug.Log("Word selected: " + word);


		for (int i = 0; i < word.Length; i++) 
		{
			textual = new GameObject();
			textual.name = word[i].ToString();
			textual.AddComponent<Text>();

			textual.transform.SetParent(canvas.transform);

			textual.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0.5f);
			textual.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0.5f);
			textual.GetComponent<RectTransform>().sizeDelta = new Vector2(20,38);
			textual.GetComponent<RectTransform>().anchoredPosition = new Vector2(140+(i*40),0);
			textual.GetComponent<RectTransform>().localScale = new Vector2(1,1);


			textual.GetComponent<Text>().font = Resources.Load("Fonts/cour") as Font;


			textual.GetComponent<Text>().text = "_";
			textual.GetComponent<Text>().color = Color.black;
			textual.GetComponent<Text>().fontStyle = FontStyle.Bold;
			textual.GetComponent<Text>().fontSize = 26;
			alphabets.Add(textual);
		}
	}
	

	void Update () 
	{
		Debug.Log(Application.persistentDataPath);
		if(gameOver == false && win == false && Input.anyKeyDown)
		{

			if(used_words.Contains(Input.inputString))
			{
				notification.text = "'" + Input.inputString.ToUpper() + "' already used.";
			}

			else if(alphabets.Contains(GameObject.Find(Input.inputString)))
			{

			    foreach(GameObject alph in alphabets)
				{
					if(alph.name == Input.inputString)
					{
						alph.GetComponent<Text>().text = alph.name.ToUpper();
						used_words.Add(Input.inputString);
						Debug.Log(used_words[0]);
						winCount += 1;
					}
				}

				if (winCount == word.Length)
					win = true;

			    Debug.Log ("CONTAINING: " + Input.inputString);

			}

			else if(int.TryParse(Input.inputString[0].ToString(), out result))// || (int)Input.inputString > 122) /*|| System.Convert.ToInt32(Input.inputString) > 122*/
			{
					Debug.Log("Result: "+result);
			}

			else if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0))
			{
				Debug.Log("Clicking!");
			}

			

			else
			{
				Debug.Log ("Wrong!");
				used_words_ui.text += Input.inputString.ToUpper() + " - ";
				used_words.Add(Input.inputString);
				count++; 
				hangman_graphic.sprite = Resources.Load<Sprite>("Images/Hangman-"+count.ToString());

				if (count == 6)
					gameOver = true;

			}
		}

		if (gameOver == true || win == true)
			GameOver();

		Debug.Log("Input string: " + Input.inputString);
		Debug.Log("Input string length: " + Input.inputString.Length);

	}

	void GameOver ()
	{
		if(win == true)
			notification.text = "You won! :) :D  --Press ENTER key to restart!";
		else if(gameOver == true)
		{
			notification.text = "You lost! :( :'(  --Press ENTER key to restart!";

			foreach(GameObject alph in alphabets)
			{
					alph.GetComponent<Text>().text = alph.name.ToUpper();
					Debug.Log(used_words[0]);
			}
		}

		if (Input.GetKeyUp(KeyCode.Return) || (Input.GetKeyUp(KeyCode.KeypadEnter)))
			Application.LoadLevel(0);

	}
}




/*
 * 

 * */




/* -------------------------------------------- Backup ------------------------------------------
 * 
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class gamePlay : MonoBehaviour 
{
	public Text underlines;
	public Text word_characters;
	public Text notification;
	public Text used_words_ui;
	
	public GameObject canvas;
	
	private GameObject textual;
	private List<GameObject> alphabets;
	
	public Image hangman_graphic;
	
	private TextAsset words_file;
	//private Text alphabets;
	private KeyCode keyPressed;
	
	private List<string> words;
	private List<string> used_words;
	private string word = null;
	private int count;
	private int winCount = 0;
	private bool gameOver;
	private bool win;
	
	
	
	void Awake()
	{
		words = new List<string>();
		alphabets = new List<GameObject>();
		used_words = new List<string>();
		count = 0;
		gameOver = false;
		win = false;
		
		words_file = AssetDatabase.LoadAssetAtPath<TextAsset>(@"Assets/Word Database/words.txt");
		
		
		for (int i = 0; i <= words_file.text.Length; i++) 
		{
			if ((i == words_file.text.Length) || (words_file.text[i].ToString() == " "))
			{
				words.Add(word);
				word = "";
				continue;
				winCount += 1;
			}
			
			
			word = word + words_file.text[i].ToString();
			
		}
		
		foreach (string wordika in words) 
		{
			Debug.Log(wordika);	
		}
	}
	
	
	void Start () 
	{
		
		//select random word
		word = words[Random.Range(0,words.Count)];
		/*	for (int i = 0; i < word.Length; i++) 
		{
			word_characters.text += "- ";
		}

		Debug.Log("Word selected: " + word);
		
		
		for (int i = 0; i < word.Length; i++) 
		{
			textual = new GameObject();
			textual.name = word[i].ToString();
			textual.AddComponent<Text>();
			
			textual.transform.SetParent(canvas.transform);
			
			textual.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0.5f);
			textual.GetComponent<RectTransform>().anchorMax = new Vector2(0f,0.5f);
			textual.GetComponent<RectTransform>().sizeDelta = new Vector2(13,30);
			textual.GetComponent<RectTransform>().anchoredPosition = new Vector2(86+(i*20),0);
			textual.GetComponent<RectTransform>().localScale = new Vector2(1,1);
			
			textual.GetComponent<Text>().font = AssetDatabase.LoadAssetAtPath<Font>("Assets/Fonts/cour.ttf");
			textual.GetComponent<Text>().text = "_";
			textual.GetComponent<Text>().color = Color.black;
			alphabets.Add(textual);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameOver == false && win == false && Input.anyKeyDown)
		{
			
			if(used_words.Contains(Input.inputString))
			{
				notification.text = "'" + Input.inputString.ToUpper() + "' already used.";
			}
			
			else if(alphabets.Contains(GameObject.Find(Input.inputString)))
			{
				
				foreach(GameObject alph in alphabets)
				{
					if(alph.name == Input.inputString)
					{
						alph.GetComponent<Text>().text = alph.name.ToUpper();
						used_words.Add(Input.inputString);
						Debug.Log(used_words[0]);
						winCount += 1;
					}
				}
				if (winCount == word.Length)
					win = true;
				
				Debug.Log ("CONTAINING: " + Input.inputString);
			}
			
				else if (!alphabets.Contains(GameObject.Find(Input.inputString)))
			{
				Debug.Log ("Wrong!");

				used_words.Add(Input.inputString);
			}
						else if((Input.inputString == " ") || (Input.inputString == KeyCode.Space.ToString()))
				Debug.Log ("Space pressed!");

			else if(System.Convert.ToInt32(Input.inputString[0]) < 97 || System.Convert.ToInt32(Input.inputString[0]) > 122)
			{
				Debug.Log("Unknown!!");
			}
			
			else
			{
				Debug.Log ("Wrong!");
				used_words_ui.text += Input.inputString.ToUpper() + "-";
				used_words.Add(Input.inputString);
				count++; 
				
				hangman_graphic.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Hangman-"+count.ToString()+".png");
				
				if (count == 6)
				{
					gameOver = true;
					
				}
				
			}
			
			
		}
		
		if (gameOver == true || win == true)
		{
			GameOver();
			
		}
		
		//if (win == true)
		//	GameOver();
		
		
	}
	
	void GameOver ()
	{
		if(win == true)
			notification.text = "You won! :) :D  --Press ENTER key to restart!";
		else if(gameOver == true)
		{
			notification.text = "You lost! :( :'(  --Press ENTER key to restart!";
			
			foreach(GameObject alph in alphabets)
			{
				alph.GetComponent<Text>().text = alph.name.ToUpper();
				Debug.Log(used_words[0]);
			}
		}
		
		if (Input.GetKeyUp(KeyCode.Return) || (Input.GetKeyUp(KeyCode.KeypadEnter)))
			Application.LoadLevel(0);
		
	}
}




			if (word.Contains(Input.inputString))
			{
				Debug.Log("Pressing: " + Input.inputString);


				word_characters.text = word_characters.text.Remove(word.IndexOf(Input.inputString));
				word_characters.text = word_characters.text.Insert(word.IndexOf(Input.inputString),Input.inputString);

*/