using UnityEngine;

public class SitController : MonoBehaviour
{
    public Transform sitTarget;
    private bool isNear = false;

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            Transform player = GameObject.FindWithTag("Player").transform;
            player.position = sitTarget.position;
            player.rotation = sitTarget.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isNear = false;
    }
}
