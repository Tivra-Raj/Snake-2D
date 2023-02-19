using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Snake_Controller : MonoBehaviour
{
    enum SnakeState
    {
        Alive,
        Dead
    }
    enum Direction
    {
        Left, 
        Right, 
        Up, 
        Down
    }

    public enum SnakeType
    {
        None,
        snake1,
        snake2
    }

    [Header("Snake Type Info")]
    [SerializeField] SnakeType snakeType;

    [Header("Snake Movement Info")]
    [SerializeField] float speedController;

    Vector2Int position;
    Direction direction;
    SnakeState state;

    float moveTimer;
    float maxMoveTimer;
    float currentSpeedController;

    [Header("Snake Body Info")]
    [SerializeField] Sprite body;
    [SerializeField] GameObject snakebodyHolder;

    List<SnakePosition> snakePostitonList;
    List<SnakeBodyPart> snakeBodyPartList;

    int snakeBodySize;

    [Header("Power Ups Info")]
    [SerializeField] TextMeshProUGUI EffectTimeLeftText;
    [SerializeField] int EffectTimeLeft = 5;
    int currentTime;

    [Header("Score Info")]
    [SerializeField] Score_Controller score_Controller;
    [SerializeField] int scoreValue = 10;
    int currentScoreValue;

    [Header("Game Over Info")]
    [SerializeField] GameMenu_Manager gameMenu_Manager;
    public bool gameOver;

    [Header("Miscellaneous")]
    [SerializeField] ScreenWrapping screenWrapping;

    private void Awake()
    {
        state = SnakeState.Alive;

        snakePostitonList = new List<SnakePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();

        direction = Direction.Right;
        maxMoveTimer = 0.2f;
        moveTimer = maxMoveTimer;
        currentSpeedController = speedController;

        snakeBodySize = 0;

        currentTime = EffectTimeLeft;

        currentScoreValue = scoreValue;
    }

    private void Start()
    {
        switch (snakeType)
        {
            case SnakeType.snake1:
                position = new Vector2Int(0, 0);
                break;

            case SnakeType.snake2:
                position = new Vector2Int(10, 10);
                break;

            default: break;
        }
    }

    private void Update()
    {
        if (gameOver)
            return;
        else
        {
            switch (state)
            {
                case SnakeState.Alive:
                    MovementType();
                    Movement();
                    break;

                case SnakeState.Dead:
                    gameMenu_Manager.GameOver();
                    break;
            }
        }
    }

    void MovementType()
    {
        switch (snakeType)
        {
            case SnakeType.snake1:  MovementInput1();
                break;
                    
            case SnakeType.snake2:  MovementInput2();
                break;

            default: break;
        }
    }

    void MovementInput1()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (direction != Direction.Down)
            {
                direction = Direction.Up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction != Direction.Up)
            {
                direction = Direction.Down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction != Direction.Right)
            {
                direction = Direction.Left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction != Direction.Left)
            {
                direction = Direction.Right;
            }
        }
    }

    void MovementInput2()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction != Direction.Down)
            {
                direction = Direction.Up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction != Direction.Up)
            {
                direction = Direction.Down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction != Direction.Right)
            {
                direction = Direction.Left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction != Direction.Left)
            {
                direction = Direction.Right;
            }
        }
    }

    void Movement()
    {
        moveTimer += Time.deltaTime;
        moveTimer *= speedController;
        if(moveTimer >= maxMoveTimer)
        {
            moveTimer -= maxMoveTimer;

            SnakePosition previousSnakePosition = null;
            if (snakePostitonList.Count > 0)
            {
                previousSnakePosition = snakePostitonList[0];
            }

            SnakePosition snakePosition = new SnakePosition(previousSnakePosition, position, direction);
            snakePostitonList.Insert(0, snakePosition);

            Vector2Int directionVector;
            switch (direction)
            {
                default:
                case Direction.Right: directionVector = new Vector2Int(+1, 0); break;
                case Direction.Left:  directionVector = new Vector2Int(-1, 0); break;
                case Direction.Up:    directionVector = new Vector2Int(0, +1); break;
                case Direction.Down:  directionVector = new Vector2Int(0, -1); break;
            }

            position += directionVector;

            if(screenWrapping != null)
                position = screenWrapping.ValidatePosition(position);

            transform.position = new Vector3(position.x, position.y);

            float rot = Mathf.Atan2(-directionVector.y, -directionVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);

            for (int i = 0; i < snakeBodyPartList.Count; i++)
            {
                snakeBodyPartList[i].SetSnakeMovePosition(snakePostitonList[i]);
            }

            foreach (SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartPosition = snakeBodyPart.GetPosition();
                if (position == snakeBodyPartPosition)
                {
                    state = SnakeState.Dead;
                }
            }
        }
    }

    void SnakeBody(Food.FoodType food)
    {
        switch(food)
        {
            case Food.FoodType.MassGainer:
                snakeBodySize++;
                CreateSnakeBody();
                AteFood();
                Debug.Log("Ate Mass Gainer Food");
                break;

            case Food.FoodType.MassBurner:
                snakeBodySize--;
                DeleteSnakeBody();
                Debug.Log("Ate Mass Burner Food");
                break;
        }
    }

    IEnumerator PowerUpTimer()
    {
        while(EffectTimeLeft > 0)
        {
            EffectTimeLeftText.enabled = true;
            yield return new WaitForSeconds(1f);
            EffectTimeLeft -= 1;
            EffectTimeLeftText.text = "Effect Time Left : " + EffectTimeLeft; 
            Debug.Log("time left " + EffectTimeLeft);
        }
        ResetPowerUP();
        EffectTimeLeftText.enabled = false;
        EffectTimeLeft = currentTime;
    }

    void SetPowerUp(PowerUpType.PowerType type)
    {
        switch (type)
        {
            case PowerUpType.PowerType.Speed:
                speedController = 1.10f;
                break;

            case PowerUpType.PowerType.Slow:
                speedController = 1f;
                break;

            case PowerUpType.PowerType.ScoreBooster:
                scoreValue *= 2;
                break;
        }
        StartCoroutine("PowerUpTimer");
    }

    void ResetPowerUP()
    {
        speedController = currentSpeedController;
        scoreValue = currentScoreValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Food>() != null )
        {
            SnakeBody(collision.GetComponent<Food>().GetFoodType());
        }

        if(collision.GetComponent<PowerUpType>() != null )
        {
            SetPowerUp(collision.GetComponent<PowerUpType>().GetPowerType());
        }

        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("collided with wall");
            state = SnakeState.Dead;
        }

        if (collision.gameObject.tag == "Snake")
        {
            Debug.Log("collided with Another Snake");
            state = SnakeState.Dead;
        }
    }

    void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(body, snakebodyHolder));
    }

    void DeleteSnakeBody()
    {
        if (snakeBodyPartList.Count != 0 && snakeBodySize != 0)
        {
            snakeBodySize--;
            int lastIndex = snakeBodyPartList.Count - 1;
            Transform lastBodyPart = snakeBodyPartList[lastIndex].transform;
            snakeBodyPartList.RemoveAt(lastIndex);
            Destroy(lastBodyPart.gameObject);
        }
    }

    private class SnakeBodyPart
    {
        private SnakePosition SnakePosition;
        public Transform transform;

        public SnakeBodyPart(Sprite snakebody, GameObject snakeBodyHolder)
        {
            GameObject snakeBody = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBody.tag = "Snake";
            snakeBody.AddComponent(typeof(BoxCollider2D));
            BoxCollider2D boxCollider = snakeBody.GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(0.75f, 0.75f);
            snakeBody.GetComponent<SpriteRenderer>().sprite = snakebody;
            snakeBody.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Player");
            transform = snakeBody.transform;
            snakeBody.transform.SetParent(snakeBodyHolder.transform);
        }

        public void SetSnakeMovePosition(SnakePosition snakePosition)
        {
            this.SnakePosition = snakePosition;
            transform.position = new Vector3(SnakePosition.GetPosition().x, SnakePosition.GetPosition().y);

            float angle;
            switch (snakePosition.GetDirection())
            {
                default:
                case Direction.Up:
                    angle = 0;
                    break;
                case Direction.Down:
                    angle = 180;
                    break;
                case Direction.Left:
                    angle = -90;
                    break;
                case Direction.Right:
                    angle = 90;
                    break;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }

        public Vector2Int GetPosition()
        {
            return SnakePosition.GetPosition();
        }
    }

    private class SnakePosition
    {
        private SnakePosition previousSnakePosition;
        private Vector2Int position;
        private Direction direction;

        public SnakePosition(SnakePosition previousSnakePosition, Vector2Int position, Direction direction)
        {
            this.previousSnakePosition = previousSnakePosition;
            this.position = position;
            this.direction = direction;
        }

        public Vector2Int GetPosition()
        {
            return position;
        }

        public Direction GetDirection()
        {
            return direction;
        }
    }

    public void AteFood()
    {
        score_Controller.AddScore(scoreValue);
    }
}