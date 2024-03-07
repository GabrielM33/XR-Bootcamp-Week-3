using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName; 

    private Transform playerCamera; // Reference to the player's camera
    private bool isEquipped = false;
    public float xOffset = 0.2f;
    public float yOffset = -0.1f;
    public float zOffset = 0.5f;

    void Start()
    {
        playerCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isEquipped) 
        {
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.F) && isEquipped)
        {
            Drop();
        }
    }

    public void Equip()
    {
        transform.SetParent(playerCamera);
        transform.localPosition = new Vector3(xOffset, yOffset, zOffset); 
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true; // Disable physics
        GetComponent<Collider>().enabled = false;  // May want to disable temporarily
        isEquipped = true;
    }

    void Drop()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false; 
        GetComponent<Collider>().enabled = true;  
        isEquipped = false;
    }
}