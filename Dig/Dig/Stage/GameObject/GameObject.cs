using System;
using Microsoft.Xna.Framework;
using MyLib.Device;

namespace Dig.Stage
{

    abstract class GameObj : ICloneable
    {
        protected string name;
        protected Coordinate coordinate;
        protected Vector2 position;
        protected int collideType;
        protected int attackType;

        protected int height;
        protected int width;
        protected bool isDead = false;

        protected GameDevice gameDevice;

        public GameObj(string name, Coordinate coordinate, int height, int width, GameDevice gameDevice, int collideType, int attackType)
        {
            this.name = name;
            this.coordinate = coordinate;
            position = coordinate.GetVector();
            this.collideType = collideType;
            this.attackType = attackType;

            this.height = height;
            this.width = width;

            this.gameDevice = gameDevice;
        }


        #region 一般用
        public virtual object Clone() { return null; }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public void SetCoordinate(Coordinate coordinate)
        {
            this.coordinate = coordinate;
            position = coordinate.GetVector();
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Coordinate GetCoordinate()
        {
            return coordinate;
        }

        public bool IsDead()
        {
            return isDead;
        }
        #endregion

        public virtual void Collide(GameObj gameObj) { }
        public virtual void Attack(GameObj gameObj) { }

        public bool IsCollision(GameObj otherObject)
        {
            return this.GetRectangle().Intersects(otherObject.GetRectangle());
        }

        public Rectangle GetRectangle()
        {
            Rectangle area = new Rectangle();

            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;

            return area;
        }

        public int GetCollideType()
        {
            return collideType;
        }


    }
}
