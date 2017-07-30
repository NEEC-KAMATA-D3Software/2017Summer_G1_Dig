using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    class GameObjectManager
    {
        private List<GameObj> gameObjectList;
        private List<GameObj> addGameObjects;

        private GameDevice gameDevice;
        private Map map;

        public GameObjectManager(GameDevice gameDevice)
        {
            this.gameDevice = gameDevice;
            Initialize();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in gameObjectList)
            {
                c.Update(gameTime);
            }

            foreach (var c in addGameObjects)
            {
                gameObjectList.Add(c);
            }

            addGameObjects.Clear();

            CollideToMap();
            CollideToGameObject();

            RemoveDeadCharacters();
        }

        public void Draw(Renderer renderer)
        {
            foreach (var c in gameObjectList)
            {
                c.Draw(renderer);
            }
        }

        #region 初期化
        public void Initialize()
        {
            if (gameObjectList != null)
            {
                gameObjectList.Clear();
            }
            else
            {
                gameObjectList = new List<GameObj>();
            }

            if (addGameObjects != null)
            {
                addGameObjects.Clear();
            }
            else
            {
                addGameObjects = new List<GameObj>();
            }
        }

        public void Add(GameObj gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        public void Add(Map map)
        {
            if (map == null)
            {
                return;
            }
            this.map = map;
        }
        #endregion

        #region　衝突処理
        private void CollideToMap()
        {
            if (map == null)
            {
                return;
            }
            foreach (var c in gameObjectList)
            {
                map.Collide(c);
            }
        }

        private void CollideToGameObject()
        {
            foreach (var c1 in gameObjectList)
            {
                foreach (var c2 in gameObjectList)
                {
                    if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                    {
                        continue;
                    }
                    if (c1.IsCollision(c2))
                    {
                        c1.Collide(c2);
                    }
                }
            }
        }
        #endregion

        //作っている
        #region 攻撃処理
        private void AttackToMap()
        {
            if (map == null)
            {
                return;
            }
            foreach (var c in gameObjectList)
            {
                map.Attack(c);
            }
        }

        private void AttackToGameObject()
        {
            foreach (var c1 in gameObjectList)
            {
                //作っている
                //if (c1's attack flag in on)
                //if ()
                {
                    foreach (var c2 in gameObjectList)
                    {
                        if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                        {
                            continue;
                        }
                        if (c1.IsCollision(c2))
                        {
                            c1.Attack(c2);
                        }
                    }
                    //}
                }
            }
        }
        #endregion

        #region 更新処理

        private void RemoveDeadCharacters()
        {
            gameObjectList.RemoveAll(c => c.IsDead());
        }

        public void AddGameObject(GameObj gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        public bool IsPlayerDead()
        {
            GameObj find = gameObjectList.Find(c => c is Player);
            return (find == null || find.IsDead());
        }

        public GameObj GetPlayer()
        {
            GameObj find = gameObjectList.Find(c => c is Player);
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;
        }
        #endregion


    }
}
