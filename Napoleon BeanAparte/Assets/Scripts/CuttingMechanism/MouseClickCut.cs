using UnityEngine;

public enum Angle
{
    Up,
    Forward,
    Right
}
public class MouseClickCut : MonoBehaviour
{
    public Angle angle;

    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out hit, 3, 1 << 2) && hit.transform.tag == "Bean")
            {
                Debug.Log("Object can be cut");

                GameObject victim = hit.collider.gameObject;
                if (victim.tag == "Bean")
                {

                    if (angle == Angle.Up)
                    {
                        Cutter.Cut(victim, hit.point, Vector3.up);

                    }
                    else if (angle == Angle.Forward)
                    {
                        Cutter.Cut(victim, hit.point, Vector3.forward);

                    }
                    else if (angle == Angle.Right)
                    {
                        Cutter.Cut(victim, hit.point, Vector3.right);

                    }
                }
            }
        }
    }
}
