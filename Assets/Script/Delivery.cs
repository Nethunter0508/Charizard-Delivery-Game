using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage;
    [SerializeField] float destroyDelay = 0.5f;
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.CompareTag("Pokeball") && !hasPackage)
        {
            Debug.Log("Pokeball Picked Up");
            hasPackage = true;
            GetComponent<ParticleSystem>().Play();
            Destroy(collision.gameObject, destroyDelay);
        }
        if(collision.CompareTag("Pokemon") && hasPackage)
        {
            Debug.Log("Pokeball Delivered");
            hasPackage = false;
            GetComponent<ParticleSystem>().Stop();
            Destroy(collision.gameObject);
        }
        
    }
}
