using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
public float PlayButton;
private Button button;
{
   public string newGameLevel = "Level1";
   public void NewGameButton()
   {
    SceneManager.LoadScene(newGameLevel);
   }
// Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayButton);
    }
}
