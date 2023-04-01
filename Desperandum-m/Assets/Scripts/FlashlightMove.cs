using UnityEngine;

public class FlashlightMove : MonoBehaviour
{
    //rotation
    private Camera mainCam;

    private Vector3 mousePos;

    // Start is called before the first frame update
    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotZ = rotZ - 90f; // Fixuje Invertaci svìtla pøi flipu I divnou chybu pøi zmìnì z Freeform na Spotlight
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}