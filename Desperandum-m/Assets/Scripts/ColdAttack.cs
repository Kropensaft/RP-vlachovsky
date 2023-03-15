using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdAttack : MonoBehaviour
{
    public GameObject coldAttackPrefab;
   
    public float duration = 5f;
    public bool IsActive { get; private set; }
    

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

        Vector3 randomPos = new Vector3(
            Random.Range(-arenaWidth / 2, arenaWidth / 2),
            Random.Range(-arenaHeight / 2, arenaHeight / 2), -2f);


        Instantiate(coldAttackPrefab, randomPos, Quaternion.identity);
        // Play sound effect or visual effect for cold attack
    }

    private void EndAttack()
    {
        IsActive = false;
        timer = 0f;
    }

    
}
