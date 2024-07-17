using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        #region Inspector

        [SerializeField] Transform[] _SpawnLocations;

        #endregion

        public void Spawn()
        {
            var gc = GameController.Instance;
            var enemy = gc.EnemyFactory.Create(EnemyTag.Simple);

            enemy.Position = _SpawnLocations[Random.Range(0, _SpawnLocations.Length)].position;
            enemy.MoveSpeed = Random.Range(gc.EnemySpeedMin, gc.EnemySpeedMax);
            enemy.onCrossedFinishLine = OnCrossedFinishLine;
        }

        void OnCrossedFinishLine(BaseEnemy enemy)
        {
            GameController.Instance.EnemyCrossedFinishedLine();

            GameController.Instance.EnemyFactory.Release(enemy);

            Spawn();
        }
    }
}
