using UnityEngine;

public class HoneyHiveThings : MonoBehaviour
{
    public GameObject Honey;
    public float Speed = 125f;
    private bool MoveTo = true; // true = right, flase = left
    public float RightEndPoint, LeftEndPoint;
    public float HoneyMaxDropTime = 5f, HoneyMinDropTime = 3f;
    public float Timer;
    private float direction = 1;
    private RectTransform rectTransform;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Timer = Random.value * (HoneyMaxDropTime - HoneyMinDropTime) + HoneyMinDropTime;
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Instantiate(Honey, new Vector3(transform.position.x,transform.position.y), Quaternion.identity).transform.SetParent(transform.parent);
            Timer = Random.value * (HoneyMaxDropTime - HoneyMinDropTime) + HoneyMinDropTime;
        }

        rectTransform.Translate(direction * Speed * Time.deltaTime * CharacterThings.speedMultiplier, 0, 0);
        
        if ((rectTransform.localPosition.x > RightEndPoint && direction == 1) || (rectTransform.localPosition.x < LeftEndPoint) && direction == -1) {
            direction = -direction;
        }
        
    }
}
