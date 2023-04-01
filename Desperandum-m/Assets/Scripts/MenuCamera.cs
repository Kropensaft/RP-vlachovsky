using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform cTransform;
    private float foo;
    public new Camera camera;

    //public Vector3 startPos;
    //public Vector3 endPos;
    public int time;

    private void Start()
    {
        foo = cTransform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        foo += (time * Time.deltaTime);

        camera.transform.position = new Vector3(foo, cTransform.position.y, cTransform.position.z);

        //if (cTransform.position.x >= endPos.x)
        //{
        //    cTransform.position = new Vector3(startPos.x, startPos.y, startPos.z);
        //
        //}
    }
}