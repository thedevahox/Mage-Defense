using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    public class StartMenu : MonoBehaviour
{
    public float PlayButton;
   

   public string newGameLevel = "Level1";
   public string mainMenu = "mainMenu";
   public void NewGameButton()
   {
    SceneManager.LoadScene(newGameLevel);
   }
   public void backToMenu()
   {
    SceneManager.LoadScene("MainMenu");
   }
// Start is called before the first frame update
    void Start()
    {
       
    }
    public void exitGame()
    {
        Application.Quit();
        
    }
}
