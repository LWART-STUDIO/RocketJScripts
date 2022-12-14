using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRendererComponent;
    

    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        lineRendererComponent.enabled = true;
        Vector3[] points = new Vector3[5];
        lineRendererComponent.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;

            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;

            /*if(points[i].y < origin.y)
            {
                lineRendererComponent.positionCount = i+1;
                break;
            }*/
        }

        lineRendererComponent.SetPositions(points);
    }

    public void HideTrajectory()
    {
        lineRendererComponent.enabled = false;
    }
}