using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame.Content.Loader
{
  public class TiledTmx
  {
    [XmlRoot(ElementName = "chunk")]
    public class Chunk
    {
      [XmlAttribute(AttributeName = "height")]
      public string Height { get; set; }
      [XmlText]
      public string Text { get; set; }
      [XmlAttribute(AttributeName = "width")]
      public string Width { get; set; }
      [XmlAttribute(AttributeName = "x")]
      public string X { get; set; }
      [XmlAttribute(AttributeName = "y")]
      public string Y { get; set; }
    }

    [XmlRoot(ElementName = "data")]
    public class Data
    {
      [XmlElement(ElementName = "chunk")]
      public List<Chunk> Chunk { get; set; }
      [XmlAttribute(AttributeName = "encoding")]
      public string Encoding { get; set; }
    }

    [XmlRoot(ElementName = "editorsettings")]
    public class Editorsettings
    {
      [XmlElement(ElementName = "export")]
      public Export Export { get; set; }
    }

    [XmlRoot(ElementName = "export")]
    public class Export
    {
      [XmlAttribute(AttributeName = "format")]
      public string Format { get; set; }
      [XmlAttribute(AttributeName = "target")]
      public string Target { get; set; }
    }

    [XmlRoot(ElementName = "layer")]
    public class Layer
    {
      [XmlElement(ElementName = "data")]
      public Data Data { get; set; }
      [XmlAttribute(AttributeName = "height")]
      public string Height { get; set; }
      [XmlAttribute(AttributeName = "id")]
      public string Id { get; set; }
      [XmlAttribute(AttributeName = "name")]
      public string Name { get; set; }
      [XmlAttribute(AttributeName = "offsetx")]
      public string Offsetx { get; set; }
      [XmlAttribute(AttributeName = "offsety")]
      public string Offsety { get; set; }
      [XmlAttribute(AttributeName = "width")]
      public string Width { get; set; }
    }

    [XmlRoot(ElementName = "map")]
    public class Map
    {
      [XmlElement(ElementName = "editorsettings")]
      public Editorsettings Editorsettings { get; set; }
      [XmlAttribute(AttributeName = "height")]
      public string Height { get; set; }
      [XmlAttribute(AttributeName = "infinite")]
      public string Infinite { get; set; }
      [XmlElement(ElementName = "layer")]
      public List<Layer> Layer { get; set; }
      [XmlAttribute(AttributeName = "nextlayerid")]
      public string Nextlayerid { get; set; }
      [XmlAttribute(AttributeName = "nextobjectid")]
      public string Nextobjectid { get; set; }
      [XmlAttribute(AttributeName = "orientation")]
      public string Orientation { get; set; }
      [XmlAttribute(AttributeName = "renderorder")]
      public string Renderorder { get; set; }
      [XmlAttribute(AttributeName = "tiledversion")]
      public string Tiledversion { get; set; }
      [XmlAttribute(AttributeName = "tileheight")]
      public string Tileheight { get; set; }
      [XmlElement(ElementName = "tileset")]
      public Tileset Tileset { get; set; }
      [XmlAttribute(AttributeName = "tilewidth")]
      public string Tilewidth { get; set; }
      [XmlAttribute(AttributeName = "version")]
      public string Version { get; set; }
      [XmlAttribute(AttributeName = "width")]
      public string Width { get; set; }
    }

    [XmlRoot(ElementName = "tileset")]
    public class Tileset
    {
      [XmlAttribute(AttributeName = "firstgid")]
      public string Firstgid { get; set; }
      [XmlAttribute(AttributeName = "source")]
      public string Source { get; set; }
    }

  }

  public class TiledMap
  {
    public Dictionary<string, Texture2D[][]> layer;
    public TiledMap(string tmxFilePath, TiledTileset tileset)
    {
      var xmlStream = System.IO.File.OpenRead(tmxFilePath);
      var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TiledTmx.Map));
      var tiledTmx = (TiledTmx.Map)serializer.Deserialize(xmlStream);
    }
  }
}