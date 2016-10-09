using UnityEngine;
using HoloToolkit.Unity;

/// <summary>
/// provide a tip text of current measure mode
/// </summary>
public class ModeTip : Singleton<ModeTip>
{
    private const string LineMode = "Line Mode";
    private const string PloygonMode = "Geometry Mode";
    private const string PointMode = "Point Mode";
    private const string OriginMode = "Origin Mode";

    private TextMesh text;
    private int fadeTime = 100;

    void Start()
    {
        text = GetComponent<TextMesh>();
        switch (MeasureManager.Instance.mode)
        {
            case GeometryMode.Line:
                text.text = LineMode;
                break;
            case GeometryMode.Ploygon:
                text.text = PloygonMode;
                break;
            case GeometryMode.Origin:
                text.text = OriginMode;
                break;
            default:
                text.text = PointMode;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            // if you want log the position of mode tip text, just uncomment it.
            // Debug.Log("pos: " + gameObject.transform.position);
            switch (MeasureManager.Instance.mode)
            {
                case GeometryMode.Line:
                    if (!text.text.Contains(LineMode))
                        text.text = LineMode;
                    break;
                case GeometryMode.Ploygon:
                    if (!text.text.Contains(PloygonMode))
                        text.text = PloygonMode;
                    break;
                case GeometryMode.Origin:
                    if (!text.text.Contains(OriginMode))
                        text.text = OriginMode;
                    break;
                default:
                    if (!text.text.Contains(PointMode))
                        text.text = PointMode;
                    break;
            }

            var render = GetComponent<MeshRenderer>().material;
            fadeTime = 100;
            // fade tip text
            if (fadeTime == 0)
            {
                var color = render.color;
                fadeTime = 100;
                color.a = 1f;
                render.color = color;
                gameObject.SetActive(false);
            }
            else
            {
                var color = render.color;
                color.a -= 0.01f;
                render.color = color;
                fadeTime--;
            }
        }
    }
}
