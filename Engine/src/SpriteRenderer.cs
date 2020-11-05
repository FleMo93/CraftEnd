using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class SpriteRenderer : Component
  {
    public List<DrawInfo> Sprites = new List<DrawInfo>();

    public SpriteRenderer() { }
    public SpriteRenderer(Texture2D texture2D, Rectangle? spriteCoordinates = null, Vector2? offsetPosition = null, RenderPivot renderPivot = RenderPivot.TopLeft)
    {
      Sprites.Add(new DrawInfo(texture2D, spriteCoordinates, offsetPosition, renderPivot));
    }

    internal override void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch)
    {
      foreach (var t in this.Sprites)
      {
        var height = this.Entity.Scale.Y * renderLayer.PixelMetersMultiplier;
        var width = this.Entity.Scale.X * renderLayer.PixelMetersMultiplier;
        var x = this.Entity.Position.X * renderLayer.PixelMetersMultiplier +
            t.OffsetPosition.X * renderLayer.PixelMetersMultiplier * this.Entity.Scale.X +
            renderLayer.Position.X * renderLayer.PixelMetersMultiplier;
        var y = this.Entity.Position.Y * renderLayer.PixelMetersMultiplier +
            t.OffsetPosition.Y * renderLayer.PixelMetersMultiplier * this.Entity.Scale.Y +
            renderLayer.Position.Y * renderLayer.PixelMetersMultiplier;

        switch (t.RenderPivot)
        {
          case RenderPivot.Center:
            x = x - width / 2;
            y = y - height / 2;
            break;
        }

        spriteBatch.Draw(t.TextureAtlas, new Rectangle
        {
          X = (int)x,
          Y = (int)y,
          Height = (int)height,
          Width = (int)width
        }, t.SpriteCoordinates, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
      }
    }
  }
}