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
    public Arabis arabis;
    public bool isSpawningBlades;

    private void Start()
    {
        StartBladeCoroutine(1);
        Arabis arabis = GetComponent<Arabis>();
    }

    private void OnEnable()
    {
        if (arabis.finalPhaseFlag && !isSpawningBlades)
        {
            print("Flag Raised");
            StartCoroutine(SpawnBlades(1));
            isSpawningBlades = true;
        }
    }

    private IEnumerator SpawnBlades(int numberOfSpawns)
    {
        // Clear the list of spawned blades before spawning new ones
        spawnedBlades.Clear();

        // spawn the blades at the desired positions
        foreach (Transform spawnPosition in spawnLocations)
        {
            for (int i = 0; i < numberOfSpawns; i++)
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
        }

        // wait for bladeDuration seconds before finishing
        yield return new WaitForSeconds(bladeDuration);
    }

    public void StartBladeCoroutine(int param)
    {
        StartCoroutine(SpawnBlades(param));
    }

    public void DestroyBlades()
    {
        foreach (GameObject blade in spawnedBlades)
        {
            Destroy(blade);
        }
    }
}