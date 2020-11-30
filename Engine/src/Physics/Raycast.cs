using System.Linq;
using Microsoft.Xna.Framework;

namespace CraftEnd.Engine.Physics
{
  public static class Raycast
  {
    private static bool LineToLineIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2? intersection)
    {
      float uA = ((b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X)) / ((b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y));
      float uB = ((a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X)) / ((b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y));

      if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
      {
        intersection = new Vector2(
          a1.X + (uA * (a2.X - a1.X)),
          a1.Y + (uA * (a2.Y - a1.Y))
        );
        return true;
      }

      intersection = null;
      return false;
    }

    private static bool IsInFront(Vector2 position, Vector2 direction, Vector2 target)
    {
      var a = (target - position) * direction;
      return (a.X + a.Y) > 0;
    }

    private static bool Cast(Vector2 position, Vector2 direction, float distance, BoxCollider collider, out RaycastHit hit)
    {
      var tl = IsInFront(position, direction, collider.TopLeft);
      var tr = IsInFront(position, direction, collider.TopRight);
      var bl = IsInFront(position, direction, collider.BottomLeft);
      var br = IsInFront(position, direction, collider.BottomRight);
      hit = new RaycastHit();

      Vector2? intersect = null;
      if (tl || tr)
      {
        if (LineToLineIntersect(collider.TopLeft, collider.TopRight, position, position + direction * distance, out intersect))
        {
          hit.Collider = collider;
          hit.Distance = Vector2.Distance(position, intersect.Value);
          hit.Point = intersect.Value;
          return true;
        }
      }

      if (tr || br)
      {
        if (LineToLineIntersect(collider.TopRight, collider.BottomRight, position, position + direction * distance, out intersect))
        {
          float hitDistance = Vector2.Distance(position, intersect.Value);
          if (hitDistance < hit.Distance)
          {
            hit.Collider = collider;
            hit.Distance = hitDistance;
            hit.Point = intersect.Value;
            return true;
          }
        }
      }

      if (br || bl)
      {
        if (LineToLineIntersect(collider.BottomRight, collider.BottomLeft, position, position + direction * distance, out intersect))
        {
          float hitDistance = Vector2.Distance(position, intersect.Value);
          if (hitDistance < hit.Distance)
          {
            hit.Collider = collider;
            hit.Distance = hitDistance;
            hit.Point = intersect.Value;
            return true;
          }
        }
      }

      if (bl || tl)
      {
        if (LineToLineIntersect(collider.BottomLeft, collider.TopLeft, position, position + direction * distance, out intersect))
        {
          float hitDistance = Vector2.Distance(position, intersect.Value);
          if (hitDistance < hit.Distance)
          {
            hit.Collider = collider;
            hit.Distance = hitDistance;
            hit.Point = intersect.Value;
            return true;
          }
        }
      }

      return false;
    }

    public static bool Cast(Vector2 position, Vector2 direction, float distance, out RaycastHit hit, params int[] ignoreLayers)
    {
      hit = null;
      foreach (var collider in Collider.Colliders)
      {
        if (ignoreLayers.Length > 0 && !ignoreLayers.Contains(collider.Layer))
          continue;

        if (collider.GetType() == typeof(BoxCollider))
        {
          RaycastHit tmpHit = null;
          if (Cast(position, direction, distance, collider as BoxCollider, out tmpHit))
          {
            if (hit == null || tmpHit.Distance < hit.Distance)
            {
              hit = tmpHit;
            }
          }
        }
        else
          throw new System.NotImplementedException();
      }

      return hit != null;
    }
  }
}