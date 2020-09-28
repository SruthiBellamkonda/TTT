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
public class PlayerColour
{
    public Color panelColor;
    public Color textColor;
    
}


public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string PlayerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private int moveCount;
    public GameObject restartbutton;
    public GameObject startinfo;

    public Player playerX;
    public Player playerO;
    public PlayerColour activePlayerColor;
    public PlayerColour inactivePlayerColor;


     void Awake()
    {
        gameOverPanel.SetActive(false);
        setContollerReferenceOnButtons();
        moveCount = 0;
        restartbutton.SetActive(false);
       
    }

    void setContollerReferenceOnButtons()
    {
        for(int i=0;i<buttonList.Length;i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void SetstartingSide(string startingSide)
    {
        PlayerSide = startingSide;
        if (PlayerSide == "X")
        {
            setplayercolor(playerX, playerO);
        }
        else
        {
            setplayercolor(playerO, playerX);
        }
        startgame();
    }
    void startgame()
    {
        setBoardInteractible(true);
        setplayerbutton(false);
        startinfo.SetActive(false);


    }
    public string getPlayerSide()
    {
        return PlayerSide;
    }
    public void endTurn()
    {
        moveCount++;
        
        if(buttonList[0].text==PlayerSide&& buttonList[1].text == PlayerSide && buttonList[2].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if (buttonList[3].text == PlayerSide && buttonList[4].text == PlayerSide && buttonList[5].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if (buttonList[6].text == PlayerSide && buttonList[7].text == PlayerSide && buttonList[8].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if (buttonList[0].text == PlayerSide && buttonList[3].text == PlayerSide && buttonList[6].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if (buttonList[1].text == PlayerSide && buttonList[4].text == PlayerSide && buttonList[7].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if (buttonList[2].text == PlayerSide && buttonList[5].text == PlayerSide && buttonList[8].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if(buttonList[0].text == PlayerSide && buttonList[4].text == PlayerSide && buttonList[8].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if(buttonList[2].text == PlayerSide && buttonList[4].text == PlayerSide && buttonList[6].text == PlayerSide)
        {
            Gameover(PlayerSide);
        }
        else if(moveCount>=9)
        {
            Gameover("draw");
        }
        else
        {
            ChangeSide();
        }
    }
     void setplayercolor(Player newplayer,Player oldplayer)
    {
        newplayer.panel.color = activePlayerColor.panelColor;
        newplayer.text.color = activePlayerColor.textColor;
        oldplayer.panel.color = inactivePlayerColor.panelColor;
        oldplayer.text.color = inactivePlayerColor.textColor;
    }
    void Gameover(string WinningPlayer)
    {
        setBoardInteractible(false);
        if (WinningPlayer == "draw")
        {
            setgameOvertext("It is a Draw");
            setplayercolorInactive();
        }
        else
        {
            setgameOvertext(WinningPlayer + " Won! ");
        }
        restartbutton.SetActive(true);

    }
    void ChangeSide()
    {
        PlayerSide = (PlayerSide == "X") ? "O" : "X";
        if (PlayerSide == "X")
        {
            setplayercolor(playerX, playerO);
        }
        else
        {
            setplayercolor(playerO, playerX);
        }
    }
    void setgameOvertext(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }
    public void Restart()
    {
        moveCount = 0;         
        gameOverPanel.SetActive(false);
        restartbutton.SetActive(false);
        setplayerbutton(true);
        setplayercolorInactive();
        startinfo.SetActive(true);
        for(int i = 0; i < buttonList.Length; i++)
        {
            
            buttonList[i].text = "";
        }
            

    }
    void setBoardInteractible(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void setplayerbutton(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }
    void setplayercolorInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}
