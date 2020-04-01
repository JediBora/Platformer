using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerParams))]
public class PlayerMovement : MonoBehaviour
{

    //Ray stuffs
    private RaycastHit2D hit;

    private float wallMargin = 0.1f;

    //Player states
    public bool jumpPress = false;
    private bool movePress = false;

    //current states
    private float currentSpeed = 0.0f;
    private float currentGravity = 0.0f;
    private float currentJumpSpeed = 0.0f;
    private float currentWallJumpSpeed = 0.0f;
    private float currentAcc = 0.0f;
    private float currentJumpCount = 0;

    private float gravity = 0;

    private Vector2 prevDirection;

    private float currentJumpInterval;
    private bool canJump = true;

    //Refs
    private PlayerParams playerParams;

    private Transform playerTransform;
    private Transform platform;
    private Transform prevPlatform;

    public SpriteRenderer mySpriteRenderer;
    public SpriteRenderer myShotgunSpriteRenderer;
    public SpriteRenderer myRifleSpriteRenderer;
    public SpriteRenderer shotgunPallete;

    public Collider2D sPallete;
    public Collider2D rPallete;

    public GameObject box;
    public GameObject bandana;
    public GameObject topHat;
    public GameObject rifle;
    public GameObject shotgun;

    public GameObject spawnPos;
    public GameObject spawnPos1;

    public GameObject topHatHeart;
    public GameObject bandanaHeart;
    public int topHatHealth = 5;
    public int bandanaHealth = 3;

    public bool topHatActivated = false;
    public bool bandanaActivated = false;
   
    float x;
    void Awake()
    {

        currentSpeed = 0;
        playerParams = GetComponent<PlayerParams>();

        playerTransform = transform;
        prevDirection = PlayerStates.direction;
    }

    void OnDrawGizmos()
    {
        playerParams = GetComponent<PlayerParams>();
        Gizmos.DrawWireCube(transform.position, new Vector3(playerParams.playerWidth, playerParams.playerHeight, 1));
    }

    void Flip()
    {
        if (PlayerStates.moveRight)
        {
            mySpriteRenderer.flipX = true;
            myShotgunSpriteRenderer.flipX = false;
            myRifleSpriteRenderer.flipX = false;
            shotgunPallete.flipX = true;
            sPallete.offset = new Vector2(1.03f, 0);
            rPallete.offset = new Vector2(1.03f, 0);
        }
        else
        {
            mySpriteRenderer.flipX = false;
            myShotgunSpriteRenderer.flipX = true;
            myRifleSpriteRenderer.flipX = true;
            shotgunPallete.flipX = false;
            sPallete.offset = new Vector2(-0.93f, 0);
            rPallete.offset = new Vector2(-0.93f, 0);

        }




    }
    public void Gravity()
    {
        if (!PlayerStates.isJump)
        {

            if (PlayerStates.onJumpbleWall && !PlayerStates.onGround)
            {
                gravity = playerParams.wallFallSpeed;
            }
            else
            {

                gravity = playerParams.maxFallSpeed;
            }

            if (currentGravity < gravity)
            {
                currentGravity += playerParams.gravityAccelerate * Time.deltaTime;
            }
            else
            {
                currentGravity = gravity;
            }

            GroundCheker();

            if (!PlayerStates.detectGround)
            {

                playerTransform.Translate(0, -currentGravity * Time.deltaTime, 0);

                PlayerStates.isFalling = true;
                PlayerStates.onGround = false;
            }
            else
            {
                playerTransform.Translate(0, hit.point.y - (playerTransform.position.y - playerParams.playerHeight / 2), 0);
                PlayerStates.onGround = true;
                PlayerStates.isJump = false;
                PlayerStates.isFalling = false;
                currentGravity = 0.0f;
                currentJumpSpeed = 0.0f;
            }
        }
        else
        {
            PlayerStates.onGround = false;
        }
    }

    public void Moving()
    {

        prevDirection = PlayerStates.direction;

        //Handle Inputs************************
        if (Input.GetAxisRaw(playerParams.MovementAxis) > 0)
        {
            if (!playerParams.disableMove)
            {
                PlayerStates.moveRight = true;
                PlayerStates.isMoving = true;
                movePress = true;
            }
        }

        if (Input.GetAxisRaw(playerParams.MovementAxis) < 0)
        {
            if (!playerParams.disableMove)
            {
                PlayerStates.moveRight = false;
                PlayerStates.isMoving = true;
                movePress = true;
            }
        }

        if (Input.GetAxisRaw(playerParams.MovementAxis) == 0)
        {
            PlayerStates.isMoving = false;
            movePress = false;
            playerParams.disableMove = false;
        }

        //**************************************

        if (PlayerStates.detectGround)
        {
            currentAcc = playerParams.groundAccelerate;
        }
        else
        {
            currentAcc = playerParams.airAccelerate;
        }

        if (movePress && !playerParams.disableMove)
        {

            if (PlayerStates.moveRight)
            {
                PlayerStates.direction = Vector2.right;

                if (currentSpeed < playerParams.speed)
                {
                    currentSpeed += (currentAcc * Time.deltaTime);
                }
                if (currentSpeed > playerParams.speed)
                {
                    currentSpeed = playerParams.speed;
                }
            }

            if (!PlayerStates.moveRight)
            {
                PlayerStates.direction = -Vector2.right;

                if (currentSpeed < playerParams.speed)
                {
                    currentSpeed += (currentAcc * Time.deltaTime);
                }
                if (currentSpeed > playerParams.speed)
                {
                    currentSpeed = playerParams.speed;
                }
            }
        }

        if (!movePress)
        {

            if (currentSpeed > 0)
            {
                currentSpeed -= (currentAcc * Time.deltaTime);
            }
            else
            {
                currentSpeed = 0;
            }
        }

        if (PlayerStates.direction != prevDirection)
        {
            currentSpeed = 0.0f;
        }


        WallCheker(currentSpeed);


        if (PlayerStates.onWall)
        {
            currentSpeed = 0.0f;
            if (PlayerStates.moveRight)
            {
                playerTransform.Translate(hit.point.x - (playerTransform.position.x + ((playerParams.playerWidth / 2) + wallMargin)), 0, 0);
            }
            if (!PlayerStates.moveRight)
            {
                playerTransform.Translate(hit.point.x - (playerTransform.position.x - ((playerParams.playerWidth / 2) + wallMargin)), 0, 0);
            }

            else
            {

                if (!playerParams.disableMove)
                {
                    playerTransform.Translate(currentSpeed * PlayerStates.direction.x * Time.deltaTime, 0, 0);
                }
            }
        }
        else
        {
            if (!playerParams.disableMove)
            {
                playerTransform.Translate(currentSpeed * PlayerStates.direction.x * Time.deltaTime, 0, 0);
            }
        }
    }


    //JUMP
    public void Jump()
    {

        if (PlayerStates.detectGround)
        {
            PlayerStates.isSecondJump = false;
            currentJumpCount = 0;
            currentWallJumpSpeed = 0;

            if (Timer())
            {
                canJump = true;
            }
        }

        //Check if  jump button not yet pressed
        if (!jumpPress)
        {

            //GROUND_JUMP
            if ((Input.GetAxisRaw(playerParams.JumpAxis) != 0) && !PlayerStates.isJump && PlayerStates.detectGround && canJump)
            {
                jumpPress = true;
                PlayerStates.onGround = false;
                PlayerStates.lastGroundPosition = PlayerStates.groundHitPoint;
                currentJumpCount += 1;
                PlayerStates.isJump = true;
                PlayerStates.detectGround = false;
                currentJumpSpeed = playerParams.jumpSpeed;
                canJump = false;
                currentJumpInterval = playerParams.jumpInterval;
            }


            //SECOND_JUMP
            if (playerParams.doubleJump)
            {//if Double jump allowed (see player params)
                if ((Input.GetAxis(playerParams.JumpAxis) != 0) && (currentJumpCount == 1) && !jumpPress && !PlayerStates.detectGround && !PlayerStates.onWall)
                {
                    jumpPress = true;
                    PlayerStates.isJump = true;
                    PlayerStates.isSecondJump = true;
                    currentJumpCount = 0;
                    currentGravity = 0.0f;
                    currentJumpSpeed = playerParams.jumpSpeed;
                    canJump = false;
                    currentJumpInterval = playerParams.jumpInterval;
                }
            }


            //WALL_JUMP
            if (playerParams.wallJump)
            {//if Wall jump allowed (see player params)
                if ((Input.GetAxis(playerParams.JumpAxis) != 0) && !PlayerStates.detectGround && PlayerStates.onJumpbleWall && !PlayerStates.isJump)
                {
                    PlayerStates.isJump = true;
                    PlayerStates.isWallJump = true;
                    currentGravity = 0.0f;
                    currentJumpSpeed = playerParams.jumpSpeed;
                    currentWallJumpSpeed = playerParams.wallJumpSpeed;
                    movePress = false;
                    playerParams.disableMove = true;
                    canJump = false;
                    currentJumpInterval = playerParams.jumpInterval;
                    currentSpeed = 0.0f;

                    if (PlayerStates.moveRight)
                    {
                        PlayerStates.direction = -Vector2.right;
                        PlayerStates.moveRight = false;
                    }
                    else
                    {
                        PlayerStates.direction = Vector2.right;
                        PlayerStates.moveRight = true;
                    }
                }
            }
        }

        //if we release jump button
        if (Input.GetAxisRaw(playerParams.JumpAxis) == 0)
        {
            jumpPress = false;
        }


        //Speed down ground jump power 
        if (currentJumpSpeed > 0)
        {
            currentJumpSpeed -= playerParams.jumpAccelerate * Time.deltaTime;
        }
        else
        {
            currentJumpSpeed = 0.0f;
            PlayerStates.isJump = false;
        }

        //Speed down wall jump power 
        if ((currentWallJumpSpeed > 0) && PlayerStates.isWallJump)
        {
            currentWallJumpSpeed -= playerParams.wallJumpAccelerate * Time.deltaTime;
        }
        else
        {
            currentWallJumpSpeed = 0.0f;
            PlayerStates.isWallJump = false;
        }

        //Function for detecting ceiling;
        ceilingCheck();


        //Translate character
        if (!PlayerStates.onCeiling)
        {
            playerTransform.Translate(0, currentJumpSpeed * Time.deltaTime, 0);
        }
        else
        {
            playerTransform.Translate(0, hit.point.y - (playerTransform.position.y + playerParams.playerHeight / 2), 0);
            PlayerStates.isJump = false;
            currentJumpCount = 0;
        }

        //Function for detecting walls;
        WallCheker(currentWallJumpSpeed);

        if (PlayerStates.isWallJump)
        {

            if (!PlayerStates.onWall)
            {
                playerTransform.Translate(currentWallJumpSpeed * PlayerStates.direction.x * Time.deltaTime, 0, 0);
            }
            else
            {
                currentWallJumpSpeed = 0;
                if (PlayerStates.moveRight)
                {
                    playerTransform.Translate(hit.point.x - (playerTransform.position.x + ((playerParams.playerWidth / 2) + wallMargin)), 0, 0);
                }

                if (!PlayerStates.moveRight)
                {
                    playerTransform.Translate(hit.point.x - (playerTransform.position.x - ((playerParams.playerWidth / 2) + wallMargin)), 0, 0);
                }
            }
        }
    }//END_OF_JUMP


    public bool Timer()
    {

        if (currentJumpInterval > 0)
        {
            currentJumpInterval -= Time.deltaTime;
            return false;
        }
        else
        {
            currentJumpInterval = 0;
            return true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!playerParams.disable)
        {
            Gravity();
            Moving();
            Jump();
            Flip();

        }
    }

    public void GroundCheker()
    {

        float originX = playerTransform.position.x - playerParams.playerWidth / 2;
        float originY = playerTransform.position.y - playerParams.playerHeight / 2;

        for (int i = 0; i < playerParams.rayCount + 1; i++)
        {

            hit = Physics2D.Raycast(new Vector2(originX, originY), -Vector2.up, currentGravity * Time.deltaTime, playerParams.surfaceLayer);

            Debug.DrawRay(new Vector2(originX, originY), -Vector2.up * currentGravity * Time.deltaTime);

            originX += (playerParams.playerWidth / playerParams.rayCount);

            if (hit.collider != null)
            {

                PlayerStates.groundHitPoint = hit.point.y;
                PlayerStates.detectGround = true;

                prevPlatform = platform;
                platform = hit.collider.transform;

                if (prevPlatform != platform)
                {
                    transform.parent = null;

                    if (platform.tag == playerParams.movingPlatform)
                    {
                        transform.parent = platform;
                    }
                }
                break;
            }
            else
            {

                PlayerStates.detectGround = false;
                continue;
            }
        }
    }

    public void WallCheker(float dist)
    {

        float originX = playerTransform.position.x;
        float originY = (playerTransform.position.y + (playerParams.playerHeight / 2)) - 0.02f;

        if (PlayerStates.moveRight)
        {
            originX += (playerParams.playerWidth / 2) + wallMargin;

        }
        else
        {
            originX -= (playerParams.playerWidth / 2) + wallMargin;

        }

        for (int i = 0; i < playerParams.rayCount + 1; i++)
        {

            hit = Physics2D.Raycast(new Vector2(originX, originY), PlayerStates.direction, dist * Time.deltaTime, playerParams.surfaceLayer);

            Debug.DrawRay(new Vector2(originX, originY), PlayerStates.direction * dist * Time.deltaTime);

            originY = originY - (playerParams.playerHeight - 0.04f) / playerParams.rayCount;

            if (hit.collider != null)
            {

                PlayerStates.onWall = true;

                if (hit.collider.tag == playerParams.jumpWallTag)
                {
                    PlayerStates.onJumpbleWall = true;
                }

                break;

            }
            else
            {
                PlayerStates.onJumpbleWall = false;
                PlayerStates.onWall = false;
            }
        }
    }

    public void ceilingCheck()
    {

        float originX = playerTransform.position.x - playerParams.playerWidth / 2;
        float originY = playerTransform.position.y + playerParams.playerHeight / 2;

        for (int i = 0; i < playerParams.rayCount; i++)
        {

            originX += (playerParams.playerWidth / playerParams.rayCount);

            hit = Physics2D.Raycast(new Vector2(originX, originY), Vector2.up, currentJumpSpeed * Time.deltaTime, playerParams.surfaceLayer);

            if (hit.collider != null)
            {
                PlayerStates.onCeiling = true;
                break;
            }
            else
            {
                PlayerStates.onCeiling = false;
                continue;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Plate")
        {
            box.SetActive(true);

        }

        if (collision.gameObject.tag == "bandana")
        {
            bandana.SetActive(true);
            bandanaHeart.SetActive(true);
            bandanaActivated = true;
        }

        if (collision.gameObject.tag == "topHat")
        {
            topHat.SetActive(true);
            topHatHeart.SetActive(true);
            topHatActivated = true;
        }

        if (collision.gameObject.tag == "rifle")
        {
            rifle.SetActive(true);
        }

        if (collision.gameObject.tag == "shotgun")
        {
            shotgun.SetActive(true);
        }

        if (collision.gameObject.tag == "CP1")
        {
            transform.position = spawnPos.transform.position;
        }

        if (collision.gameObject.tag == "CP2")
        {
            transform.position = spawnPos1.transform.position;
        }
        if(collision.gameObject.tag == "Door") 
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}