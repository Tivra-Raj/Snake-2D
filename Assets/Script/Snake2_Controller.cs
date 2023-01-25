using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Snake2_Controller : MonoBehaviour
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

    public Vector2Int position;
    Direction direction;
    SnakeState state;

    float speed;
    float maxSpeed;
    int snakeBodySize;
    bool ateFood;

    List<SnakePosition> snakePostitonList;
    List<SnakeBodyPart> snakeBodyPartList;

    [SerializeField] GameMenu_Manager gameMenu_Manager;
    [SerializeField] Score_Controller score_Controller;
    [SerializeField] ScreenWrapping screenWrapping;
    [SerializeField] Food snakeFood;
    [SerializeField] Sprite body;
    [SerializeField] GameObject snakebodyHolder;
    [SerializeField] float speedController;


    private void Awake()
    {
        position = new Vector2Int(-10, 5);
        direction = Direction.Right;

        maxSpeed = 0.2f;
        speed = maxSpeed;
        snakeBodySize = 0;

        snakePostitonList = new List<SnakePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();

        state = SnakeState.Alive;
    }

    private void Update()
    {
        switch (state)
        {
            case SnakeState.Alive:
                MovementInput();
                Movement();
                break;

            case SnakeState.Dead:
                gameMenu_Manager.GameOver();
                break;
        }
    }

    void MovementInput()
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
        speed += Time.deltaTime;
        speed *= speedController;
        if (speed >= maxSpeed)
        {
            speed -= maxSpeed;

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
                case Direction.Left: directionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: directionVector = new Vector2Int(0, +1); break;
                case Direction.Down: directionVector = new Vector2Int(0, -1); break;
            }
            position += directionVector;

            position = screenWrapping.ValidatePosition(position);

            if (ateFood == true)
            {
                snakeBodySize++;
                CreateSnakeBody();
                AteFood();
                ateFood = false;
            }

            if (snakePostitonList.Count >= snakeBodySize + 1)
            {
                snakePostitonList.RemoveAt(snakePostitonList.Count - 1);
            }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Food>() != null)
        {
            ateFood = true;
        }

        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("collided with wall");
            state = SnakeState.Dead;
        }

        if (collision.gameObject.tag == "Snake1")
        {
            Debug.Log("collided with Snake1");
            state = SnakeState.Dead;
        }
    }

    void CreateSnakeBody()
    {
        snakeBodyPartList.Add(new SnakeBodyPart(body, snakebodyHolder));
    }

    private class SnakeBodyPart
    {
        private SnakePosition SnakePosition;
        private Transform transform;

        public SnakeBodyPart(Sprite snakebody, GameObject snakeBodyHolder)
        {
            GameObject snakeBody = new GameObject("Snake2Body", typeof(SpriteRenderer));
            snakeBody.tag = "Snake2";
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
        score_Controller.MultiPlayerScore(10);
    }
}