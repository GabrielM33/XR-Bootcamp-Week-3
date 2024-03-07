using UnityEngine;

public class CardBoxInteraction2 : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject attachPoint;
    public GameObject player;
    public GameObject cardbox;
    
    public Renderer[] playerRenderers;
    
    private bool _playerWasVisible;
    private Vector3 _cameraOriginalPosition;

    private void Start() 
    {
        _cameraOriginalPosition = mainCamera.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerWasVisible = AreRenderersEnabled(playerRenderers);
            DisableRenderers(playerRenderers);
            
            Vector3 loweredPosition = _cameraOriginalPosition;
            loweredPosition.y *= 0.5f;
            mainCamera.transform.position = loweredPosition;
            
            if (attachPoint != null)
            {
                transform.SetParent(attachPoint.transform, true);
                transform.localPosition = Vector3.zero; 
                player.GetComponent<Collider>().enabled = false;
                cardbox.GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerWasVisible) {
                EnableRenderers(playerRenderers);
            }
            
            mainCamera.transform.position = _cameraOriginalPosition;
            
            player.GetComponent<Collider>().enabled = true;
            cardbox.GetComponent<Collider>().enabled = true;
        }
    }
    
    private void DisableRenderers(Renderer[] renderers)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
        }
    }

    private void EnableRenderers(Renderer[] renderers)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }
    }

    private bool AreRenderersEnabled(Renderer[] renderers) 
    {
        foreach (Renderer r in renderers) 
        {
            if (r.enabled) {
                return true;
            }
        }
        return false;
    }
}