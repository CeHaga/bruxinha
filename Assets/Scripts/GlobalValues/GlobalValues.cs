using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    public static GlobalValues instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public Digits[] digits;
    public HighscoreDigits[] highscoreDigits;
}
