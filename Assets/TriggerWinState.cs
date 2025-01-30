using UnityEngine;

public class TriggerWinState : MonoBehaviour
{
    public WinStater WinStater;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            WinStater.ShowWinScreen();
        }
    }
}
