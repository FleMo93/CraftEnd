using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class BoxCollider : Component
  {
    public bool RenderBounds { get; set; } = false;
    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }

    private Texture2D boundingTexture = null;

    public BoxCollider(Vector2 size, Vector2 position)
    {
      this.Size = size;
      this.Position = position;
    }

    internal override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }

    internal override void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch)
    {
      if (!this.RenderBounds)
        return;

      var sizeX = (int)(this.Size.X * 10);
      var sizeY = (int)(this.Size.Y * 10);
      if (this.boundingTexture == null || this.boundingTexture.Width != sizeX || this.boundingTexture.Height != sizeY)
      {
        var color = new Color[sizeX * sizeY];
        for (int y = 0; y < sizeY; y++)
        {
          for (int x = 0; x < sizeX; x++)
          {
            if (y != 0 && y != sizeY - 1 && x != 0 && x != sizeX - 1)
              continue;

            color[y * sizeX + x] = Color.GreenYellow;
          }
        }

        this.boundingTexture = new Texture2D(spriteBatch.GraphicsDevice, sizeX, sizeY);
        this.boundingTexture.SetData<Color>(color);
      }

      spriteBatch.Draw(
        this.boundingTexture,
        new Vector2(
          this.Entity.Position.X * renderLayer.PixelMetersMultiplier +
          this.Position.X * renderLayer.PixelMetersMultiplier +
          renderLayer.Position.X * renderLayer.PixelMetersMultiplier,
          this.Entity.Position.Y * renderLayer.PixelMetersMultiplier +
          this.Position.Y * renderLayer.PixelMetersMultiplier +
          renderLayer.Position.Y * renderLayer.PixelMetersMultiplier),
        null, Color.White, 0, new Vector2(), new Vector2(0.1f * renderLayer.PixelMetersMultiplier, 0.1f * renderLayer.PixelMetersMultiplier), SpriteEffects.None, 1);
      base.Draw(gameTime, renderLayer, spriteBatch);
    }
  }
}