using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityCustomAirResis : MonoBehaviour
{
    public float gravity = -9.81f;
    public float airResistance = 0.98f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ปิด gravity ปกติ
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        // ใส่แรงโน้มถ่วงเอง
        rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        // ใส่แรงต้านอากาศ
        rb.linearVelocity *= airResistance;
    }
}