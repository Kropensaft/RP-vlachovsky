using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArabisBladeDance : MonoBehaviour
{
    public GameObject bladePrefab;
    public List<GameObject> spawnedBlades;
    public List<Transform> spawnLocations;
    public float homingDelay = 2.0f;
    public float bladeDuration = 20f;
    public float bladeStartDelay;


    void Start()
    {
        StartCoroutine(SpawnBlades());
    }

    IEnumerator SpawnBlades()
    {
        // spawn the blades at the desired positions
        foreach (Transform spawnPosition in spawnLocations)
        {
            GameObject blade = Instantiate(bladePrefab, spawnPosition.position, Quaternion.identity);
            HomingBlade bladeHoming = blade.GetComponent<HomingBlade>();
            spawnedBlades.Add(blade);
            if (bladeHoming != null)
            {
                bladeHoming.enabled = true;
                // wait for bladeStartDelay seconds before starting the next blade
                yield return new WaitForSeconds(bladeStartDelay);
            }
        }

        // wait for bladeDuration seconds before finishing
        yield return new WaitForSeconds(bladeDuration);
    }

    public void DestroyBlades()
    {
        foreach(GameObject blade in spawnedBlades)
        {
            Destroy(blade);
        }

    }
}
