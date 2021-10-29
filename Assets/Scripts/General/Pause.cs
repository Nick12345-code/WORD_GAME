using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] KeyCode pause;

    void Update()
    {
        if (Input.GetKeyDown(pause))
        {
            if (!menu.activeSelf) PauseGame();
            else ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0.0f;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        menu.SetActive(false);
    }



}
