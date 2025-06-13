using UnityEngine;

public class PaperTrigger : MonoBehaviour
{
    public CaseManager caseManager;

    void OnMouseDown()
    {
        caseManager.OpenCases();
    }
}
