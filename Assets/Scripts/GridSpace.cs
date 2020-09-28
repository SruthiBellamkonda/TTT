using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    //public string Playerside;
    private GameController gameController;

    public void setSpace()
    {
        buttonText.text = gameController.getPlayerSide();
        button.interactable = false;
        gameController.endTurn();
    }
   public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }
}
