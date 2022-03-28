using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadaScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
       GameManager._gmInstance.ResetScoreAndTimer();
        GameManager._gmInstance.ChangeState();
    }
    public void QuitApplication()
    {
        Application.Quit();
    }

}
