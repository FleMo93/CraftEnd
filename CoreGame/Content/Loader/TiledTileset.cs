using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame.Content.Loader
{
  public class TiledTsx
  {
    [XmlRoot(ElementName = "animation")]
    public class Animation
    {
      [XmlElement(ElementName = "frame")]
      public List<Frame> Frame { get; set; }
    }

    [XmlRoot(ElementName = "frame")]
    public class Frame
    {
      [XmlAttribute(AttributeName = "duration")]
      public string Duration { get; set; }
      [XmlAttribute(AttributeName = "tileid")]
      public string Tileid { get; set; }
    }

    [XmlRoot(ElementName = "image")]
    public class Image
    {
      [XmlAttribute(AttributeName = "height")]
      public string Height { get; set; }
      [XmlAttribute(AttributeName = "source")]
      public string Source { get; set; }
      [XmlAttribute(AttributeName = "width")]
      public string Width { get; set; }
    }

    [XmlRoot(ElementName = "object")]
    public class Object
    {
      [XmlAttribute(AttributeName = "height")]
      public string Height { get; set; }
      [XmlAttribute(AttributeName = "id")]
      public string Id { get; set; }
      [XmlAttribute(AttributeName = "width")]
      public string Width { get; set; }
      [XmlAttribute(AttributeName = "x")]
      public string X { get; set; }
      [XmlAttribute(AttributeName = "y")]
      public string Y { get; set; }
    }

    [XmlRoot(ElementName = "objectgroup")]
    public class Objectgroup
    {
      [XmlAttribute(AttributeName = "draworder")]
      public string Draworder { get; set; }
      [XmlAttribute(AttributeName = "id")]
      public string Id { get; set; }
      [XmlElement(ElementName = "object")]
      public List<Object> Object { get; set; }
    }

    [XmlRoot(ElementName = "properties")]
    public class Properties
    {
      [XmlElement(ElementName = "property")]
      public List<Property> Property { get; set; }
    }

    [XmlRoot(ElementName = "property")]
    public class Property
    {
      [XmlAttribute(AttributeName = "name")]
      public string Name { get; set; }
      [XmlAttribute(AttributeName = "value")]
      public string Value { get; set; }
    }

    [XmlRoot(ElementName = "tile")]
    public class Tile
    {
      [XmlElement(ElementName = "animation")]
      public Animation Animation { get; set; }
      [XmlAttribute(AttributeName = "id")]
      public string Id { get; set; }
      [XmlElement(ElementName = "objectgroup")]
      public Objectgroup Objectgroup { get; set; }
      [XmlElement(ElementName = "properties")]
      public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "tileset")]
    public class Tileset
    {
      [XmlAttribute(AttributeName = "columns")]
      public string Columns { get; set; }
      [XmlElement(ElementName = "image")]
      public Image Image { get; set; }
      [XmlAttribute(AttributeName = "name")]
      public string Name { get; set; }
      [XmlElement(ElementName = "properties")]
      public Properties Properties { get; set; }
      [XmlElement(ElementName = "tile")]
      public List<Tile> Tile { get; set; }
      [XmlAttribute(AttributeName = "tilecount")]
      public string Tilecount { get; set; }
      [XmlAttribute(AttributeName = "tiledversion")]
      public string Tiledversion { get; set; }
      [XmlAttribute(AttributeName = "tileheight")]
      public string Tileheight { get; set; }
      [XmlAttribute(AttributeName = "tilewidth")]
      public string Tilewidth { get; set; }
      [XmlAttribute(AttributeName = "version")]
      public string Version { get; set; }
    }
  }

  public class TilesetTileAnimation
  {
    public TilesetTile TilesetTile { get; private set; }
    public int AnimationDuration { get; private set; }

    public TilesetTileAnimation(TilesetTile tilesetTile, int animationDuration)
    {
      this.TilesetTile = tilesetTile;
      this.AnimationDuration = animationDuration;
    }
  }

  public class BoxColiderDefinitions
  {
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
  }

  public class TilesetTile
  {
    public string Id { get; private set; }
    public bool IsAnimated { get { return this.AnimationList.Count > 0; } }
    public List<TilesetTileAnimation> AnimationList { get; private set; }
    public Rectangle Rectangle { get; private set; }
    public Vector2 Offset { get; set; } = new Vector2();
    public IEnumerable<BoxColiderDefinitions> BoxColiderDefinitions { get; private set; }

    public TilesetTile(string id, Rectangle rectangle, List<TilesetTileAnimation> animationIdList, IEnumerable<BoxColiderDefinitions> boxColiderDefinitions)
    {
      this.Id = id;
      this.AnimationList = animationIdList;
      this.Rectangle = rectangle;
      this.BoxColiderDefinitions = boxColiderDefinitions;
    }
  }

  public class TiledTileset
  {
    public Texture2D TextureAtlas { get; private set; }
    public Dictionary<string, TilesetTile> Tiles { get; }

    public TiledTileset(string tsxFilePath, ContentManager contentManager)
    {
      var xmlStream = System.IO.File.OpenRead(tsxFilePath);
      var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TiledTsx.Tileset));
      var tiledTsx = (TiledTsx.Tileset)serializer.Deserialize(xmlStream);

      var textureAtlasPath = tiledTsx.Properties.Property.Find(p => p.Name == "contentPath");
      if (textureAtlasPath == null || string.IsNullOrWhiteSpace(textureAtlasPath.Value))
        throw new System.NullReferenceException("Missing contentPath on " + tsxFilePath);

      TextureAtlas = contentManager.Load<Texture2D>(textureAtlasPath.Value);
      Tiles = new Dictionary<string, TilesetTile>();

      var columns = int.Parse(tiledTsx.Columns, System.Globalization.NumberStyles.Integer);
      var rows = int.Parse(tiledTsx.Tilecount, System.Globalization.NumberStyles.Integer) / columns;
      var tileWidth = int.Parse(tiledTsx.Tilewidth, System.Globalization.NumberStyles.Integer);
      var tileHeight = int.Parse(tiledTsx.Tileheight, System.Globalization.NumberStyles.Integer);
      Dictionary<string, List<TilesetTileAnimation>> animationListById = new Dictionary<string, List<TilesetTileAnimation>>();

      for (int y = 0; y < rows; y++)
        for (int x = 0; x < columns; x++)
        {
          var id = y * columns + x;
          animationListById.Add(id.ToString(), new List<TilesetTileAnimation>());

          var specialTile = tiledTsx.Tile.FirstOrDefault(t => t.Id == id.ToString());

          var boxColiderDefinitions = new List<BoxColiderDefinitions>();
          if (specialTile != null && specialTile.Objectgroup != null)
          {
            specialTile.Objectgroup.Object.ForEach(o =>
            {
              boxColiderDefinitions.Add(new BoxColiderDefinitions
              {
                Position = new Vector2(float.Parse(o.X) / tileWidth, float.Parse(o.Y) / tileHeight),
                Size = new Vector2(float.Parse(o.Width) / tileWidth, float.Parse(o.Height) / tileHeight)
              });
            });
          }

          Tiles.Add(id.ToString(), new TilesetTile(
            id.ToString(),
            new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight),
            animationListById[id.ToString()],
            boxColiderDefinitions
          ));
        }

      foreach (var specialTile in tiledTsx.Tile)
      {
        if (specialTile.Animation != null)
        {
          specialTile.Animation.Frame.ForEach(f => animationListById[specialTile.Id].Add(
            new TilesetTileAnimation(
              Tiles[f.Tileid],
              int.Parse(f.Duration, System.Globalization.NumberStyles.Integer))));
        }
      }
    }
  }
}