using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    private Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();

        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turn();
        Animating(h, v);
        Dash(h,v);
    }

    private void Move(float h, float v)
    {
        //movement.Set(h, 0.0f, v);

        // 接收玩家的輸入判斷移動方向並標準化避免影響速度
        movement = new Vector3(h, 0.0f, v).normalized;

        // 給予希望的速度
        movement *= speed * Time.deltaTime;

        // 時祭儀動完加 <-- 對我的輸入法就是這麼扯
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turn()
    {
        // 先定義射線的起點與終點
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 用於儲存擊中資訊
        RaycastHit floorHit;

        // 判斷式會回傳布林 如果在條件內有擊中物體為true
        if (Physics.Raycast(cameraRay, out floorHit, camRayLength, floorMask))
        {
            // 取得玩家與轉向目標點的向量
            Vector3 playerToMouse = floorHit.point - transform.position;

            // 忽略y軸
            playerToMouse.y = 0.0f;

            // 將玩家與轉向目標點的向量轉為面向目標的四元數
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // 使用此函示實際旋轉玩家
            playerRigidbody.MoveRotation(newRotation);

        }
    }

    public void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0;
        anim.SetBool("IsWalking", walking);

    }

    private void Dash(float h, float v)
    {
        if (Input.GetKey(KeyCode.V))
        {
            
        }
    }
}
