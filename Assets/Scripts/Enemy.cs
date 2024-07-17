using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public abstract class BaseEnemy : MonoBehaviour
    {
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
    }
}
