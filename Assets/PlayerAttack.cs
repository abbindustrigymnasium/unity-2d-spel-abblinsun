using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos1;
    public Transform attackPos2;
    public float attackRange;
    public LayerMask enemy;
    public LayerMask ground;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0 && Input.GetKeyDown(KeyCode.Mouse0)){
            animator.SetTrigger("Slash");
            timeBtwAttack = startTimeBtwAttack;
                if(checkForHit(enemy) || checkForHit(ground)){
                    _playerRigidbody.GetComponent<Movement>().attackHit();
                }
        }else{
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private bool checkForHit(LayerMask target){
        if(Physics2D.OverlapCircle(attackPos1.position, attackRange, target) || Physics2D.OverlapCircle(attackPos2.position, attackRange, target)){
            return true;
        }else{
            return false;
        }
            
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPos1.position, attackRange);
        Gizmos.DrawWireSphere(attackPos2.position, attackRange);
    }
}
