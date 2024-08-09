using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 24*60)]
    private int startMinute = 8 * 60;
    [SerializeField]
    private float minutesPerSecond = 0.01f;
    
    private int currentDay;
    private float currentMinute;
    private Coroutine dayCycle;

    private const int Hrs24 = 24 * 60;

    /// <summary>
    /// Fired whenever game's initial values are set and starts
    /// </summary>
    public static event Action OnGameStarted;
    /// <summary>
    /// Fired whenever the player loses the game
    /// </summary>
    public static event Action OnGameLost;
    /// <summary>
    /// Fired whenever game's time changes. [currentMinute, currentDay]
    /// </summary>
    public static event Action<int, int> OnTimeChanged;

    private void Start()
    {
        StartGame();
    }

    /// <summary>
    /// Resets all game variables to start values and starts the game
    /// </summary>
    public void StartGame()
    {
        currentDay = 1;
        currentMinute = startMinute;
        dayCycle = StartCoroutine(DayCycle());
        OnGameStarted?.Invoke();
    }

    private IEnumerator DayCycle()
    {
        while (true)
        {
            var minutesPassed = Time.deltaTime * minutesPerSecond;
            var addedUp = currentMinute + minutesPassed;
            if (addedUp >= Hrs24)
            {
                // new day
                currentDay++;
            }
            currentMinute = addedUp % Hrs24;
            OnTimeChanged?.Invoke((int)currentMinute, currentDay);
            yield return null;
        }
    }
}
