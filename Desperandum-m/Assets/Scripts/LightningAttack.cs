using UnityEngine;

public class LightningAttack : MonoBehaviour
{
    public GameObject lightningAttackPrefab;
    public BossFightCharacter player;
    public ScreenShake screenshake;
    public float duration = 5f;
    public float attackDuration;
    public bool IsActive { get; private set; }
    public float stunDuration = 2f;

    private float timer;

    [HideInInspector] public bool hit = false;
    private const float arenaWidth = 35.6f;
    private const float arenaHeight = 13.74f;

    // Start is called before the first frame update
    private void Start()
    {
        IsActive = false;
        attackDuration = duration;
        BossFightCharacter player = GetComponent<BossFightCharacter>();
        ScreenShake screenshake = GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    private void Update()
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

        GameObject lightningAttackObject = Instantiate(lightningAttackPrefab, randomPos, Quaternion.identity);
        LightningAttackObject lightningAttack = lightningAttackObject.GetComponent<LightningAttackObject>();
        lightningAttack.duration = attackDuration;

        screenshake.TriggerShake();
    }

    private void EndAttack()
    {
        IsActive = false;
        timer = 0f;
    }
}