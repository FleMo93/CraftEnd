using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame
{
  public class Life : Entity
  {
    private Rectangle heartFullSpriteCoordinates;
    private Rectangle heartHalfSpriteCoordinates;
    private Rectangle heartEmptySpriteCoordinates;
    private SpriteRenderer spriteRenderer;

    private Texture2D texture;
    private int _numberOfHearts = 1;
    public int NumberOfHearts
    {
      get { return _numberOfHearts; }
      set
      {
        _numberOfHearts = value;
        this.UpdateSpriteRenderer();
      }
    }

    public Life()
    {
      this.spriteRenderer = new SpriteRenderer();
      AddComponent(this.spriteRenderer);
    }

    public void LoadContent(DungenonTilesetII0x72Loader content)
    {
      this.texture = content.Texture;
      this.heartFullSpriteCoordinates = content.TryGetSpriteCoordinates("ui_heart_full")[0];
      this.heartHalfSpriteCoordinates = content.TryGetSpriteCoordinates("ui_heart_half")[0];
      this.heartEmptySpriteCoordinates = content.TryGetSpriteCoordinates("ui_heart_empty")[0];
      this.UpdateSpriteRenderer();
    }

    private void UpdateSpriteRenderer()
    {
      spriteRenderer.Sprites.Clear();
      for (int i = 0; i < _numberOfHearts; i++)
      {
        spriteRenderer.Sprites.Add(new SpriteStatic(this, this.texture, heartFullSpriteCoordinates, new Vector2(i, 0)));
      }
    }
  }
}