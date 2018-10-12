using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class gameMaster : MonoBehaviour 
{
	private int turn;
    private RaycastHit mouseClickHit;
    private Ray ray;

	private string[] board;
	private string[] board_nos;

	private string[] board_temp;

	private string winning_string1 = "xxx";
	private string winning_string2 = "ooo";

	private int[] turns;

	private int pickAposition = -1;

	private string[] board_nos_temp;
	private string[] board_ai_temp;

	private List<int> used_nums;
	private GameObject image;
	private string[] markers;

	private int count_x;
	private int count_o;

	private int computer_turn = 0;

	private int[] corners;

	void Awake()
	{
		InitializeBoard ();
	}


	void Start () 
	{
		corners = new int[5] { 0,2,4,6,8};
		turn = 0;
		//Randomize turn
		//turn = Random.Range (0, 2);
		//turns = new int[2];

		//markers = new string[2]{"o","x"};

	}
	

	void Update () 
	{
		//used_nums = new List<int> ();
		//if (turn == 0) 
		//{
		//	Debug.Log ("player turn");
			//turns[0] = Mathf.Abs(turn);
			//turns[1] = Mathf.Abs(turn - 1);
		if (turn == 0)
		{
			PlayerTurn();
			CheckBoard();
		}
		else if(turn == 1)
		{
 			ComputerTurn();
			CheckBoard();
		}
		else if (turn == -1)
			GameOver();
		//} 
		//else if (turn == 1) 
		//{
		//	Debug.Log ("computer turn");
			//turns[0] = Mathf.Abs(1);
			//turns[1] = Mathf.Abs(turn - 1);
		//	ComputerTurn();
		//}

        
       //CheckBoard();
	}




	void PlayerTurn()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		
		if (Input.GetMouseButtonUp (0))
		{
			if (Physics.Raycast (ray, out mouseClickHit)) 
			{
				Debug.Log (mouseClickHit.collider.gameObject.name);
				if ( board_nos[int.Parse(mouseClickHit.collider.gameObject.name)] == "x" || board_nos[int.Parse(mouseClickHit.collider.gameObject.name)] == "o")
				{
					Debug.Log("Try again!");
					return;
					//PlayerTurn();
				}
				else
				{
					board_nos[int.Parse(mouseClickHit.collider.gameObject.name)] = "o";
					mouseClickHit.collider.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/o");//+turns[0].ToString());
					mouseClickHit.collider.gameObject.GetComponent<Image>().color = new Vector4(255,255,255,1);
				//board_nos.SetValue(int.Parse(mouseClickHit.collider.gameObject.name), pickAposition);
				//board_nos[int.Parse(mouseClickHit.collider.gameObject.name)] = "-";
					CreateBoard();
					//CheckBoard();
					
					//ComputerTurn();
					turn = 1;
				}
			}
		}
		
		//CheckBoard ();
		//ComputerTurn ();
	}

    void ComputerTurn()
    {
        //turn = 1;
        count_o = 0;
        count_x = 0;

        computer_turn += 1;
        Debug.Log(corners.Length);

        if (computer_turn == 1)
        {
            pickAposition = corners[Random.Range(0, corners.Length)];
            while (board_nos[pickAposition] == "x" || board_nos[pickAposition] == "o")
            {
                pickAposition = corners[Random.Range(0, corners.Length)];
            }
            board_nos[pickAposition] = "x";
        }


        if (computer_turn > 1)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == 'x')
                        count_x += 1;
                    else if (board[i][j] == 'o')
                        count_o += 1;
                    else if ((board[i][j] >= 48) && (board[i][j] <= 57))
                        pickAposition = int.Parse(board[i][j].ToString());
                }
                if ((count_o + count_x) == 3)
                {
                    count_o = 0;
                    count_x = 0;
                    continue;
                }
                else if (count_x == 2)
                {
                    board_nos[pickAposition] = "x";
                    break;
                }
                else if (count_o == 2)
                {
                    board_nos[pickAposition] = "x";
                    break;
                }
                else
                {
                    pickAposition = -1;
                    count_o = 0;
                    count_x = 0;
                    /*
                    pickAposition = Random.Range (0, 9);
                    while (board_nos[pickAposition] == "x" || board_nos[pickAposition] == "o") 
                    {
                        pickAposition = Random.Range (0, 9);
                    }
                    break;
                    */
                }
            }
        }

        if (pickAposition == -1)
        {
            if (!board[corners[0]].Equals('x') || !board[corners[1]].Equals('x') || !board[corners[2]].Equals('x') || !board[corners[3]].Equals('x') || !board[corners[4]].Equals('x') || !board[corners[0]].Equals('o') || !board[corners[1]].Equals('o') || !board[corners[2]].Equals('o') || !board[corners[3]].Equals('o') || !board[corners[4]].Equals('o'))
            {
                pickAposition = corners[Random.Range(0, corners.Length)];
                while (board_nos[pickAposition] == "x" || board_nos[pickAposition] == "o")
                {
                    pickAposition = corners[Random.Range(0, corners.Length)];
                }
            }
            else
            {
                pickAposition = Random.Range(0, 9);
                while (board_nos[pickAposition] == "x" || board_nos[pickAposition] == "o")
                {
                    pickAposition = Random.Range(0, 9);
                }
            }
            board_nos[pickAposition] = "x";
        }



        GameObject.Find(pickAposition.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/x");
        GameObject.Find(pickAposition.ToString()).GetComponent<Image>().color = new Vector4(255, 255, 255, 1);
        CreateBoard();

        turn = 0;
    }

		/*
		if (pickAposition >= 0)
		{
		for (int i = 0; i < board.Length; i++) 
		{
			for (int j = 0; j < board[i].Length; j++) 
			{
				if (board[i][j] == 'x')
					count_x += 1;
				if (board[i][j] == 'o')
					count_o += 1;
				if ((board[i][j] >= 48) && (board[i][j] <=57))
					pickAposition = int.Parse(board[i][j].ToString());
			}
			if (count_o == 2)
			{
				board_nos[pickAposition] = "x";
				break;
			}
			else if(count_x == 2)
			{
				board_nos[pickAposition] = "x";
				break;
			}
			count_o = 0;
			count_x = 0;
			pickAposition = -1;
			//pickAposition = Random.Range (0, 9);
		}
		}
		if (pickAposition == -1)
		{
			pickAposition = Random.Range (0, 9);
			if (board_nos[pickAposition] == "x" || board_nos[pickAposition] == "o") 
			{
				return;
			}
		}
	

		//board_nos[pickAposition] = "x";
		GameObject.Find(pickAposition.ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/x");
		GameObject.Find(pickAposition.ToString()).GetComponent<Image>().color = new Vector4(255,255,255,1);

		//image = GameObject.Find(pickAposition.ToString());
		//image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/x");
		//image.GetComponent<Image>().color = new Vector4(0,0,0,1);
		//GameObject.Find (pickAposition.ToString ()).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images/1");// + turns [1].ToString());
		CreateBoard ();


		if (board_nos [pickAposition] != "x" || board_nos[pickAposition] == "o")
			pickAposition = Random.Range (0, 10);
		else {
			GameObject.Find (pickAposition.ToString ()).GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images/" + turns [1]);
			board_nos.SetValue(null, pickAposition);
		}

		//CheckBoard ();
		//PlayerTurn ();
		*/
	


    void CheckBoard()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if(board[i]==winning_string1 || board[i]==winning_string2)
			{
				Debug.Log(board[i]+ " wins!");
				for (int j = 0; j < board[i].Length; j++) 
				{
					Debug.Log("Images/"+board[i][j].ToString()+"_w");
					GameObject.Find(board_temp[i][j].ToString()).GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/"+board[i][0].ToString()+"_w");
				}
				turn = -1;
				return;
			}
        }
    }

	void GameOver()
	{
		Debug.Log("game OveR!");
	}



	void InitializeBoard()
	{
		board_nos = new string[9]{"0","1","2","3","4","5","6","7","8"};
		board = new string[8];
		//board_nos_temp = new string[9]{"0","1","2","3","4","5","6","7","8"};
		board_temp = new string[8]{"012","345","678","036","147","258","048","246"};
		board_ai_temp = new string[8]{"012","345","678","036","147","258","048","246"};

		CreateBoard ();
		Debug.Log(board.Length);
	}



	void CreateBoard()
	{
		board [0] = board_nos [0] + board_nos [1] + board_nos [2];  
		board [1] = board_nos [3] + board_nos [4] + board_nos [5];
		board [2] = board_nos [6] + board_nos [7] + board_nos [8];
		board [3] = board_nos [0] + board_nos [3] + board_nos [6];
		board [4] = board_nos [1] + board_nos [4] + board_nos [7];
		board [5] = board_nos [2] + board_nos [5] + board_nos [8];
		board [6] = board_nos [0] + board_nos [4] + board_nos [8];
		board [7] = board_nos [2] + board_nos [4] + board_nos [6];
	}

}



















//////////////////////////////////////////////////
/// EXTRA STUFF ///
///////////////////
/*
board[0] = new string[3] { "00", "01", "02" };
board[1] = new string[3] { "10", "11", "12" };
board[2] = new string[3] { "20", "21", "22" };
board[3] = new string[3] { "00", "10", "20" };
board[4] = new string[3] { "01", "11", "21" };
board[5] = new string[3] { "02", "12", "22" };
board[6] = new string[3] { "00", "11", "22" };
board[7] = new string[3] { "02", "11", "20" };

		 board = new string[8,3]
		{
			{board_nos[0],board_nos[1],board_nos[2]},  
			{"10","11","12"},  
			{"20","21","22"},  
			{"00","10","20"},  
			{"01","11","21"},  
			{"02","12","22"},  
			{"00","11","22"},  
			{"02","11","20"}   
		};

 /*if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Clicked!");
            Debug.DrawRay(ray.origin, ray.direction);
        }*/
///
/// 
//////////////////////////////////////////////////









