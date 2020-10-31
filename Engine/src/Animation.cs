using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class Animation
  {
    public string Name { get; private set; }
    public Texture2D[] Sprites { get; private set; }
    public double SpriteTime { get; private set; }

    public Animation(string name, Texture2D[] sprites, double spriteTime = 0.16f)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new System.NullReferenceException();

      if (sprites.Length == 0)
        throw new System.NullReferenceException("Animation must at least contain one frame");

      this.Name = name;
      this.Sprites = sprites;
      this.SpriteTime = spriteTime;
    }
  }
}