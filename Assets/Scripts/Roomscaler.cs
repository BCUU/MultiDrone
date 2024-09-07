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

    private GameObject roomObjectCopy = null;

    void Start()
    {
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
        NetworkObject networkObject = roomObject.AddComponent<NetworkObject>();


        if (roomObject != null)
        {
            // Objeyi bulduk, þimdi onun bir kopyasýný oluþturuyoruz
            roomObjectCopy = Instantiate(roomObject);

            // Kopyanýn pozisyonunu ve boyutunu deðiþtiriyoruz
            roomObjectCopy.transform.position = newRoomPosition; // Yeni pozisyona taþýyoruz
            roomObjectCopy.transform.localScale = roomScale; // Odayý küçültüyoruz
        }
        else
        {
            Debug.LogWarning("Room object not found!");
        }
        if (parentObjectForClone != null)
        {
            roomObjectCopy.transform.SetParent(parentObjectForClone.transform);
        }
    }

    public void ScaleBack()
    {
        if (roomObjectCopy != null)
        {
            objectToBecomeChild.transform.SetParent(roomObjectCopy.transform);
            roomObjectCopy.transform.localScale = roomScale1;
            objectToBecomeChild.transform.SetParent(null);
            roomObjectCopy.SetActive(false);
            //roomObject.SetActive(false);
            DisableAllMeshRenderers(roomObject);
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