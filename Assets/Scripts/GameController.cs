using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}



[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}



public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;



    public GameObject gameOverPanel;
    public Text gameOverText;



    private int moveCount;



    public GameObject restartButton;



    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;
    


    private string computerSide;
    public bool playerMove;
    public float delay;
    private int value;
    bool counter;


    void Awake()
    {
        gameOverPanel.SetActive(false);
        SetGameControllerReferenceOnButtons();
        moveCount = 0;
        restartButton.SetActive(false);
        playerMove = true;
    }



    void Update()
    {
        if (playerMove == false)
        {
            delay += delay * Time.deltaTime;
            if (delay >= 100)
            {
                value = Random.Range(0, buttonList.Length);
                if (buttonList[value].GetComponentInParent<Button>().interactable == true)
                {
                    buttonList[value].text = GetComputerSide();
                    buttonList[value].GetComponentInParent<Button>().interactable = false;
                    EndTurn();
                }
            }
        }
    }



    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);



        }
    }



    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            computerSide = "O";
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            computerSide = "X";
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }



    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }



    public string GetPlayerSide()
    {
        return playerSide;
    }



    public string GetComputerSide()
    {
        return computerSide;
    }



    public void EndTurn()
    {
        moveCount++;
        counter = true;
        for (int i=0;i<=6;i++)
        {
            if(i%3==0)//Row
            {
                //i=0,i=3,i=6
                if (buttonList[i].text == playerSide && buttonList[i+1].text == playerSide && buttonList[i+2].text == playerSide)
                {
                    GameOver(playerSide);
                    //counter = false;
                }
                else if (buttonList[i].text == computerSide && buttonList[i + 1].text == computerSide && buttonList[i + 2].text == computerSide)
                {
                    GameOver(computerSide);
                    //counter = false;
                }
            }
            if (i < 3)//Column
            {
                //i=0,i=1,i=2
                if (buttonList[i].text == playerSide && buttonList[i + 3].text == playerSide && buttonList[i + 6].text == playerSide)
                {
                    GameOver(playerSide);
                    //counter = false;
                }
                else if (buttonList[i].text == computerSide && buttonList[i + 3].text == computerSide && buttonList[i + 6].text == computerSide)
                {
                    GameOver(computerSide);
                    //counter = false;
                }
            }
        }       
                
        if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);//Left-diagonal
            //counter = false;
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);//Right-diagonal
            //counter = false;
        }
        else if (buttonList[0].text == computerSide && buttonList[4].text == computerSide && buttonList[8].text == computerSide)
        {
            GameOver(computerSide);//Left-diagonal
            //counter = false;
        }
        else if (buttonList[2].text == computerSide && buttonList[4].text == computerSide && buttonList[6].text == computerSide)
        {
            GameOver(computerSide);//Right-diagonal
            //counter = false;

        }
        else if (moveCount >= 9)
        {
            if (counter!=false)
            GameOver("Draw");
        }
        else
        {
            ChangeSides();
        }



    }



    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }
    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        counter = false;


        if (winningPlayer == "Draw")
        {
            SetGameOverText("Match Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }



        restartButton.SetActive(true);
    }



    void ChangeSides()
    {
        //playerSide = (playerSide == "X") ? "O" : "X";
        playerMove = (playerMove == true) ? false : true;
        //if (playerSide == "X")
        if (playerMove == true)
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }



    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }



    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);
        playerMove = true;
        delay = 10;



        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }



    }



    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }



    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }



    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    
}