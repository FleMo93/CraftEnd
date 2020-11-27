using CraftEnd.Engine;

namespace CraftEnd.CoreGame.Debug
{
  public class PositionAxis : Entity
  {
    public PositionAxis()
    {
      var spriteRenderer = new SpriteRenderer();
      spriteRenderer.ShowEntityPositionAxis = true;
      this.Position.Z = float.MaxValue;
      this.AddComponent(spriteRenderer);
    }
  }
}