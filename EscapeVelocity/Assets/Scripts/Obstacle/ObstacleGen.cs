using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGen : MonoBehaviour
{
    [Header("Wall Settings")]
    public GameObject wallSegmentPrefab;
    public float destroyX = -15f;
    public float spawnX = 15f;
    public float startY = -2f;
    public float segmentHeight = 1f;

    [Header("Difficulty Settings")]
    public float baseSpawnInterval = 2f;
    public float minSpawnInterval = 0.7f;
    public float baseMoveSpeed = 5f;
    public float maxMoveSpeed = 12f;
    public float difficultyRampTime = 120f;

    [Header("Manual Difficulty Override")]
    public bool useManualDifficulty = false;
    [Range(0f, 1f)] public float manualDifficultyFactor = 0f;

    [Header("Debug Info")]
    [Range(0f, 1f)] public float currentDifficultyFactor = 0f;

    private float elapsedTime = 0f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            UpdateDifficulty();
            SpawnWall();

            float difficulty = GetDifficultyFactor();
            float currentSpawnInterval = Mathf.Lerp(baseSpawnInterval, minSpawnInterval, difficulty);
            yield return new WaitForSeconds(currentSpawnInterval);
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

        WallMover mover = wallGroup.AddComponent<WallMover>();
        mover.speed = Mathf.Lerp(baseMoveSpeed, maxMoveSpeed, GetDifficultyFactor());
        mover.destroyX = destroyX;
    }

    void UpdateDifficulty()
    {
        if (!useManualDifficulty)
        {
            elapsedTime += Time.deltaTime;
        }

        currentDifficultyFactor = GetDifficultyFactor();
    }

    float GetDifficultyFactor()
    {
        float factor = useManualDifficulty ? manualDifficultyFactor : Mathf.Clamp01(elapsedTime / difficultyRampTime);
        
        return Mathf.Clamp01(factor * 0.5f);
        }
}
