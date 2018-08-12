using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginButton : MonoBehaviour
{
    public void BeginClicked()
    {
        SceneManager.LoadScene("Maze");
    }

    public void TitleClicked()
    {
        SceneManager.LoadScene("Title");
    }

    public void ExitClicked()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#elif (UNITY_WEBGL)
        Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
    }
}
