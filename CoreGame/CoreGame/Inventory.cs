using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd.CoreGame
{
  public class Inventory: Entity
  {
    private SpriteRenderer spriteRenderer;

    public Inventory(Texture2D inventoryTile, int sizeX, int sizeY)
    {
      this.spriteRenderer = new SpriteRenderer();

      for (var y = 0; y < sizeY; y++)
        for (var x = 0; x < sizeX; x++)
        {
          var sprite = new SpriteStatic(this, inventoryTile, null, new Vector2(x, y));
          spriteRenderer.Sprites.Add(sprite);
        }
      
      this.AddComponent(spriteRenderer);
      this.spriteRenderer.Active = false;
    }

    public override void Update(GameTime gameTime)
    {
      if (Keyboard.GetState().IsKeyPressed(Keys.I))
      {
        this.spriteRenderer.Active = !this.spriteRenderer.Active;
      }
      base.Update(gameTime);
    }
  }
}