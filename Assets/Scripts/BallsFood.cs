using TMPro;
using UnityEngine;

public class BallsFood : MonoBehaviour
{
    public int Value;
    public TextMeshPro PointsText;

    void Start()
    {
        Value = Random.Range(1, 5);
        PointsText.SetText(Value.ToString());
    }
}
