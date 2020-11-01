using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class Animation
  {
    public string Name { get; private set; }
    public double SpriteTime { get; private set; }
    public int NumberOfSprites { get; private set; }
    private Texture2D[] textures;
    private Rectangle[] subTextureCoordinates;

    private Animation(string name, double spriteTime = 0.16f)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new System.NullReferenceException();

      this.Name = name;
      this.SpriteTime = spriteTime;
    }

    public Animation(string name, Texture2D[] textures, double spriteTime = 0.16f) : this(name, spriteTime)
    {
      if (textures.Length == 0)
        throw new System.NullReferenceException("Animation must at least contain one frame");

      this.textures = textures;
      this.NumberOfSprites = textures.Length;
    }

    public Animation(string name, Texture2D texture, Rectangle[] subTextureCoordinates, double spriteTime = 0.16f) : this(name, spriteTime)
    {
      if (subTextureCoordinates.Length == 0)
        throw new System.NullReferenceException("Animation must at least contain one frame");

      this.textures = new Texture2D[] { texture };
      this.NumberOfSprites = subTextureCoordinates.Length;
      this.subTextureCoordinates = subTextureCoordinates;
    }

    public Tuple<Texture2D, Rectangle?> GetSpriteInfo(int spriteIndex)
    {
      if (this.subTextureCoordinates != null)
        return new Tuple<Texture2D, Rectangle?>(this.textures[0], this.subTextureCoordinates[spriteIndex]);
      else
        return new Tuple<Texture2D, Rectangle?>(this.textures[spriteIndex], null);
    }
  }
}