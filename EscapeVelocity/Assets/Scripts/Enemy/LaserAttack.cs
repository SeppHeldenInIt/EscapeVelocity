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

    [Header("Raycast Settings")]
    [SerializeField] private float raycastLength = 20f;
    [SerializeField] private float raycastOffset = 0.5f; 

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
        
        Vector3 topOrigin = laserSpawnPoint.position + Vector3.up * raycastOffset;
        Vector3 bottomOrigin = laserSpawnPoint.position + Vector3.down * raycastOffset;
        Vector3 direction = Vector3.right;

        RaycastHit2D hitTop = Physics2D.Raycast(topOrigin, direction, raycastLength);
        RaycastHit2D hitBottom = Physics2D.Raycast(bottomOrigin, direction, raycastLength);

        Debug.DrawRay(topOrigin, direction * raycastLength, Color.red, 1f);
        Debug.DrawRay(bottomOrigin, direction * raycastLength, Color.red, 1f);

        if (hitTop.collider != null && hitTop.collider.CompareTag("Player"))
        {
            Debug.Log("Top ray hit player!");
        }

        if (hitBottom.collider != null && hitBottom.collider.CompareTag("Player"))
        {
            Debug.Log("Bottom ray hit player!");
        }

        yield return new WaitForSeconds(laserDuration);
        Destroy(laser);

        if (movementScript != null)
            movementScript.enabled = true;

        isAttacking = false;
    }
}
