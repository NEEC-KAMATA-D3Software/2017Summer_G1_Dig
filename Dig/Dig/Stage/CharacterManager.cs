using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{
    class CharacterManager
    {
        private List<Character> characterList;
        private List<Character> addCharacter;

        private GameDevice gameDevice;
        private Map map;

        public CharacterManager(GameDevice gameDevice)
        {
            this.gameDevice = gameDevice;
            Initialize();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in characterList)
            {
                c.Update(gameTime);
            }

            foreach (var c in addCharacter)
            {
                characterList.Add(c);
            }

            addCharacter.Clear();

            CollideToMap();
            CollideToCharacter();

            AttackToMap();
            AttackToCharacter();

            RemoveDeadCharacters();
        }

        public void Draw(Renderer renderer)
        {
            foreach (var c in characterList)
            {
                c.Draw(renderer);
            }
        }

        #region 初期化
        public void Initialize()
        {
            if (characterList != null)
            {
                characterList.Clear();
            }
            else
            {
                characterList = new List<Character>();
            }

            if (addCharacter != null)
            {
                addCharacter.Clear();
            }
            else
            {
                addCharacter = new List<Character>();
            }
        }

        public void Add(Character character)
        {
            if (character == null)
            {
                return;
            }
            addCharacter.Add(character);
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
            foreach (var c in characterList)
            {
                map.Collide(c);
            }
        }

        private void CollideToCharacter()
        {
            foreach (var c1 in characterList)
            {
                foreach (var c2 in characterList)
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

        #region 攻撃処理
        private void AttackToMap()
        {
            if (map == null)
            {
                return;
            }
            foreach (var c in characterList)
            {
                if (c.IsAttack())
                {
                    map.Attack(c);
                }
            }
        }

        private void AttackToCharacter()
        {
            foreach (var c1 in characterList)
            {
                if (c1.IsAttack())
                {
                    foreach (var c2 in characterList)
                    {
                        if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                        {
                            continue;
                        }
                        if (c1.IsAttackRange(c2))
                        {
                            c1.Attack(c2);
                        }
                    }
                    c1.AttackEnd();
                }
            }
        }
        #endregion

        #region 更新処理
        private void RemoveDeadCharacters()
        {
            characterList.RemoveAll(c => c.IsDead());
        }

        public void AddCharacter(Character character)
        {
            if (character == null)
            {
                return;
            }
            addCharacter.Add(character);
        }
        #endregion


    }
}
