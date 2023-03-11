using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdAttack : MonoBehaviour
{
    public GameObject coldAttackPrefab;
    public GameObject coldPuddlePrefab;
    public float duration = 5f;
    public bool IsActive { get; private set; }
    public float slowDuration = 3f;
    public float slowAmount = 0.5f;

    private const float arenaWidth = 35.6f;
    private const float arenaHeight = 13.74f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        IsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                EndAttack();
            }
        }
    }

    public void StartAttack()
    {
        IsActive = true;
        timer = 0f;

        Vector2 randomPos = new Vector2(
            Random.Range(-arenaWidth / 2, arenaWidth / 2),
            Random.Range(-arenaHeight / 2, arenaHeight / 2));


        Instantiate(coldAttackPrefab, randomPos, Quaternion.identity);
        // Play sound effect or visual effect for cold attack
    }

    private void EndAttack()
    {
        IsActive = false;
        timer = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsActive && other.gameObject.tag == "Player")
        {
            BossFightCharacter player = other.gameObject.GetComponent<BossFightCharacter>();
            player.ApplySlow(slowAmount, slowDuration);
        }
    }
}
