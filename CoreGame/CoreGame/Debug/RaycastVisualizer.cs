using CraftEnd.Engine;
using CraftEnd.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame.Debug
{
  public class RaycastVisualizer : Entity
  {
    private PositionAxis startPosition;
    private PositionAxis hitPosition;
    private PositionAxis endPosition;

    public RaycastVisualizer()
    {
      startPosition = new PositionAxis();
      hitPosition = new PositionAxis();
      endPosition = new PositionAxis();

      startPosition.SetParent(this);
      hitPosition.SetParent(this);
      endPosition.SetParent(this);
    }

    public void UpdatePosition(Vector2 start, Vector2 end, RaycastHit hit)
    {
      this.startPosition.Position.X = start.X;
      this.startPosition.Position.Y = start.Y;

      this.hitPosition.Position.X = hit.Point.X;
      this.hitPosition.Position.Y = hit.Point.Y;

      this.endPosition.Position.X = end.X;
      this.endPosition.Position.Y = end.Y;
    }
  }
}