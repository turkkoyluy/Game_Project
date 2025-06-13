using UnityEngine;

public class CupboardTrigger : MonoBehaviour
{
    public TaskManager taskManager;

    void OnMouseDown()
    {
        taskManager.ShowTasksForToday();

    }
}
