using UnityEngine;

public class Coin : MonoBehaviour
{
    public float spinSpeed = 50f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Blob>().coins.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }

}
