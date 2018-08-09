using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum State
    {
        normal,
        dash
    }
    State state = new State();

    public float normalSpeed = 6f;
    private float speed ;
    private Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    private int floorMask;
    private int shootAbleMask;
    private float camRayLength = 100f;
    float h;
    float v;

    bool dashEntry = false;
    // 儲存玩家與滑鼠座標的向量
    Vector3 playerToMouse = new Vector3(0,0,0);

    // 衝刺方向
    Vector3 dashDirection = new Vector3(0, 0, 0);



    private bool isDashing = false;
    public float dashDuration;
    private float dashTime ;
    public float dashSpeed;
    private void Awake()
    {
        speed = normalSpeed;
        floorMask = LayerMask.GetMask("Floor");
        shootAbleMask = LayerMask.GetMask("Shootable");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        dashTime = dashDuration;
        state = State.normal;

        
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.normal:
                {

                    h = Input.GetAxisRaw("Horizontal");
                    v = Input.GetAxisRaw("Vertical");

                    Move(h, v);
                    Turn();
                    Animating(h, v);
                    
                    break;
                }

            case State.dash:
                {
                    Turn();
                    // 給予衝刺時間限制
                    dashTime -= Time.deltaTime;

                    // 進入衝刺時提高速度
                    if (dashEntry == false)
                    {
                        dashDirection = playerToMouse;
                        speed = dashSpeed;
                        dashEntry = true;

                    }

                    // 判定衝刺方向是否有障礙物 起點為角色衝刺的方向0.1單位(如果設在角色中間會導致背對障礙的時候也卡住
                    bool dashIntoWall = Physics.Raycast(transform.position + (dashDirection.normalized*0.1f), dashDirection.normalized, 2.0f,shootAbleMask);

                    if (dashIntoWall)
                    {
                        speed = 0;
                        
                    }

                    // 移動角色 速度值會遞減
                    speed = Mathf.Lerp(speed,0,0.2f);
                    movement = dashDirection.normalized * speed * Time.deltaTime;
                    playerRigidbody.MovePosition(transform.position + movement);

                    // 衝刺結束後將數值回歸正常
                    if (dashTime <= 0)
                    {
                        dashTime = dashDuration;
                        dashEntry = false;
                        speed = normalSpeed;
                        state = State.normal;
                    }

                    break;
                }
        }
    }

    private void Update()
    {
        Dash();

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
            playerToMouse = floorHit.point - transform.position;

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

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.dash;
        }



    }
}
