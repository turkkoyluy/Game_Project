using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    public GameObject targetCanvas; // Canvas'ı buraya sürükle
    private bool isCanvasActive = false;

    void Start()
    {
        if (targetCanvas != null)
            targetCanvas.SetActive(false); // Başlangıçta kapalı
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol tıklama
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    ToggleCanvas();
                }
            }
        }
    }

    void ToggleCanvas()
    {
        if (targetCanvas != null)
        {
            isCanvasActive = !isCanvasActive;
            targetCanvas.SetActive(isCanvasActive);
        }
    }
}
