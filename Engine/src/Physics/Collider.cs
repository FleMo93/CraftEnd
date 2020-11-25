using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CraftEnd.Engine.Physics
{
  public class Collider : Component
  {
    public Rigidbody Rigidbody { get; internal set; }

    internal static List<Collider> Colliders;
    static Collider()
    {
      Colliders = new List<Collider>();
    }

    private static bool IsColliding(Collider a, Collider b)
    {
      if (a.GetType() == typeof(BoxCollider) && b.GetType() == typeof(BoxCollider))
      {
        return IsColliding(a as BoxCollider, b as BoxCollider);
      }

      throw new System.NotImplementedException();
    }

    private static bool IsColliding(BoxCollider a, BoxCollider b)
    {
      var aPosition = new Vector2(a.Entity.Position.X, a.Entity.Position.Y) + a.Position;
      var bPosition = new Vector2(b.Entity.Position.X, b.Entity.Position.Y) + b.Position;

      if (aPosition.Y + a.Size.Y < bPosition.Y || aPosition.Y > bPosition.Y + b.Size.Y ||
        aPosition.X > bPosition.X + b.Size.X || aPosition.X + a.Size.X < bPosition.X)
        return false;

      return true;
    }

    public Collider()
    {
      Collider.Colliders.Add(this);
    }

    public IEnumerable<Collision> GetCollsions()
    {
      List<Collision> collisionList = new List<Collision>();
      foreach (var collider in Collider.Colliders)
      {
        if (collider == this)
          continue;

        if (Collider.IsColliding(this, collider))
          collisionList.Add(new Collision(this, collider));
      }

      return collisionList;
    }

    public bool IsColliding()
    {
      foreach (var collider in Collider.Colliders)
      {
        if (collider == this)
          continue;

        if (Collider.IsColliding(this, collider))
          return true;
      }

      return false;
    }

    internal override void Destroy()
    {
      Collider.Colliders.Remove(this);
    }
  }
}