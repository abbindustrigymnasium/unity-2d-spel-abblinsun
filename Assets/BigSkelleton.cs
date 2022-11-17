using UnityEngine;

public class BigSkelleton : MonoBehaviour
{

    private Rigidbody2D _enemyRigidbody;
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    public Transform WallCheck;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {

        _enemyRigidbody = GetComponent<Rigidbody2D>();
        if (_enemyRigidbody == null)
        {
            Debug.LogError("Enemy is missing a Rigidbody2D component");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale.x == -1.5f)
        {
            Movement(1);
        }
        else
        {
            Movement(-1);
        }

        if (Wall())
        {
            if (gameObject.transform.localScale.x == -1.5f)
            {
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            }
        }

    }
    void Movement(int i)
    {
        _enemyRigidbody.velocity = new Vector2(i * speed, _enemyRigidbody.velocity.y);
    }
    private bool Wall()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.4f, GroundLayer);
    }

}
