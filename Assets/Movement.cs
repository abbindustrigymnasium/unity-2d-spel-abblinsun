using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    public float JumpPower = 5.0f;
    public LayerMask groundLayer;
    public Transform groundChack;
    private Rigidbody2D _playerRigidbody;
    private bool Grounded; 
    public float attackPower;
    public Animator animator; 
    private void Start()
    {

        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }
    private void Update()
    {
        
        MovePlayer();
        GetVelocity();
        

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Grounded = false;
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.position.x, JumpPower);
        }
        if (isGrounded())
        {
            Grounded = true;
        }
        
    }
    private void MovePlayer()
    {

        var horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0){
            gameObject.transform.localScale = new Vector3(-1,1,1);
            animator.SetBool("Running", true);
        }
        if (horizontalInput>0){
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("Running", true);
        }
        if(horizontalInput == 0){
            animator.SetBool("Running", false);
        }
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }
    
    private bool isGrounded()
    {   
        animator.SetBool("Grounded", Physics2D.OverlapCircle(groundChack.position, 0.4f, groundLayer));
        return Physics2D.OverlapCircle(groundChack.position, 0.4f, groundLayer);
    }
    public void attackHit(){
        Vector3 mousePos = Input.mousePosition;
        float xDist = mousePos.x - Camera.main.WorldToScreenPoint(transform.position).x;
        float yDist = mousePos.y - Camera.main.WorldToScreenPoint(transform.position).y;
        float absYDist = Mathf.Abs(yDist);
        float absXDist = Mathf.Abs(xDist);

        float yF = attackPower * Mathf.Sin(Mathf.Atan(absYDist/absXDist));
        float xF = attackPower * Mathf.Cos(Mathf.Atan(yDist / xDist))*10;
        if (xDist > 0){
            xF = xF * -1;
        }
        if (yDist > 0)
        {
            yF = yF * -1;
        }
        Debug.Log("y" + yF);
        Debug.Log("x" + xF);
        
        _playerRigidbody.velocity = new Vector2(0, 0);
        StartCoroutine(smoothForce(xF, yF));
            
        
        Grounded = true;
        Debug.DrawLine(Camera.main.WorldToScreenPoint(transform.position), new Vector3(xF, yF, 0), Color.cyan, 1.0f, false);

    }
    private IEnumerator smoothForce(float x, float y){ 
        for (int i = 0; i <= 10; i ++){
            _playerRigidbody.AddForce(new Vector3(x, y, 0));
            yield return new WaitForSeconds(.01f);
        }
    }
    private void GetVelocity(){
        if(_playerRigidbody.velocity.y >= 0){
            animator.SetBool("Falling", false);
        }else{
            animator.SetBool("Falling",true);
        }
    }
    private void die(){
        gameObject.transform.position = new Vector3(0,0,0);
        }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy")){
            die();
        }
    }
}

   



