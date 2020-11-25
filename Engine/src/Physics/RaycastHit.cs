using Microsoft.Xna.Framework;

namespace CraftEnd.Engine.Physics
{
  public class RaycastHit
  {
    public Collider Collider { get; internal set; }
    public float Distance { get; internal set; }
    public Vector2 Point { get; internal set; }
  }
}