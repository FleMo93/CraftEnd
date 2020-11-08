using System.Linq;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame
{
  public class LevelTile : Entity
  {
    public LevelTile(Content.Loader.TilesetTile tilesetTile, Texture2D mapTextureAtlas)
    {
      var spriteRenderer = new SpriteRenderer();
      this.AddComponent(spriteRenderer);

      if (tilesetTile.IsAnimated)
      {
        var rectangles = new Rectangle[] { tilesetTile.Rectangle }
          .Concat(tilesetTile.AnimationList.Select(a => a.TilesetTile.Rectangle))
          .ToArray();

        var animator = new SpriteAnimator(this, new SpriteAnimation[] {
              new SpriteAnimation("default", mapTextureAtlas, rectangles)
            }, "default");
        animator.RenderPivot = RenderPivot.BottomCenter;
        spriteRenderer.Sprites.Add(animator);
      }
      else
        spriteRenderer.Sprites.Add(new SpriteStatic(this, mapTextureAtlas, tilesetTile.Rectangle, null, RenderPivot.BottomCenter));
    }
  }
}