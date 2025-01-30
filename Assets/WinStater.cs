using UnityEngine;

public class WinStater : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject bar;

    private void Start()
    {
        Time.timeScale = 1f;
        winScreen.SetActive(false);
        bar.SetActive(true);
    }
    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        bar.SetActive(false);
        Time.timeScale = 0f;
    }
}
