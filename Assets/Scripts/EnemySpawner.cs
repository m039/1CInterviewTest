using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        #region Inspector

        [SerializeField] Transform[] _SpawnLocations;

        #endregion

        readonly public List<BaseEnemy> enemies = new();

        float _timeout = 0f;

        void Update()
        {
            ProcessUpdate();
        }

        void ProcessUpdate()
        {
            _timeout -= Time.deltaTime;

            if (_timeout <= 0f)
            {
                Spawn();
                var gc = GameController.Instance;
                _timeout = Random.Range(gc.EnemySpawnTimeoutMin, gc.EnemySpawnTimeoutMax);
            }
        }

        static List<EnemyTag> s_Tags;

        void Spawn()
        {
            if (s_Tags == null)
            {
                s_Tags = new List<EnemyTag>(System.Enum.GetValues(typeof(EnemyTag)).Cast<EnemyTag>());
            }

            var gc = GameController.Instance;
            var enemy = gc.EnemyFactory.Create(s_Tags[Random.Range(0, s_Tags.Count)]);

            enemy.Position = _SpawnLocations[Random.Range(0, _SpawnLocations.Length)].position;
            enemy.MoveSpeed = Random.Range(gc.EnemySpeedMin, gc.EnemySpeedMax);
            enemy.onCrossedFinishLine = OnCrossedFinishLine;
            enemy.onDead = OnDead;
            enemy.Health = gc.EnemyHealth;

            enemies.Add(enemy);
        }

        void OnDead(BaseEnemy enemy)
        {
            enemies.Remove(enemy);
            GameController.Instance.EnemyFactory.Release(enemy);
            GameController.Instance.EnemyDied();
        }

        void OnCrossedFinishLine(BaseEnemy enemy)
        {
            GameController.Instance.EnemyCrossedFinishedLine();

            GameController.Instance.EnemyFactory.Release(enemy);

            enemies.Remove(enemy);
        }
    }
}
