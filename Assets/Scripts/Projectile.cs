using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Projectile : MonoBehaviour
    {
        public static Projectile Create(Vector2 position, Vector2 direction)
        {
            var prefab = Resources.Load("Projectile");
            var projectile = GameObject.Instantiate(prefab).GetComponent<Projectile>();
            projectile._direction = direction;
            projectile.transform.position = position;

            return projectile;
        }

        Vector2 _direction;

        void Start()
        {
            Init();
        }

        void Init()
        {
            var bullet = transform.Find("Visual");

            bullet.rotation = Quaternion.LookRotation(Vector3.forward, _direction.normalized);
        }

        void Update()
        {
            ProcessUpdate();
        }

        void ProcessUpdate()
        {
            var moveSpeed = GameController.Instance.ProjectileSpeed;

            transform.position += (Vector3)(_direction * moveSpeed * Time.deltaTime);

            var viewRect = CameraController.Instance.GetViewRect();
            if (!viewRect.Contains(transform.position))
            {
                Destroy(gameObject);
            }
        }
    }
}
