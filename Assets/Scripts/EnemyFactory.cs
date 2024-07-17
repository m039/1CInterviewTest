using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum EnemyTag
    {
        Simple = 0
    }

    public class EnemyFactory
    {
        Dictionary<EnemyTag, Func<GameObject>> _builders = new();

        GameObject _parent;

        Dictionary<EnemyTag, List<BaseEnemy>> _pool = new();

        public EnemyFactory()
        {
            Init();
        }

        public BaseEnemy Create(EnemyTag tag)
        {
            if (_builders.TryGetValue(tag, out var builder))
            {
                if (_parent == null)
                {
                    _parent = new GameObject("<< Enemies >>");
                }

                BaseEnemy enemy;

                if (_pool.TryGetValue(tag, out var list) && list.Count > 0)
                {
                    enemy = list[0];
                    list.RemoveAt(0);
                } else
                {
                    var instance = UnityEngine.Object.Instantiate(builder());
                    instance.transform.SetParent(_parent.transform);

                    enemy = instance.GetComponent<BaseEnemy>();
                    enemy.Tag = tag;
                }

                enemy.gameObject.SetActive(true);

                return enemy;
            }

            throw new Exception("Can't find a builder");
        }

        public void Release(BaseEnemy enemy)
        {
            enemy.gameObject.SetActive(false);

            if (_pool.TryGetValue(enemy.Tag, out var list))
            {
                list.Add(enemy);
            } else
            {
                _pool.Add(enemy.Tag, new List<BaseEnemy> { enemy });
            }
        }

        void Init()
        {
            _builders.Add(EnemyTag.Simple, () =>
            {
                return Load("SimpleEnemy");
            });
        }

        static GameObject Load(string path)
        {
            return Resources.Load<GameObject>("Enemies/" + path);
        }
    }
}
