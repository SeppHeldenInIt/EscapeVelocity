using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClick : MonoBehaviour
{
    [SerializeField] private GameObject targetCube; // Assign your cube here in Inspector
    [SerializeField] private SoundFXManager sfxManager;
    [SerializeField] private AudioClip _audioClip;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == targetCube)
                {
                    OnCubeClicked();
                }
            }
        }
    }

    private void OnCubeClicked()
    {
        sfxManager.PLaySoundFXClip(_audioClip, transform, 1f);
    }
}
