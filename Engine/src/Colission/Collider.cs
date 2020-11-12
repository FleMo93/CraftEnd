using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CraftEnd.Engine.Colission
{
  public class Collider : Component
  {
    public Rigidbody Rigidbody { get; internal set; }

    private static List<Collider> Colliders;
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

      return (Math.Abs((aPosition.X + a.Size.X / 2) - (bPosition.X + b.Size.X / 2)) * 2 < (a.Size.X + b.Size.X)) &&
          (Math.Abs((aPosition.Y + a.Size.Y / 2) - (bPosition.Y + b.Size.Y / 2)) * 2 < (a.Size.Y + b.Size.Y));
    }

    public Collider()
    {
      Collider.Colliders.Add(this);
    }

    public IEnumerable<Collider> GetOverlapCollider()
    {
      List<Collider> collisionList = new List<Collider>();
      foreach (var collider in Collider.Colliders)
      {
        if (collider == this)
          continue;

        if (Collider.IsColliding(this, collider))
          collisionList.Add(collider);
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