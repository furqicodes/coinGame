using UnityEngine;

public class Coin : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Blob>().coins.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }

}
