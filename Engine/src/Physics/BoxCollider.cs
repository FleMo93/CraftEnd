using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine.Physics
{
  public class BoxCollider : Collider
  {
    public bool RenderBounds { get; set; } = false;
    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 TopLeft
    {
      get
      {
        return new Vector2(Entity.Position.X + Position.X, Entity.Position.Y + Position.Y);
      }
    }
    public Vector2 TopRight
    {
      get
      {
        var position = new Vector2(Position.X + Entity.Position.X, Position.Y + Entity.Position.Y);
        return position + (Size * new Vector2(1, 0));
      }
    }
    public Vector2 BottomLeft
    {
      get
      {
        var position = new Vector2(Position.X + Entity.Position.X, Position.Y + Entity.Position.Y);
        return position + (Size * new Vector2(0, 1));
      }
    }
    public Vector2 BottomRight
    {
      get
      {
        var position = new Vector2(Position.X + Entity.Position.X, Position.Y + Entity.Position.Y);
        return position + Size;
      }
    }

    private Texture2D boundingTexture = null;

    public BoxCollider(Vector2 size, Vector2 position)
    {
      this.Size = size;
      this.Position = position;
    }

    public override void Update(GameTime gameTime)
    {
      base.Update(gameTime);
    }

    internal override void Draw(GameTime gameTime, Camera camera, SpriteBatch spriteBatch)
    {
      if (!this.RenderBounds)
        return;

      var textureSizeX = (int)(this.Size.X * 10);
      var textureSizeY = (int)(this.Size.Y * 10);
      if (this.boundingTexture == null || this.boundingTexture.Width != textureSizeX || this.boundingTexture.Height != textureSizeY)
      {
        var color = new Color[textureSizeX * textureSizeY];
        for (int y = 0; y < textureSizeY; y++)
        {
          for (int x = 0; x < textureSizeX; x++)
          {
            if (y != 0 && y != textureSizeY - 1 && x != 0 && x != textureSizeX - 1)
              continue;

            color[y * textureSizeX + x] = Color.White;
          }
        }

        this.boundingTexture = new Texture2D(spriteBatch.GraphicsDevice, textureSizeX, textureSizeY);
        this.boundingTexture.SetData<Color>(color);
      }

      spriteBatch.Draw(
        this.boundingTexture,
        new Vector2(
          this.Entity.Position.X + this.Position.X,
          this.Entity.Position.Y + this.Position.Y),
        null, Color.LimeGreen, 0, new Vector2(0, 0),
        new Vector2(0.1f, 0.1f),
        SpriteEffects.None, 1);
      base.Draw(gameTime, camera, spriteBatch);
    }
  }
}