using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class DrawInfo
  {
    public Texture2D TextureAtlas { get; private set; }
    public Rectangle SpriteCoordinates { get; private set; }
    public Vector2 OffsetPosition { get; private set; }

    public DrawInfo(Texture2D textureAtlas, Rectangle spriteCoordinates, Vector2 offsetPosition)
    {
      this.TextureAtlas = textureAtlas;
      this.SpriteCoordinates = spriteCoordinates;
      this.OffsetPosition = offsetPosition;
    }
  }
}