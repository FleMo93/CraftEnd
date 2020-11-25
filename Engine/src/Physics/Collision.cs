using System;
using Microsoft.Xna.Framework;

namespace CraftEnd.Engine.Physics
{
  public class Collision
  {
    static float GetCollisionArea(BoxCollider a, BoxCollider b)
    {
      Vector2 aPositionTL = new Vector2(a.Entity.Position.X, a.Entity.Position.Y) + a.Position;
      Vector2 bPositionTL = new Vector2(b.Entity.Position.X, b.Entity.Position.Y) + b.Position;
      Vector2 oTL = new Vector2(
        Math.Max(aPositionTL.X, bPositionTL.X),
        Math.Max(aPositionTL.Y, bPositionTL.Y)
      );

      Vector2 aPositionBR = new Vector2(aPositionTL.X + a.Size.X, aPositionTL.Y + a.Size.Y);
      Vector2 bPositionBR = new Vector2(bPositionTL.X + b.Size.X, bPositionTL.Y + b.Size.Y);

      Vector2 oBR = new Vector2(
        Math.Min(aPositionBR.X, bPositionBR.X),
        Math.Min(aPositionBR.Y, bPositionBR.Y)
      );

      return (oBR.X - oTL.X) * (oBR.Y - oTL.Y);
    }

    public readonly Collider ColliderSource;
    public readonly Collider ColliderTarget;
    public readonly float ColissionArea;

    public Collision(Collider source, Collider target)
    {
      this.ColliderSource = source;
      this.ColliderTarget = target;

      if (source.GetType() == typeof(BoxCollider) && target.GetType() == typeof(BoxCollider))
        this.ColissionArea = GetCollisionArea(source as BoxCollider, target as BoxCollider);
      else
        throw new System.NotImplementedException();
    }
  }
}