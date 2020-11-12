using CraftEnd.Engine;
using CraftEnd.Engine.Colission;
using Microsoft.Xna.Framework;

namespace CraftEnd.CoreGame
{
  public class EmptyCollider: Entity
  {
    public EmptyCollider()
    {
      var collider = new BoxCollider(new Vector2(10, 10), new Vector2(-5, -5), true);
      this.AddComponent(collider);
    }
  }
}