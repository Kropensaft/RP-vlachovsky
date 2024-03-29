using UnityEngine;

public class RespawnHealth : MonoBehaviour
{
    [SerializeField] private GameObject healingBeans;
    [SerializeField] private Collider2D coll;

    public float HealhtRespawnTime = 5f;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            Debug.Log("Invoking");
            Invoke(nameof(SpawnBeans), HealhtRespawnTime);
        }
    }

    private void SpawnBeans()
    {
        Debug.Log("Spawned Beans");
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Instantiate(healingBeans, pos, Quaternion.identity);
        healingBeans.SetActive(true); // Zaru�� aby se klon spawnul aktivovan�
        coll.isTrigger = true;
    }
}