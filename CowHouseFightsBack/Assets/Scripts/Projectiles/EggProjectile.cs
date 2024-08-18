using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EggProjectile : Projectile
{
    [SerializeField] private Billboard eggBillboard;
    [SerializeField] private Image egg;
    [SerializeField] private Sprite brokenEggSprite;

    private double startTime;
    private bool collided = false;
    private Vector3 firstFramePos;
    private Vector3 direction;
    
    private void OnCollisionEnter(Collision other)
    {
        if (collided) return;
        
        var enemyController = other.gameObject.GetComponent<EnemyController>();
        if (!enemyController) return;
        
        collided = true;
        enemyController.TakeDamage(damage);
        egg.sprite = brokenEggSprite;
        StartCoroutine(EnemyHitCoroutine());
    }

    private IEnumerator EnemyHitCoroutine()
    {
        // some delay
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        firstFramePos = transform.position;
        direction = transform.forward;
        eggBillboard.ChangePlayersCamera(Camera.main);
        startTime = Time.timeAsDouble;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collided)
            return;
        
        if (Time.timeAsDouble > startTime + lifetime)
            Destroy(this.gameObject);

        var usePos = transform.position;
        if (usePos.Equals(Vector3.zero))
        {
            usePos = firstFramePos;
        }
        rb.MovePosition(usePos + direction * (speed * Time.fixedDeltaTime));
    }
}
