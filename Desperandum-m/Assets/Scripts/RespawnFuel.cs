using UnityEngine;

public class RespawnFuel : MonoBehaviour
{
    [SerializeField] private GameObject FuelCan;
    [SerializeField] private Collider2D coll;

    public float respawnTime = 5f;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sprite")
        {
            Debug.Log("Invoking");
            Invoke(nameof(SpawnCanister), respawnTime);
        }
    }

    private void SpawnCanister()
    {
        Debug.Log("Spawned Canister");
        Vector2 pos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Instantiate(FuelCan, pos, Quaternion.identity);
        FuelCan.SetActive(true); // Zaru�� aby se klon spawnul aktivovan�
        coll.isTrigger = true;
    }
}