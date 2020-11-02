using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame.Content.Loader
{
  public class TiledTsx
  {
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
      [XmlAttribute(AttributeName = "id")]
      public string Id { get; set; }
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

  public class TilesetTile
  {
    public string Id { get; private set; }
    public bool IsAnimated { get; private set; }
    public int? AnimationIndex { get; private set; }
    public List<string> AnimationIdList { get; private set; }
    public Rectangle Rectangle { get; private set; }

    public TilesetTile(string id, Rectangle rectangle, bool isAnimated, int? animationIndex, List<string> animationIdList)
    {
      this.Id = id;
      this.IsAnimated = isAnimated;
      this.AnimationIndex = animationIndex;
      this.AnimationIdList = animationIdList;
      this.Rectangle = rectangle;
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
      Dictionary<string, List<string>> animationIdsByName = new Dictionary<string, List<string>>();

      for (int y = 0; y < rows; y++)
        for (int x = 0; x < columns; x++)
        {
          var id = y * columns + x;
          string animationName = null;
          int? animationIndex = null;
          var specialTile = tiledTsx.Tile.Find(t => t.Id == id.ToString());

          if (specialTile != null)
          {
            var animationNameProperty = specialTile.Properties.Property.Find(p => p.Name == "animationName");

            if (animationNameProperty != null)
            {
              var animationIndexProperty = specialTile.Properties.Property.Find(p => p.Name == "animationIndex");

              if (animationIndexProperty == null)
                throw new System.NullReferenceException("Mission animationIndex property on tile id: " + specialTile.Id);

              animationName = animationNameProperty.Value;
              animationIndex = int.Parse(animationIndexProperty.Value, System.Globalization.NumberStyles.Integer);

              if (!animationIdsByName.ContainsKey(animationName))
                animationIdsByName.Add(animationName, new List<string>());

              animationIdsByName[animationName].Insert((int)animationIndex, id.ToString());
            }
          }

          Tiles.Add(id.ToString(), new TilesetTile(
            id.ToString(),
            new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight),
            animationIndex != null,
            animationIndex,
            animationName != null ? animationIdsByName[animationName] : null
          ));
        }
    }
  }
}