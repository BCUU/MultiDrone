using UnityEngine;
using System.Collections;
using Fusion;

public class RoomScaler : MonoBehaviour
{
    public Vector3 newRoomPosition = new Vector3(0, 1, 0); // K���lt�lm�� odan�n pozisyonu
    public Vector3 roomScale = new Vector3(0.1f, 0.1f, 0.1f); // Odan�n k���lt�lm�� �l�e�i
    public Vector3 roomScale1 = new Vector3(1f, 1f, 1f); // Odan�n k���lt�lm�� �l�e�i
    public float delayTime = 3f; // Gecikme s�resi (saniye cinsinden)
    public GameObject roomObject = null;
    public GameObject objectToBecomeChild = null;
    public GameObject parentObjectForClone = null;

    // Bu art�k NetworkObject olacak
    private NetworkObject roomObjectCopy = null;

    // NetworkRunner referans�
    private NetworkRunner runner;

    void Start()
    {
        // Runner referans�n� buluyoruz
        runner = FindObjectOfType<NetworkRunner>();

        if (runner == null)
        {
            Debug.LogError("NetworkRunner bulunamad�. L�tfen sahnede bir NetworkRunner objesi oldu�undan emin olun.");
            return;
        }

        // Coroutine ba�lat�yoruz
        StartCoroutine(DelayedRoomScaling());
    }

    // Coroutine ile gecikmeli odan�n k���lt�lmesi
    IEnumerator DelayedRoomScaling()
    {
        // Belirtilen s�re boyunca bekliyoruz
        yield return new WaitForSeconds(delayTime);

        // Oda objesini sahnede buluyoruz
        roomObject = GameObject.Find("Room - 36b78a70-9a5f-99fb-4781-7a0a772916c7");

        if (roomObject != null)
        {
            NetworkObject networkObject = roomObject.GetComponent<NetworkObject>();
            if (networkObject == null)
            {
                networkObject = roomObject.AddComponent<NetworkObject>(); // E�er NetworkObject yoksa ekliyoruz
            }

            // Objeyi bulduk, �imdi onun bir kopyas�n� Fusion'da olu�turuyoruz
            roomObjectCopy = runner.Spawn(roomObject, newRoomPosition, Quaternion.identity);

            // Kopyan�n pozisyonunu ve boyutunu de�i�tiriyoruz
            roomObjectCopy.transform.position = newRoomPosition;
            roomObjectCopy.transform.localScale = roomScale; // Oday� k���lt�yoruz
        }
        else
        {
            Debug.LogWarning("Room object not found!");
        }

        // Kopyay� bir parent objeye eklemek isterseniz
        if (parentObjectForClone != null && roomObjectCopy != null)
        {
            roomObjectCopy.transform.SetParent(parentObjectForClone.transform);
        }
    }

    public void ScaleBack()
    {
        if (roomObjectCopy != null)
        {
            // roomObjectCopy'nin GameObject bile�enine eri�iyoruz
            objectToBecomeChild.transform.SetParent(roomObjectCopy.transform);
            roomObjectCopy.transform.localScale = roomScale1;
            objectToBecomeChild.transform.SetParent(null);
            roomObjectCopy.gameObject.SetActive(false); // Burada GameObject olarak SetActive yap�yoruz
            DisableAllMeshRenderers(roomObjectCopy.gameObject);
        }
        else
        {
            Debug.LogWarning("Room object copy not found!");
        }
    }

    public void DisableAllMeshRenderers(GameObject parentObject)
    {
        // �lk �nce parent objedeki MeshRenderer'� kontrol edelim.
        MeshRenderer meshRenderer = parentObject.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        // �imdi child objeleri �zerinde i�lem yapal�m.
        foreach (Transform child in parentObject.transform)
        {
            DisableAllMeshRenderers(child.gameObject);
        }
    }
}
