using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;

namespace CraftEnd.CoreGame
{
  public class Level : Entity
  {
    public Level(TiledMap map) : base()
    {
      foreach (var layer in map.Layers)
        foreach (var sprite in layer.Value)
        {
          var tile = new LevelTile(sprite.TilesetTile, map.TextureAtlas);
          tile.Position = new Microsoft.Xna.Framework.Vector3(
            sprite.Position.X,
            sprite.Position.Y,
            layer.Key == "Floor" ? -1 :
              layer.Key == "Roof" ? 1 : 0);
          this.Children.Add(tile);
        }
    }
  }
}