using System.Linq;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame
{
  public class LevelTile : Entity
  {
    public LevelTile(Content.Loader.MapTile mapTile, Texture2D mapTextureAtlas) : base()
    {
      this.Position = mapTile.Position;
      this.Position.Y = this.Position.Y + mapTile.YOffset;

      var spriteRenderer = new SpriteRenderer();
      this.AddComponent(spriteRenderer);
      Sprite sprite;

      if (mapTile.TilesetTile.IsAnimated)
      {
        var rectangles = new Rectangle[] { mapTile.TilesetTile.Rectangle }
          .Concat(mapTile.TilesetTile.AnimationList.Select(a => a.TilesetTile.Rectangle))
          .ToArray();

        sprite = new SpriteAnimator(this, new SpriteAnimation[] {
              new SpriteAnimation("default", mapTextureAtlas, rectangles)
            }, "default");
        sprite.RenderPivot = RenderPivot.BottomCenter;
      }
      else
      {
        sprite = new SpriteStatic(this, mapTextureAtlas, mapTile.TilesetTile.Rectangle, null, RenderPivot.BottomCenter);
      }

      sprite.OffsetPosition = new Vector2(0, -mapTile.YOffset);
      spriteRenderer.Sprites.Add(sprite);

      foreach (var boxColliderDefinition in mapTile.TilesetTile.BoxColiderDefinitions)
        this.AddComponent(new Engine.Physics.BoxCollider(1,
          boxColliderDefinition.Size,
          boxColliderDefinition.Position - new Vector2(0.5f, 1) - new Vector2(0, mapTile.YOffset)));
    }
  }
}