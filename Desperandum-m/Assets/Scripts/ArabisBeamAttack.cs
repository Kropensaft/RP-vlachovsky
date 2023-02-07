using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ArabisBeamAttack : MonoBehaviour
{
    public GameObject beamPrefab;
    public Transform[] spawnPoints;
    public float progressionSpeed = 1.0f;
    private Transform randomSpawnPoint;



     private List<int> usedIndices = new List<int>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int randomIndex = GetRandomAvailableIndex();
            if (randomIndex != -1)
            {
                 randomSpawnPoint = spawnPoints[randomIndex];
                Vector3 endPosition = new Vector3(randomSpawnPoint.position.x, randomSpawnPoint.position.y - 15, randomSpawnPoint.position.z);
                GameObject beamInstance = Instantiate(beamPrefab, randomSpawnPoint.position, Quaternion.Euler(0, 0, 90));
                StartCoroutine(MoveBeam(beamInstance.transform, endPosition));
                usedIndices.Add(randomIndex);
            }
        }
    }

    IEnumerator MoveBeam(Transform beamTransform, Vector3 endPosition)
    {
        float journey = 0.0f;
        while (journey <= 1.0f)
        {
            journey += Time.deltaTime * progressionSpeed;
            beamTransform.position = Vector3.Lerp(randomSpawnPoint.position, endPosition, journey);
            yield return null;
        }
    }

    private int GetRandomAvailableIndex()
    {
        if (spawnPoints.Length == usedIndices.Count)
        {
            return -1;
        }
        int randomIndex = Random.Range(0, spawnPoints.Length);
        while (usedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, spawnPoints.Length);
        }
        return randomIndex;
    }
}