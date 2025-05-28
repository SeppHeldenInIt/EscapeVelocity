using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        // Show warning
        GameObject warning = Instantiate(warningLinePrefab, laserSpawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(warningDuration);
        Destroy(warning);

        // Fire laser
        GameObject laser = Instantiate(laserBeamPrefab, laserSpawnPoint.position, Quaternion.identity);

        Vector3 topOrigin = laserSpawnPoint.position + Vector3.up * raycastOffset;
        Vector3 bottomOrigin = laserSpawnPoint.position + Vector3.down * raycastOffset;
        Vector3 direction = Vector3.right;

        // Process each ray separately with penetration
        bool topRayActive = true;
        bool bottomRayActive = true;
        HashSet<Health> damagedPlayers = new HashSet<Health>();

        ProcessPenetratingRay(topOrigin, direction, ref topRayActive, ref bottomRayActive, damagedPlayers);
        ProcessPenetratingRay(bottomOrigin, direction, ref topRayActive, ref bottomRayActive, damagedPlayers);

        yield return new WaitForSeconds(laserDuration);
        Destroy(laser);

        if (movementScript != null)
            movementScript.enabled = true;

        isAttacking = false;
    }

    private void ProcessPenetratingRay(Vector3 origin, Vector3 direction, ref bool topRayActive, ref bool bottomRayActive, HashSet<Health> damagedPlayers)
    {
        Vector3 currentOrigin = origin;
        float remainingDistance = raycastLength;
        
        // Determine which ray this is based on origin
        bool isTopRay = (origin.y > laserSpawnPoint.position.y);
        
        // Check if this specific ray is still active
        if ((isTopRay && !topRayActive) || (!isTopRay && !bottomRayActive))
            return;

        while (remainingDistance > 0.1f)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentOrigin, direction, remainingDistance);
            
            if (hit.collider == null)
            {
                // No more hits, draw the rest of the ray
                Debug.DrawRay(currentOrigin, direction * remainingDistance, Color.red, 1f);
                break;
            }

            // Draw ray up to the hit point
            Debug.DrawRay(currentOrigin, direction * hit.distance, Color.red, 1f);

            if (hit.collider.CompareTag("Player"))
            {
                // Hit a player - damage them and disable both rays
                Health playerHealth = hit.collider.GetComponent<Health>();
                if (playerHealth != null && !damagedPlayers.Contains(playerHealth))
                {
                    playerHealth.Hit();
                    damagedPlayers.Add(playerHealth);
                }
                
                // Disable both rays when any ray hits a player
                topRayActive = false;
                bottomRayActive = false;
                break;
            }
            else if (hit.collider.CompareTag("Obstacle"))
            {
                // Destroy the obstacle and continue through it
                Destroy(hit.collider.gameObject);
                
                // Continue the ray from just past the destroyed obstacle
                currentOrigin = (Vector3)hit.point + direction * 0.1f;
                remainingDistance -= (hit.distance + 0.1f);
            }
            else
            {
                // Hit something else that stops the ray
                break;
            }
        }
    }
}