using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
   
    public TextMeshPro PointsText;
    public int Value;
    Color lerpedColor = Color.white;

    void Start()
    {
        Value = Random.Range(1, 7);
        PointsText.SetText(Value.ToString());
        lerpedColor = Color.Lerp(Color.white, Color.blue, (float)Value / 4f);
        this.GetComponent<Renderer>().material.color = lerpedColor;
    }
}
