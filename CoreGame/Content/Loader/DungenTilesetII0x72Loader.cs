using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame.Content.Loader
{
  public class DungenonTilesetII0x72Loader
  {
    private Dictionary<string, Rectangle[]> sprites;
    public Texture2D Texture { get; private set; }

    public DungenonTilesetII0x72Loader()
    {
      this.sprites = new Dictionary<string, Rectangle[]>();
    }

    public void LoadContent(ContentManager contentManager)
    {
      var tileList = System.IO.File.ReadAllText(Content.FilePath0x72DungeonTilesetSpriteSheetList);

      foreach (var line in tileList.Split('\n'))
      {
        if (line.Length == 0)
          continue;

        var lineInfos = line.Split(' ').Where(l => l.Length > 0).ToArray();
        if (lineInfos.Length == 5)
        {
          this.sprites.Add(lineInfos[0],
          new Rectangle[] { new Rectangle(
            int.Parse(lineInfos[1], System.Globalization.NumberStyles.Integer),
              int.Parse(lineInfos[2], System.Globalization.NumberStyles.Integer),
              int.Parse(lineInfos[3], System.Globalization.NumberStyles.Integer),
              int.Parse(lineInfos[4], System.Globalization.NumberStyles.Integer)
          )});
        }
        else
        {
          var rectangles = new Rectangle[int.Parse(lineInfos[5], System.Globalization.NumberStyles.Integer)];

          for (int i = 0; i < rectangles.Length; i++)
          {
            rectangles[i] = new Rectangle(
              int.Parse(lineInfos[1], System.Globalization.NumberStyles.Integer) + (i * 16),
              int.Parse(lineInfos[2], System.Globalization.NumberStyles.Integer),
              int.Parse(lineInfos[3], System.Globalization.NumberStyles.Integer),
              int.Parse(lineInfos[4], System.Globalization.NumberStyles.Integer)
            );
          }

          this.sprites.Add(lineInfos[0], rectangles);
        }
      }

      this.Texture = contentManager.Load<Texture2D>(Content.Texture2D0x72DungeonTilesetSpriteSheet);
    }

    public Rectangle[] TryGetSpriteCoordinates(string sprite)
    {
      return this.sprites[sprite];
    }
  }
}