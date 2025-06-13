using UnityEngine;

public class Collector : MonoBehaviour
{
    // Karakterin sahip olduğu bir Collider bileşeni olmalı (genellikle Capsule Collider).
    // Bu Collider, "Is Trigger" olarak işaretlenmiş olmalı.

    private void OnTriggerEnter(Collider other)
    {
        // Temas eden objenin Tag'ini kontrol ediyoruz.
        // "Trash" yerine çöplere verdiğin Tag adını yazmalısın.
        if (other.CompareTag("Trash"))
        {
            // Çöp objesini yok et.
            Destroy(other.gameObject);
            Debug.Log("Çöp toplandı: " + other.name);

            // İsteğe bağlı: Görev sayacını artırabilirsin veya ses çalabilirsin.
            // Örneğin: GameManager.instance.IncreaseTrashCount();
        }
    }
}