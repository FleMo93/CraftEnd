using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public enum RenderPivot { TopLeft, Center, BottomCenter }

  public class SpriteRenderer : Component
  {
    public bool ShowEntityPositionAxis { get; set; } = false;
    public bool ShowSpritePositionAxis { get; set; } = false;
    public List<Sprite> Sprites = new List<Sprite>();

    internal override void Update(GameTime gameTime)
    {
      this.Sprites.ForEach(r => r.Update(gameTime));
    }

    internal override void Draw(GameTime gameTime, Camera camera, SpriteBatch spriteBatch)
    {
      foreach (var t in this.Sprites)
      {
        var height = this.Entity.Scale.Y * t.Scale.Y * camera.PixelMetersMultiplier;
        var width = this.Entity.Scale.X * t.Scale.X * camera.PixelMetersMultiplier;

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

        var x = this.Entity.Position.X * camera.PixelMetersMultiplier;
        var y = this.Entity.Position.Y * camera.PixelMetersMultiplier;

        switch (t.RenderPivot)
        {
          case RenderPivot.Center:
            x = x - width / 2;
            y = y - height / 2;
            break;
          case RenderPivot.BottomCenter:
            x = x - width / 2;
            y = y - height;
            break;
        }

        x += t.OffsetPosition.X * camera.PixelMetersMultiplier;
        y += -t.OffsetPosition.Y * camera.PixelMetersMultiplier;

        spriteBatch.Draw(t.Texture, new Rectangle
        {
          X = (int)x,
          Y = (int)y,
          Height = (int)height,
          Width = (int)width
        }, t.SpriteCoordinates, Color.White, 0, new Vector2(0, 0),
          t.FlipHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);


        if (this.ShowSpritePositionAxis && Renderer.DebugPositionTexture != null)
        {
          spriteBatch.Draw(Renderer.DebugPositionTexture, new Rectangle
          {
            X = (int)x,
            Y = (int)y,
            Height = (int)camera.PixelMetersMultiplier,
            Width = (int)camera.PixelMetersMultiplier
          }, null, Color.White, 0, new Vector2(), SpriteEffects.None, 1);
        }
      }

      if (this.ShowEntityPositionAxis && Renderer.DebugPositionTexture != null)
      {
        spriteBatch.Draw(Renderer.DebugPositionTexture, new Rectangle
        {
          X = (int)(this.Entity.Position.X * camera.PixelMetersMultiplier),
          Y = (int)(this.Entity.Position.Y * camera.PixelMetersMultiplier),
          Height = (int)camera.PixelMetersMultiplier,
          Width = (int)camera.PixelMetersMultiplier,
        }, null, Color.White, 0, new Vector2(), SpriteEffects.None, 1);
      }
    }
  }
}