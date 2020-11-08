using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;

namespace CraftEnd.CoreGame
{
  public class Level : Entity
  {
    public Level(TiledMap map): base()
    {
      foreach (var layer in map.Layers)
        foreach (var sprite in layer.Value)
        {
          var tile = new LevelTile(sprite.TilesetTile, map.TextureAtlas);
          tile.Position = sprite.Position;
          this.Children.Add(tile);
        }
    }
  }
}