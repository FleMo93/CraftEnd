using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;

namespace CraftEnd.CoreGame
{
  public class Level : Entity
  {
    public Level(TiledMap map) : base()
    {
      foreach (var layer in map.Layers)
      {
        foreach (var mapTile in layer.Value)
        {
          var tile = new LevelTile(mapTile, map.TextureAtlas);
          tile.SetParent(this);
        }
      }
    }
  }
}