using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class SpriteRenderer : Component
  {
    public List<Sprite> Sprites = new List<Sprite>();

    internal override void Update(GameTime gameTime)
    {
      this.Sprites.ForEach(r => r.Update(gameTime));
    }

    internal override void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch)
    {
      foreach (var t in this.Sprites)
      {
        var height = this.Entity.Scale.Y * t.Scale.Y * renderLayer.PixelMetersMultiplier;
        var width = this.Entity.Scale.X * t.Scale.X * renderLayer.PixelMetersMultiplier;

        if (t.SpriteCoordinates.HasValue)
          if (t.SpriteCoordinates.Value.Height > t.SpriteCoordinates.Value.Width)
            width = width * t.SpriteCoordinates.Value.Width / t.SpriteCoordinates.Value.Height;
          else
            height = height * t.SpriteCoordinates.Value.Width / t.SpriteCoordinates.Value.Height;
        else
        {
          if (t.Texture.Height > t.Texture.Width)
            width = width * t.Texture.Height / t.Texture.Width;
          else
            height = height * t.Texture.Width / t.Texture.Height;
        }

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

        spriteBatch.Draw(t.Texture, new Rectangle
        {
          X = (int)x,
          Y = (int)y,
          Height = (int)height,
          Width = (int)width
        }, t.SpriteCoordinates, Color.White, 0, new Vector2(0, 0),
        t.FlipHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1);
      }
    }
  }
}