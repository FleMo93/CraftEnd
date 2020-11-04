using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class SpriteRenderer : Component
  {
    public List<DrawInfo> Sprites = new List<DrawInfo>();

    public SpriteRenderer() { }
    public SpriteRenderer(Texture2D texture2D, Rectangle? spriteCoordinates = null, Vector2? offsetPosition = null)
    {
      Sprites.Add(new DrawInfo(texture2D, spriteCoordinates, offsetPosition));
    }

    internal override void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch)
    {
      foreach (var t in this.Sprites)
      {
        spriteBatch.Draw(t.TextureAtlas, new Rectangle
        {
          X = (int)(this.Entity.Position.X * renderLayer.PixelMetersMultiplier +
            t.OffsetPosition.X * renderLayer.PixelMetersMultiplier * this.Entity.Scale.X),
          Y = (int)(this.Entity.Position.Y * renderLayer.PixelMetersMultiplier +
            t.OffsetPosition.Y * renderLayer.PixelMetersMultiplier * this.Entity.Scale.Y),
          Height = (int)(this.Entity.Scale.Y * renderLayer.PixelMetersMultiplier),
          Width = (int)(this.Entity.Scale.X * renderLayer.PixelMetersMultiplier)
        }, t.SpriteCoordinates, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
      }
    }
  }
}