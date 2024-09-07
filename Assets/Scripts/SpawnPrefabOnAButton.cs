using Fusion;
using UnityEngine;

public class SpawnPrefabOnAButton : NetworkBehaviour
{
    public GameObject prefab; // The prefab you want to spawn
    public Transform spawnPoint; // Where the prefab will spawn

    private bool hasSpawned = false; // Check if it has been spawned

    void FixedUpdate()
    {
        // Check if A button is pressed and object has not been spawned yet
        if (OVRInput.GetDown(OVRInput.Button.One) )
        {
            // Prefab should be spawned using network spawning
            
            Runner.Spawn(prefab, spawnPoint.position, spawnPoint.rotation,Runner.LocalPlayer);
            
            // Mark as spawned
            hasSpawned = true;
        }
    }
}
