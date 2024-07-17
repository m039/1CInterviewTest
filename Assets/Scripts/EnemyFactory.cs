using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

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

        Dictionary<EnemyTag, List<IEnemy>> _pool = new();

        public EnemyFactory()
        {
            Init();
        }

        public IEnemy Create(EnemyTag tag)
        {
            if (_builders.TryGetValue(tag, out var builder))
            {
                if (_parent == null)
                {
                    _parent = new GameObject("<< Enemies >>");
                }

                IEnemy enemy;

                if (_pool.TryGetValue(tag, out var list) && list.Count > 0)
                {
                    enemy = list[0];
                    list.RemoveAt(0);
                } else
                {
                    var instance = UnityEngine.Object.Instantiate(builder());
                    instance.transform.SetParent(_parent.transform);

                    enemy = instance.GetComponent<IEnemy>();
                    enemy.Tag = tag;
                }

                var go = ((MonoBehaviour)enemy).gameObject;
                go.SetActive(true);

                return enemy;
            }

            throw new Exception("Can't find a builder");
        }

        public void Release(IEnemy enemy)
        {
            var go = ((MonoBehaviour)enemy).gameObject;

            go.SetActive(false);

            if (_pool.TryGetValue(enemy.Tag, out var list))
            {
                list.Add(enemy);
            } else
            {
                _pool.Add(enemy.Tag, new List<IEnemy> { enemy });
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
