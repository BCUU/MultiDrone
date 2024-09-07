using UnityEngine;
using System.Collections;
using Fusion;

public class RoomScaler : MonoBehaviour
{
    public Vector3 newRoomPosition = new Vector3(0, 1, 0); // Küçültülmüþ odanýn pozisyonu
    public Vector3 roomScale = new Vector3(0.1f, 0.1f, 0.1f); // Odanýn küçültülmüþ ölçeði
    public Vector3 roomScale1 = new Vector3(1f, 1f, 1f); // Odanýn küçültülmüþ ölçeði
    public float delayTime = 3f; // Gecikme süresi (saniye cinsinden)
    public GameObject roomObject = null;
    public GameObject objectToBecomeChild = null;
    public GameObject parentObjectForClone = null;

    // Bu artýk NetworkObject olacak
    private NetworkObject roomObjectCopy = null;

    // NetworkRunner referansý
    private NetworkRunner runner;

    void Start()
    {
        // Runner referansýný buluyoruz
        runner = FindObjectOfType<NetworkRunner>();

        if (runner == null)
        {
            Debug.LogError("NetworkRunner bulunamadý. Lütfen sahnede bir NetworkRunner objesi olduðundan emin olun.");
            return;
        }

        // Coroutine baþlatýyoruz
        StartCoroutine(DelayedRoomScaling());
    }

    // Coroutine ile gecikmeli odanýn küçültülmesi
    IEnumerator DelayedRoomScaling()
    {
        // Belirtilen süre boyunca bekliyoruz
        yield return new WaitForSeconds(delayTime);

        // Oda objesini sahnede buluyoruz
        roomObject = GameObject.Find("Room - 36b78a70-9a5f-99fb-4781-7a0a772916c7");

        if (roomObject != null)
        {
            NetworkObject networkObject = roomObject.GetComponent<NetworkObject>();
            if (networkObject == null)
            {
                networkObject = roomObject.AddComponent<NetworkObject>(); // Eðer NetworkObject yoksa ekliyoruz
            }

            // Objeyi bulduk, þimdi onun bir kopyasýný Fusion'da oluþturuyoruz
            roomObjectCopy = runner.Spawn(roomObject, newRoomPosition, Quaternion.identity);

            // Kopyanýn pozisyonunu ve boyutunu deðiþtiriyoruz
            roomObjectCopy.transform.position = newRoomPosition;
            roomObjectCopy.transform.localScale = roomScale; // Odayý küçültüyoruz
        }
        else
        {
            Debug.LogWarning("Room object not found!");
        }

        // Kopyayý bir parent objeye eklemek isterseniz
        if (parentObjectForClone != null && roomObjectCopy != null)
        {
            roomObjectCopy.transform.SetParent(parentObjectForClone.transform);
        }
    }

    public void ScaleBack()
    {
        if (roomObjectCopy != null)
        {
            // roomObjectCopy'nin GameObject bileþenine eriþiyoruz
            objectToBecomeChild.transform.SetParent(roomObjectCopy.transform);
            roomObjectCopy.transform.localScale = roomScale1;
            objectToBecomeChild.transform.SetParent(null);
            roomObjectCopy.gameObject.SetActive(false); // Burada GameObject olarak SetActive yapýyoruz
            DisableAllMeshRenderers(roomObjectCopy.gameObject);
        }
        else
        {
            Debug.LogWarning("Room object copy not found!");
        }
    }

    public void DisableAllMeshRenderers(GameObject parentObject)
    {
        // Ýlk önce parent objedeki MeshRenderer'ý kontrol edelim.
        MeshRenderer meshRenderer = parentObject.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        // Þimdi child objeleri üzerinde iþlem yapalým.
        foreach (Transform child in parentObject.transform)
        {
            DisableAllMeshRenderers(child.gameObject);
        }
    }
}
