using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyLib.Device;
using Dig.Def;

namespace Dig.Stage
{
    class Map
    {

        private GameObj[,] mapMatrix;

        private GameDevice gameDevice;


        public Map(GameDevice gameDevice)
        {
            mapMatrix = new GameObj[Screen.MaxRow, Screen.MaxColumn];
            this.gameDevice = gameDevice;
        }

        #region LoadMap

        private GameObj AddObj(int num, Coordinate coordinate)
        {
            Dictionary<int, GameObj> objectList = new Dictionary<int, GameObj>();
            objectList.Add(0, new Space(new Coordinate(0, 0), gameDevice));
            objectList.Add(1, new Block(new Coordinate(0, 0), gameDevice, "tuti", 1));
            objectList.Add(2, new Block(new Coordinate(0, 0), gameDevice, "hardtuti", 3));
            objectList.Add(3, new Block(new Coordinate(0, 0), gameDevice, "rock", 999));
            objectList.Add(4, new Goal(new Coordinate(0, 0), gameDevice));
            objectList.Add(5, new Trap(new Coordinate(0, 0), gameDevice));

            GameObj obj = objectList[num];
            obj.SetCoordinate(coordinate);
            return obj;
        }

        public void Load(string filename)
        {
            if (!filename.Contains(".csv"))
            {
                filename = filename + ".csv";
            }

            CSVReader csv = new CSVReader();
            csv.Read(filename);

            var data = csv.GetData();

            //load to mapMatrix
            for (int row = 0; row < Screen.MaxRow; ++row)
            {
                for (int column = 0; column < Screen.MaxColumn; ++column)
                {
                    try
                    {
                        mapMatrix[row, column] = AddObj(int.Parse(data[row][column]), new Coordinate(row, column));
                    }
                    catch
                    {
                        mapMatrix[row, column] = AddObj(0, new Coordinate(row, column));
                    }
                }
            }
        }
        #endregion

        #region 更新処理
        public void Draw(Renderer renderer)
        {
            foreach (var obj in mapMatrix)
            {
                if (obj is Space)
                {
                    continue;
                }
                if (!obj.IsDead())
                {
                    obj.Draw(renderer);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (GameObj obj in mapMatrix)
            {
                if (obj is Space)
                {
                    continue;
                }
                obj.Update(gameTime);
            }
        }
        #endregion

        #region Characterとの処理
        //近くのオブジェクトをチェックして、衝突処理
        public void Collide(Character character)
        {
            int row = character.GetCoordinate().row;
            int column = character.GetCoordinate().column;

            for (int y = row - 2; y <= row + 2; ++y)
            {
                for (int x = column - 2; x < column + 2; ++x)
                {
                    try
                    {
                        if (mapMatrix[y, x].IsDead())
                        {
                            continue;
                        }
                        if (character.IsCollision(mapMatrix[y, x]))
                        {
                            character.Collide(mapMatrix[y, x]);
                            mapMatrix[y, x].Collide(character);
                        }
                    }
                    catch { }
                }
            }
        }

        //近くのオブジェクトをチェックして、攻撃処理
        public void Attack(Character character)
        {
            int row = character.GetCoordinate().row;
            int column = character.GetCoordinate().column;

            for (int y = row - 2; y <= row + 2; ++y)
            {
                for (int x = column - 2; x < column + 2; ++x)
                {
                    try
                    {
                        if (mapMatrix[y, x].IsDead())
                        {
                            continue;
                        }
                        if (character.IsAttackRange(mapMatrix[y, x]))
                        {
                            character.Attack(mapMatrix[y, x]);
                            mapMatrix[y, x].Attack(character);
                        }
                    }
                    catch { }
                }
            }
        }

        public void AttackTo(Character character, Coordinate coordinate)
        {
            mapMatrix[coordinate.row, coordinate.column].Attack(character);
        }

        public bool Movable(Coordinate coordinate)
        {
            return (mapMatrix[coordinate.row, coordinate.column].IsDead()
                 || mapMatrix[coordinate.row, coordinate.column] is Space);
        }
        #endregion

        public void ChangeObject(GameObj gameObj, Coordinate coordinate)
        {
            mapMatrix[coordinate.row, coordinate.column] = gameObj;
        }

        public bool Changeable(Coordinate coordinate)
        {
            return !(mapMatrix[coordinate.row, coordinate.column] is Space);
        }


    }
}
