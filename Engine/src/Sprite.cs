using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class Sprite
  {
    public Texture2D Texture { get; set; }
    public Rectangle? SpriteCoordinates { get; set; }
    public Vector2 OffsetPosition { get; set; }
    public RenderPivot RenderPivot { get; set; } = RenderPivot.TopLeft;
    public Vector2 Scale { get; set; } = new Vector2(1, 1);
    public bool FlipHorizontal { get; set; } = false;
    internal virtual void Update(GameTime gameTime) { }
  }
}