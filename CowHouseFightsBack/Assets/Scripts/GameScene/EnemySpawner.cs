using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject testEnemy;
    [SerializeField] private int spawnMinuteInterval;
    [SerializeField] private Transform enemiesGO;
    [SerializeField] private Transform towersGO;
    [SerializeField] private PlayfieldController playfieldController;
    [SerializeField] private Transform endGoal;
    [SerializeField] private int maxEnemies = 5;
    
    // Last minute that an enemy had been spawned
    private int lastMinuteSpawned = 0;
    // Last day that an enemy had been spawned
    private int lastDaySpawned = 0;
    private bool gameInProgress = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameController.OnGameStarted += GameStarted;
        GameController.OnTimeChanged += TimeChanged;
        GameController.OnGameLost += GameEnded;
    }

    private void GameStarted()
    {
        gameInProgress = true;
    }

    private void GameEnded()
    {
        gameInProgress = false;
    }
    
    private void TimeChanged(int currentMinute, int currentDay)
    {
        if (!gameInProgress)
            return;
        
        if (enemiesGO.childCount >= maxEnemies)
            return;

        if (
            // same day check
            !(lastMinuteSpawned + spawnMinuteInterval <= currentMinute) &&
            // maybe new day?
            !(currentDay > lastDaySpawned && currentMinute >= spawnMinuteInterval)
            )
            return;
        
        var enemy = Instantiate(testEnemy, enemiesGO);
        var randOffset = new Vector3(transform.lossyScale.x * Random.Range(-.5f, .5f), 0, transform.lossyScale.y * Random.Range(-.5f, .5f));
        enemy.transform.position = transform.position + randOffset;
        var enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Setup(towersGO, playfieldController, endGoal);
        
        lastMinuteSpawned = currentMinute;
        lastDaySpawned = currentDay;
    }
}
