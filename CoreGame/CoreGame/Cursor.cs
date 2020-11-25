using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd.CoreGame
{
  public class Cursor : Entity
  {
    private Camera camera;

    public Cursor(): base()
    {
      this.Scale = new Vector2(0.5f, 0.5f);
    }

    public void LoadContent(Texture2D cursor, Camera camera)
    {
      var sprite = new SpriteStatic(this, cursor);
      var renderer = new SpriteRenderer();
      renderer.Sprites.Add(sprite);
      this.AddComponent(renderer);
      this.camera = camera;
    }

    public override void Update(GameTime gameTime)
    {
      var mouseWorldPosition = this.camera.ScreenToWorldPosition(Mouse.GetState().Position);
      this.Position.X = mouseWorldPosition.X;
      this.Position.Y = mouseWorldPosition.Y;

      base.Update(gameTime);
    }
  }
}