using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public void LoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }
    public void Quit()
    {
        //NO IDEA IF THIS WOULD WORK
        //Application.Quit();
        Destroy(gameObject);
    }

    public void LoadBuildIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void MoveToLevelSelect(Transform canvasTransform)
    {
        canvasTransform.Translate(0, 180, 0);
    }
}
