using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class DrawInfo
  {
    public Texture2D TextureAtlas { get; private set; }
    public Rectangle? SpriteCoordinates { get; set; }
    public Vector2 OffsetPosition { get; set; }
    public RenderPivot RenderPivot { get; set; } = RenderPivot.TopLeft;


    public DrawInfo(Texture2D texture, Rectangle? spriteCoordinates = null, Vector2? offsetPosition = null, RenderPivot renderPivot = RenderPivot.TopLeft)
    {
      this.TextureAtlas = texture;
      this.SpriteCoordinates = spriteCoordinates;
      this.OffsetPosition = offsetPosition ?? new Vector2();
      this.RenderPivot = renderPivot;
    }
  }
}