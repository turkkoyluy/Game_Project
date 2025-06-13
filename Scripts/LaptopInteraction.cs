using UnityEngine;

public class LaptopInteraction : MonoBehaviour
{
    public CVPanelController panelController;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("laptop")) // Tag büyük ihtimalle "Laptop" olmalı!
                {
                    Debug.Log("Laptop'a tıklandı!");
                    panelController.TogglePanel();
                }
            }
        }
    }
}

