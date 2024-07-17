using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        #region Inspector

        [SerializeField] float _MoveSpeed = 10f;

        [SerializeField] BoxCollider2D _Collider;

        #endregion

        readonly PlayerInput _input = new();

        float _shootingDelay = 0f;

        void Update()
        {
            ProcessMove();
            ProcessShooting();
        }

        void ProcessMove()
        {
            var move = _input.GetMove();
            if (move.sqrMagnitude > 0)
            {
                var viewRect = CameraController.Instance.GetViewRect();
                var playerSize = _Collider.bounds.size;

                var deltaMove = move.normalized * _MoveSpeed * Time.deltaTime;

                if (viewRect.min.x > transform.position.x + deltaMove.x - playerSize.x / 2f ||
                    viewRect.max.x < transform.position.x + deltaMove.x + playerSize.x / 2f)
                {
                    deltaMove.x = 0;
                }

                if (viewRect.min.y > transform.position.y + deltaMove.y - playerSize.y / 2f ||
                    viewRect.max.y < transform.position.y + deltaMove.y + playerSize.y / 2f)
                {
                    deltaMove.y = 0;
                }

                var finishLine = GameController.Instance.FinishLine.GetRect();
                if (finishLine.min.y < transform.position.y + deltaMove.y + playerSize.y / 2f)
                {
                    deltaMove.y = 0;
                }

                transform.position = transform.position + (Vector3) deltaMove;
            }
        }

        void ProcessShooting()
        {
            _shootingDelay += Time.deltaTime;

            if (_shootingDelay > 1 / GameController.Instance.PlayerShootingSpeed)
            {
                _shootingDelay = 0;

                // Find closest enemy.

                BaseEnemy enemy = null;

                foreach (var e in GameController.Instance.EnemySpawner.enemies)
                {
                    if (enemy == null)
                    {
                        enemy = e;
                    } else if (enemy.transform.position.y > e.transform.position.y)
                    {
                        enemy = e;
                    }
                }

                if (enemy != null)
                {
                    var direction = enemy.transform.position - transform.position;

                    if (direction.magnitude < GameController.Instance.PlayerShootingRadius)
                    {
                        Projectile.Create(transform.position, direction);
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            if (GameController.Instance == null)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, GameController.Instance.PlayerShootingRadius);
        }

        public class PlayerInput
        {
            public Vector2 GetMove()
            {
                var direction = new Vector2();

                if (Input.GetKey(KeyCode.A))
                {
                    direction.x = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction.x = 1;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    direction.y = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    direction.y = -1;
                }

                return direction;
            }
        }
    }
}
