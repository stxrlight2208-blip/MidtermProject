using UnityEngine;

public class GravityGunFull : MonoBehaviour
{
    [Header("Setting")]
    public float forcePower = 40f;
    public float range = 100f;
    public Camera cam;

    [Header("Hold Settings")]
    public float holdDistance = 3f;
    public float holdForce = 50f;

    [Header("Effects")]
    public ParticleSystem shootEffect;
    public LineRenderer line;

    Rigidbody heldObject;

    void Start()
    {
        if (line != null)
        {
            line.positionCount = 2;
            line.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ยิง (เหมือนเดิม)
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // กดขวา = หยิบ
        if (Input.GetMouseButtonDown(1))
        {
            TryGrab();
        }

        // ปล่อยเมาส์ขวา = ปล่อยของ
        if (Input.GetMouseButtonUp(1))
        {
            Release();
        }

        // ถ้าถืออยู่ → ดึงให้ลอยค้าง
        if (heldObject != null)
        {
            HoldObject();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Vector3 endPoint = ray.origin + ray.direction * range;

        if (Physics.Raycast(ray, out hit, range))
        {
            endPoint = hit.point;

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(ray.direction * forcePower * rb.mass, ForceMode.Impulse);
            }
        }

        PlayEffects(endPoint);
    }

    void TryGrab()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                heldObject = rb;

                // ปิดแรงโน้มถ่วง
                heldObject.useGravity = false;

                // รีเซ็ตความเร็ว (สำคัญมาก)
                heldObject.linearVelocity = Vector3.zero;
                heldObject.angularVelocity = Vector3.zero;

                // ทำให้นิ่ง
                heldObject.linearDamping = 20f;
                heldObject.angularDamping = 10f;

                // กันหมุนมั่ว
                heldObject.freezeRotation = false; // เปลี่ยนเป็น true ถ้ายังหมุนแรงเกิน
            }
        }
    }
    void HoldObject()
    {
        Vector3 targetPos = cam.transform.position + cam.transform.forward * holdDistance;

        Vector3 forceDir = targetPos - heldObject.position;

        // คำนวณแรงแบบสมูท
        Vector3 desiredVelocity = forceDir * holdForce;

        // ลดความแรงไม่ให้กระชาก
        Vector3 smoothVelocity = Vector3.Lerp(heldObject.linearVelocity, desiredVelocity, 0.2f);

        heldObject.linearVelocity = smoothVelocity;
    }
    void Release()
    {
        if (heldObject != null)
        {
            heldObject.useGravity = true;
            heldObject.linearDamping = 0f;
            heldObject = null;
        }
    }

    void PlayEffects(Vector3 endPoint)
    {
        if (shootEffect != null)
        {
            shootEffect.Play();
        }

        if (line != null)
        {
            StartCoroutine(ShowLine(endPoint));
        }
    }

    System.Collections.IEnumerator ShowLine(Vector3 endPoint)
    {
        line.enabled = true;
        line.SetPosition(0, cam.transform.position);
        line.SetPosition(1, endPoint);

        yield return new WaitForSeconds(0.05f);

        line.enabled = false;
    }

    void OnGUI()
    {
        float size = 10f;
        float posX = Screen.width / 2;
        float posY = Screen.height / 2;

        GUI.Label(new Rect(posX - size / 2, posY - size / 2, size, size), "+");
    }
}