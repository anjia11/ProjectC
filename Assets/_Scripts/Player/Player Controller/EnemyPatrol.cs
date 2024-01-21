using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] points;
    private int currentPoint;
    public float moveSpeed;
    public float waitAtPoint;
    private float waitCounter;
    public float jumpForce;
    [SerializeField] Rigidbody2D _rigidbody2D;

    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
        if (points != null)
        {
            foreach (Transform pPoint in points)
            {
                pPoint.SetParent(null);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - points[currentPoint].position.x) > .2)
        {
            if (transform.position.x < points[currentPoint].position.x)
            {
                _rigidbody2D.velocity = new Vector2(moveSpeed, _rigidbody2D.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(-moveSpeed, _rigidbody2D.velocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < points[currentPoint].position.y && _rigidbody2D.velocity.y < 0.1f)
            {
                _rigidbody2D.velocity = new Vector2(transform.position.x, jumpForce);
            }
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);

            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waitCounter = waitAtPoint;
                currentPoint++;
                if (currentPoint >= points.Length)
                {
                    currentPoint = 0;
                }
            }
        }

        anim.SetFloat("speed", Mathf.Abs(_rigidbody2D.velocity.x));
    }
}