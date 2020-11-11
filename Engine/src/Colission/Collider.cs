using System;
using System.Collections.Generic;

namespace CraftEnd.Engine.Colission
{
  public class Collider : Component
  {
    private static List<Collider> Colliders;
    static Collider()
    {
      Colliders = new List<Collider>();
    }

    private static bool IsColliding(Collider a, Collider b)
    {
      if (a.GetType() == typeof(BoxCollider) && a.GetType() == typeof(BoxCollider))
      {
        return IsColliding(a as BoxCollider, b as BoxCollider);
      }

      throw new System.NotImplementedException();
    }

    private static bool IsColliding(BoxCollider a, BoxCollider b)
    {
      return (Math.Abs((a.Position.X + a.Size.X/2) - (b.Position.X + b.Size.X/2)) * 2 < (a.Size.X + b.Size.X)) &&
         (Math.Abs((a.Position.Y + a.Size.Y/2) - (b.Position.Y + b.Size.Y/2)) * 2 < (a.Size.Y + b.Size.Y));
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

        
      }

      return collisionList;
    }

    internal override void Destroy()
    {
      Collider.Colliders.Remove(this);
    }
  }
}