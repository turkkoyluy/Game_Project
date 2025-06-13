using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private bool wasMoving = false; // Önceki hareket durumu

    // Yeni eklenen kodlar
    [Header("Oturma Sistemi")] // Inspector'da görsel ayrım için
    public Transform seatTarget; // Koltuğun oturma noktası (boş bir GameObject oluşturacağız)
    public float sitDistance = 1.5f; // Oturmak için koltuğa ne kadar yaklaşması gerektiği

    private bool isSitting = false; // Karakterin oturup oturmadığını takip eder
    // Yeni eklenen kodlar bitti

    void Start()
    {
        animator = GetComponent<Animator>();

        // Yeni eklenen kodlar
        if (animator == null)
        {
            Debug.LogError("Animator bileşeni bulunamadı! Lütfen karakterinize bir Animator ekleyin.");
        }

        if (seatTarget == null)
        {
            Debug.LogWarning("Seat Target (koltuk oturma noktası) atanmamış! Oturma kontrolü çalışmayabilir.");
        }
        // Yeni eklenen kodlar bitti
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(moveX) > 0.01f || Mathf.Abs(moveZ) > 0.01f;

        // Yeni eklenen kodlar: Oturma/Ayağa Kalkma Kontrolü
        if (Input.GetKeyDown(KeyCode.Z)) // Z tuşuna basıldı mı?
        {
            if (!isSitting) // Eğer şu an oturmuyorsa
            {
                // Koltuk hedefine yeterince yakın mı kontrol et
                if (seatTarget != null && Vector3.Distance(transform.position, seatTarget.position) <= sitDistance)
                {
                    SitDown(); // Oturma fonksiyonunu çağır
                }
                else
                {
                    Debug.Log("Koltuk hedefine yeterince yakın değilsin! Oturmak için yaklaşmalısın.");
                }
            }
            else // Eğer şu an oturuyorsa
            {
                StandUp(); // Ayağa kalkma fonksiyonunu çağır
            }
        }
        // Yeni eklenen kodlar bitti

        // Yalnızca karakter hareket etmiyorsa veya oturmuyorsa yürüme/idle animasyonlarını kontrol et
        if (!isSitting) // Karakter oturmadığı sürece hareket animasyonları çalışsın
        {
            if (animator != null)
            {
                if (isMoving && !wasMoving)
                {
                    animator.ResetTrigger("Idle");
                    animator.SetTrigger("Walk");
                }
                else if (!isMoving && wasMoving)
                {
                    animator.ResetTrigger("Walk");
                    animator.SetTrigger("Idle");
                }
            }

            // Hareket ettir (Karakter oturmadığı sürece hareket edebilir)
            if (isMoving)
            {
                Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                }
            }
        }
        // Yeni eklenen kodlar: Karakter oturuyorsa hareket etmesin
        else
        {
            // Eğer karakter oturuyorsa, hareket girdilerini sıfırla
            // Bu, otururken yürümeye başlamasını engeller.
            if (animator != null)
            {
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Idle"); // Otururken Idle animasyonu göstermesi beklenmez,
                                             // ama yürümeyi durdurur. Asıl animasyon Animator'de yönetilir.
            }
        }
        // Yeni eklenen kodlar bitti

        wasMoving = isMoving;
    }

    // Yeni eklenen kodlar: Oturma fonksiyonu
    void SitDown()
    {
        if (animator != null)
        {
            // Hareket animasyonlarını sıfırla
            animator.ResetTrigger("Walk");
            animator.ResetTrigger("Idle");

            // Oturma animasyonunu tetikle
            animator.SetTrigger("SitTrigger");
            isSitting = true;

            // Karakteri koltuğun tam oturma noktasına ışınla veya yavaşça taşı
            // Bu satırları aktif edersen karakter aniden koltuğa ışınlanır.
            // Eğer CharacterController veya Rigidbody kullanıyorsan, bu ışınlama fizik motoruyla çakışabilir.
            // transform.position = seatTarget.position;
            // transform.rotation = seatTarget.rotation; // Koltuğun yönüne dön

            Debug.Log("Oturma animasyonu tetiklendi.");
        }
    }

    // Yeni eklenen kodlar: Ayağa kalkma fonksiyonu
    void StandUp()
    {
        if (animator != null)
        {
            // Animator'daki "StandTrigger" parametresini tetikle (Animator'da bu Trigger'ı oluşturmalısın!)
            animator.SetTrigger("StandTrigger");
            isSitting = false;
            Debug.Log("Ayağa kalkma animasyonu tetiklendi.");
        }
    }
    // Yeni eklenen kodlar bitti
}