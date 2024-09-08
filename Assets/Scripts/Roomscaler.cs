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
    //private GameObject roomObjectCopy = null;

   

    void Start()
    {
        
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
            
            //roomObjectCopy = Instantiate(roomObject);

            
            //roomObjectCopy.transform.position = newRoomPosition;
            //roomObjectCopy.transform.localScale = roomScale;
            roomObject.transform.position = newRoomPosition;
            roomObject.transform.localScale = roomScale;
        }
        else
        {
            Debug.LogWarning("Room object not found!");
        }
        objectToBecomeChild.transform.SetParent(roomObject.transform);
        //roomObject.SetActive(false);
        // Kopyayý bir parent objeye eklemek isterseniz
        //if (parentObjectForClone != null && roomObject != null)
        //{
        //    roomObject.transform.SetParent(parentObjectForClone.transform);
        //}
    }

    public void ScaleBack()
    {
        if (roomObject != null)
        {
            // roomObjectCopy'nin GameObject bileþenine eriþiyoruz
            
            roomObject.transform.localScale = roomScale1;
            objectToBecomeChild.transform.SetParent(null);
            roomObject.gameObject.SetActive(false); 
            //DisableAllMeshRenderers(roomObjectCopy.gameObject);
        }
        else
        {
            Debug.LogWarning("Room object copy not found!");
        }
    }

    //public void DisableAllMeshRenderers(GameObject parentObject)
    //{
    //    // Ýlk önce parent objedeki MeshRenderer'ý kontrol edelim.
    //    MeshRenderer meshRenderer = parentObject.GetComponent<MeshRenderer>();
    //    if (meshRenderer != null)
    //    {
    //        meshRenderer.enabled = false;
    //    }

    //    // Þimdi child objeleri üzerinde iþlem yapalým.
    //    foreach (Transform child in parentObject.transform)
    //    {
    //        DisableAllMeshRenderers(child.gameObject);
    //    }
    //}
}
