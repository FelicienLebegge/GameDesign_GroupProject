using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class DrawCut : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;

    private LineRenderer cutRender;
    private bool animateCut;
    private bool _mouseButtonReleased;

    Camera cam;

    void Start() 
    {
        cam = FindObjectOfType<Camera>();
        cutRender = GetComponent<LineRenderer>();
        cutRender.startWidth = .05f;
        cutRender.endWidth = .05f;
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            pointA = new Vector3(Screen.width, Screen.height / 2, cam.ScreenToWorldPoint(mouse).z);
            pointB = new Vector3(0, Screen.height / 2, cam.ScreenToWorldPoint(mouse).z);
            cutRender.startColor = Color.gray;
            cutRender.endColor = Color.gray;
            CreateSlicePlane();
            cutRender.positionCount = 2;
            cutRender.SetPosition(0,pointA);
            cutRender.SetPosition(1,pointB);
            _mouseButtonReleased = false;
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            _mouseButtonReleased = true;
        }

        if (animateCut)
        {
            cutRender.SetPosition(0,Vector3.Lerp(pointA,pointB,1f));
            animateCut = false;
        }
    }

    void CreateSlicePlane() 
    {
        Vector3 pointInPlane = (pointA + pointB) / 2;
        
        Vector3 cutPlaneNormal = Vector3.Cross((pointA-pointB),(pointA-cam.transform.position)).normalized;
        Quaternion orientation = Quaternion.FromToRotation(Vector3.up, cutPlaneNormal);
        
        var all = Physics.OverlapBox(pointInPlane, new Vector3(100, 0.01f, 100), orientation);
        foreach (var hit in all)
        {
            MeshFilter filter = hit.gameObject.GetComponentInChildren<MeshFilter>();
            if(filter != null)
            {
                Cutter.Cut(hit.gameObject, pointInPlane, cutPlaneNormal);
            }
        }
    }
}
