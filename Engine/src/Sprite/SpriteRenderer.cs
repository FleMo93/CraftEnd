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

    public override void Update(GameTime gameTime)
    {
      this.Sprites.ForEach(r => r.Update(gameTime));
    }

    internal override void Draw(GameTime gameTime, Camera camera, SpriteBatch spriteBatch)
    {
      foreach (var t in this.Sprites)
      {
        var x = this.Entity.Position.X;
        var y = this.Entity.Position.Y;

        switch (t.RenderPivot)
        {
          case RenderPivot.Center:
            x = x - t.Scale.X / 2;
            y = y - t.Scale.Y / 2;
            break;
          case RenderPivot.BottomCenter:
            x = x - t.Scale.X / 2;
            y = y - t.Scale.Y;
            break;
          case RenderPivot.TopLeft:
            break;
          default:
            throw new System.NotImplementedException();
        }

        x += t.OffsetPosition.X;
        y += -t.OffsetPosition.Y;

        var width = 1f * t.Entity.Scale.X * t.Scale.X / (t.SpriteCoordinates.HasValue ? t.SpriteCoordinates.Value.Width : t.Texture.Width);
        var height = 1f * t.Entity.Scale.Y * t.Scale.Y / (t.SpriteCoordinates.HasValue ? t.SpriteCoordinates.Value.Height : t.Texture.Height);

        spriteBatch.Draw(t.Texture, new Vector2(x, y),
          t.SpriteCoordinates, Color.White, 0, new Vector2(0, 0),
          new Vector2(width, height),
          t.FlipHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

        if (this.ShowSpritePositionAxis && Renderer.DebugPositionTexture != null)
        {
          spriteBatch.Draw(Renderer.DebugPositionTexture,
            new Vector2(this.Entity.Position.X, this.Entity.Position.Y),
            null, Color.White, 0, new Vector2(),
            new Vector2(
              0.5f / Renderer.DebugPositionTexture.Width,
              0.5f / Renderer.DebugPositionTexture.Height),
            SpriteEffects.None, 0);
        }
      }

      if (this.ShowEntityPositionAxis && Renderer.DebugPositionTexture != null)
      {
        spriteBatch.Draw(
          Renderer.DebugPositionTexture,
          new Vector2(this.Entity.Position.X, this.Entity.Position.Y),
          null, Color.White, 0, new Vector2(),
          new Vector2(
            0.5f / Renderer.DebugPositionTexture.Width,
            0.5f / Renderer.DebugPositionTexture.Height),
          SpriteEffects.None, 0);
      }
    }
  }
}