using System.Linq;
using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;

namespace CraftEnd.CoreGame
{
  public class Level : Entity
  {
    TiledMap map;
    public Level(TiledMap map)
    {
      this.map = map;
      var spriteRenderer = new SpriteRenderer();
      this.AddComponent(spriteRenderer);

      foreach (var layer in map.Layers)
        foreach (var sprite in layer.Value)
        {
          if (sprite.TilesetTile.IsAnimated)
          {
            var rectangles = new Rectangle[] { sprite.TilesetTile.Rectangle }
              .Concat(sprite.TilesetTile.AnimationList.Select(a => a.TilesetTile.Rectangle))
              .ToArray();


            var animator = new Animator(new Animation[] {
              new Animation("a", map.TextureAtlas, rectangles)
            }, "a", sprite.Position);

            AddComponent(animator);
          }
          else
            spriteRenderer.Sprites.Add(new DrawInfo(map.TextureAtlas, sprite.TilesetTile.Rectangle, sprite.Position));
        }
    }
  }
}