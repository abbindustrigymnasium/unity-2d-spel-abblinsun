using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotationOffset;
    [SerializeField] private GameObject player;
    void Start()
    {
        rotationOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.transform.localScale.x == -1){
            rotationOffset = 180;
        }
        else{
            rotationOffset = 0;
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        


        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0, angle + rotationOffset));

    }
}
