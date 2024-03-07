using UnityEngine;

public class StickyMine : MonoBehaviour
{
    public GameObject enemyRobot;
    public GameObject explosionEffect;
    public AudioClip explosionAudio;
    public Rigidbody[] ragdollRigidbodies;

    void Start()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }
    
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit) &&
            hit.collider.gameObject == enemyRobot)
        {
            Explode();
        }
    }
    
    private void Explode()
    {
        AudioSource.PlayClipAtPoint(explosionAudio, transform.position);
        
        explosionEffect.SetActive(true);
        explosionEffect.GetComponent<ParticleSystem>().Play();
        
        Ragdoll();
        
        Destroy(gameObject);
        
        Debug.Log("Mine Exploded");
    }
    
    private void Ragdoll()
    {
        foreach (var part in ragdollRigidbodies)
        { 
            part.isKinematic = false;
        }
        
        Debug.Log("Ragdolled");
    }


    public void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * 5f);
        }
}