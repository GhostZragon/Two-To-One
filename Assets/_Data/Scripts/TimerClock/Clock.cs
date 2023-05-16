using UnityEngine;

public class Clock : MonoBehaviour
{
    Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        Show();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Show()
    {
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one, 1).setEaseOutBack();
    }
}
