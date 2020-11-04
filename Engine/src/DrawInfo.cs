using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class DrawInfo
  {
    public Texture2D TextureAtlas { get; private set; }
    public Rectangle? SpriteCoordinates { get; private set; }
    public Vector2 OffsetPosition { get; private set; }

    public DrawInfo(Texture2D texture, Rectangle? spriteCoordinates = null, Vector2? offsetPosition = null)
    {
      this.TextureAtlas = texture;
      this.SpriteCoordinates = spriteCoordinates;
      this.OffsetPosition = offsetPosition ?? new Vector2();
    }
  }
}