using System;
using UnityEngine;
using System.Collections;

public class LaserAttack : MonoBehaviour
{
    [Header("Target & Movement")]
    [SerializeField] private Transform target;
    [SerializeField] private MonoBehaviour movementScript; 
    
    [Header("Laser Timing")]
    [SerializeField] private float laserInterval = 6f;
    [SerializeField] private float warningDuration = 1.5f;
    [SerializeField] private float laserDuration = 0.5f;

    [Header("Laser Visuals")]
    [SerializeField] private GameObject warningLinePrefab;
    [SerializeField] private GameObject laserBeamPrefab;
    [SerializeField] private Transform laserSpawnPoint;

    private float timer = 0f;
    private bool isAttacking = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= laserInterval && !isAttacking)
        {
            StartCoroutine(LaserRoutine());
            timer = 0f;
        }
    }

    private IEnumerator LaserRoutine()
    {
        isAttacking = true;
        
        if (movementScript != null)
            movementScript.enabled = false;
        
        GameObject warning = Instantiate(warningLinePrefab, laserSpawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(warningDuration);
        Destroy(warning);
        
        GameObject laser = Instantiate(laserBeamPrefab, laserSpawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(laserDuration);
        Destroy(laser);
        
        if (movementScript != null)
            movementScript.enabled = true;

        isAttacking = false;
    }
}