using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Explosion : MonoBehaviour
    {
        public static Explosion Create(Vector2 position)
        {
            var prefab = Resources.Load<GameObject>("Explosion");
            var explosion = GameObject.Instantiate(prefab).GetComponent<Explosion>();

            explosion.transform.position = position;

            return explosion;
        }

        public void OnEnd()
        {
            Destroy(gameObject);
        }
    }
}
