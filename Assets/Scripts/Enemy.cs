using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public abstract class BaseEnemy : MonoBehaviour
    {
        public int Health { get; set; }

        public Vector2 Position
        {
            get => transform.position;

            set
            {
                var p = transform.position;
                p.x = value.x;
                p.y = value.y;
                transform.position = p;
            }
        }

        public float MoveSpeed { get; set; }

        public EnemyTag Tag { get; set; }

        public System.Action<BaseEnemy> onCrossedFinishLine;

        public System.Action<BaseEnemy> onDead;
    }

    public class Enemy : BaseEnemy
    {
        #region Inspector

        [SerializeField] CircleCollider2D _Collider;

        #endregion

        void Update()
        {
            ProcessMove();
        }

        void ProcessMove()
        {
            var p = Position + Vector2.down * MoveSpeed * Time.deltaTime;
            var rect = GameController.Instance.FinishLine.GetRect();

            if (rect.min.y > p.y - _Collider.bounds.size.y / 2f)
            {
                onCrossedFinishLine?.Invoke(this);
                return;
            }

            Position = p;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            var p = collision.GetComponentInParent<Projectile>();
            if (p != null)
            {
                Destroy(p.gameObject);

                Health -= GameController.Instance.ProjectileDamage;
                if (Health <= 0)
                {
                    Explosion.Create(Position);

                    onDead?.Invoke(this);
                }
            }
        }
    }
}
