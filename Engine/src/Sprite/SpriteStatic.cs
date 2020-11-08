using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class SpriteStatic : Sprite
  {
    public SpriteStatic(Entity entity, Texture2D texture, Rectangle? spriteCoordinates = null, Vector2? offsetPosition = null, RenderPivot renderPivot = RenderPivot.TopLeft): base(entity)
    {
      this.Texture = texture;
      this.SpriteCoordinates = spriteCoordinates;
      this.OffsetPosition = offsetPosition ?? new Vector2();
      this.RenderPivot = renderPivot;
    }
  }
}