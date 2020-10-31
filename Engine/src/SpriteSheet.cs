using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public partial class Entity
  {
    public partial class SpriteSheet : Component
    {
      public int SpriteCountHeight { get; private set; }
      public int SpriteCountWidth { get; private set; }
      public int SpriteWidth { get; private set; }
      public int SpriteHeight { get; private set; }
      public Texture2D[] SpritesByColumn { get; private set; }
      public Texture2D[] SpritesByRow { get; private set; }

      private SpriteSheet() {}
      public SpriteSheet(Texture2D texture2D, int spirteCountHeight, int spriteCountWidth)
      {
        this.SpriteCountHeight = spirteCountHeight;
        this.SpriteCountWidth = spriteCountWidth;
        this.SpritesByColumn = new Texture2D[SpriteCountHeight * SpriteCountWidth];
        this.SpritesByRow = new Texture2D[SpriteCountHeight * SpriteCountWidth];

        SpriteHeight = texture2D.Height / SpriteCountHeight;
        SpriteWidth = texture2D.Width / SpriteCountWidth;

        var spriteSheetColor = new Color[texture2D.Width * texture2D.Height];
        texture2D.GetData<Color>(spriteSheetColor);

        for (var countY = 0; countY < SpriteCountHeight; countY++)
          for (var countX = 0; countX < SpriteCountWidth; countX++)
          {
            var startX = countX * SpriteWidth;
            var startY = countY * SpriteHeight;
            var endX = startX + SpriteWidth;
            var endY = startY + SpriteHeight;

            var sprite = new Texture2D(texture2D.GraphicsDevice, SpriteWidth, SpriteHeight);
            var spriteColor = new Color[SpriteHeight * SpriteWidth];

            for (var y = startY; y < endY; y++)
              for (var x = startX; x < endX; x++)
                spriteColor[x - startX + ((y - startY) * SpriteWidth)] = (Color)spriteSheetColor.GetValue(x + ((countY * SpriteHeight + y - startY) * texture2D.Width));

            sprite.SetData<Color>(spriteColor);
            SpritesByColumn[countY + countX * SpriteCountHeight] = sprite;
            SpritesByRow[countX + countY * SpriteCountWidth] = sprite;
          }
      }
    }
  }
}
