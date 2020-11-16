using Microsoft.Xna.Framework;

namespace CraftEnd.Engine.Colission
{
  public class Raycast
  {
    public readonly Vector2 Position;
    public readonly Vector2 Direction;

    public Raycast(Vector2 position, Vector2 direction)
    {
      this.Position = position;
      this.Direction = direction;

    }
  }
}