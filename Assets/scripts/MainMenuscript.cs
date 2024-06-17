using UnityEngine;
using UnityEngine.SceneManagement;     
public class MainMenu : MonoBehaviour
{
  public void playgame()
  {
    SceneManager.LoadSceneAsync("level1");
  }

  public void quitgame()
  {
    Application.Quit();
  }
}
