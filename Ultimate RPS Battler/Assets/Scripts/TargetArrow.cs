using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetArrow : MonoBehaviour
{

    public GameObject arrowHeadPref, arrowNodePref;

    public int arrowNodeNum = 5;

    public float scaleFactor = 1f;


    private RectTransform origin;

    [SerializeField]
    private List<RectTransform> arrowNodes = new List<RectTransform>();

    private List<Vector2> controlPoints = new List<Vector2>();

    private readonly List<Vector2> controlPointFactors = new List<Vector2> { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };

    private void Awake()
    {
        origin = GetComponent<RectTransform>();

        for (int i = 0; i < arrowNodeNum; i++)
        {
            arrowNodes.Add(Instantiate(arrowNodePref, transform).GetComponent<RectTransform>());
        }

        arrowNodes.Add(Instantiate(arrowHeadPref, transform).GetComponent<RectTransform>());

        //Hide arrow nodes
        arrowNodes.ForEach(a => a.GetComponent<RectTransform>().position = new Vector2(-10000, -10000));

        //Initialize the control point list
        for (int i = 0; i < 4; i++)
        {
            controlPoints.Add(Vector2.zero);
        }
    }


    private void Update()
    {

        //WARNING LOTS OF MATH MAGIC AHEAD

        //P0 is is the origin
        controlPoints[0] = new Vector2(origin.position.x, origin.position.y);

        //P3 is end pos also in this case mouse pos

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        controlPoints[3] = new Vector2(mousePos.x, mousePos.y);

        //P1, P2 determines  by P0 and P3
        //P1 = P0 + (P3 - P0) * controlPointFactors[0]
        //P2 = P0 + (P3 - P0) * controlPointFactors[1]

        controlPoints[1] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[0];
        controlPoints[2] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[1];

        for (int i = 0; i < arrowNodes.Count; i++)
        {
            //Calculate t
            var t = Mathf.Log(1f * i / (arrowNodes.Count - 1) + 1f, 2f);

            //Cubic Bezier curve
            //Aka math magic
            arrowNodes[i].position =
                Mathf.Pow(1 - t, 3) * controlPoints[0] +
                3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] +
                3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2] +
                Mathf.Pow(t, 3) * controlPoints[3];


            //calculates arrow node rotations
            if (i > 0)
            {
                var euler = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, arrowNodes[i].position - arrowNodes[i - 1].position));
                arrowNodes[i].rotation = Quaternion.Euler(euler);
            }

            //Calculate arrow node scale
            var scale = scaleFactor * (1f - 0.03f * (arrowNodes.Count - 1 - i));
            arrowNodes[i].localScale = new Vector3(scale, scale, 1);

        }

        //the arrow head rotation
        arrowNodes[0].transform.rotation = arrowNodes[1].transform.rotation;


    }

    public void ArrowVisability(bool isVisable)
    {
        for (int i = 0; i < arrowNodes.Count; i++)
        {
            arrowNodes[i].GetComponent<Image>().enabled = isVisable;
        }
    }


}
