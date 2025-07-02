using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGen : MonoBehaviour
{
    public GameObject wallSegmentPrefab;
    public float spawnInterval = 2f;
    public float moveSpeed = 5f;
    public float destroyX = -15f;
    public float spawnX = 15f;
    public float startY = -2f;
    public float segmentHeight = 1f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnWall();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnWall()
    {
        GameObject wallGroup = new GameObject("WallGroup");
        wallGroup.transform.position = new Vector3(spawnX, 0, 0);
        
        List<int> indices = new List<int> { 0, 1, 2, 3, 4 };
        int firstGap = indices[Random.Range(0, indices.Count)];
        indices.Remove(firstGap);
        int secondGap = indices[Random.Range(0, indices.Count)];

        for (int i = 0; i < 5; i++)
        {
            if (i == firstGap || i == secondGap) continue;

            Vector3 segmentPos = new Vector3(spawnX, startY + i * segmentHeight, 0);
            GameObject segment = Instantiate(wallSegmentPrefab, segmentPos, Quaternion.identity, wallGroup.transform);
        }

        wallGroup.AddComponent<WallMover>().speed = moveSpeed;
        wallGroup.GetComponent<WallMover>().destroyX = destroyX;
    }
}