using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public partial class Entity
  {
    public class Animator : Component
    {
      public string CurrentAnimationName { get; private set; }
      public Animation CurrentAnimation { get { return this.animations[this.CurrentAnimationName]; } }
      public bool FlipHorizontal { get; set; } = false;
      private bool hasAnimationChanged = false;
      private double spriteTime = 0;
      private int spriteNumber = 0;
      private Dictionary<string, Animation> animations;

      public Animator(Animation[] animations)
      {
        this.animations = new Dictionary<string, Animation>();

        foreach (var animation in animations)
        {
          if (this.animations.ContainsKey(animation.Name))
            throw new System.Exception("Animation with name already exists");

          this.animations.Add(animation.Name, animation);
        }

        if (animations.Length > 0)
          this.CurrentAnimationName = animations[0].Name;
      }
      public Animator(Animation[] animations, string startAnimation) : this(animations)
      {
        this.CurrentAnimationName = startAnimation;
      }
      public void SetAnimation(string animationName)
      {
        if (this.CurrentAnimationName == animationName)
          return;

        if (!animations.ContainsKey(animationName))
          throw new System.NullReferenceException("Animation not found");

        this.CurrentAnimationName = animationName;
        this.hasAnimationChanged = true;
      }
      int prevNumber = -1;
      internal override void Draw(GameTime gameTime, RenderLayer renderer, SpriteBatch spriteBatch)
      {
        Texture2D currentSprite;
        Rectangle? subTexture = null;

        if (this.hasAnimationChanged)
        {
          this.spriteTime = 0;
          this.spriteNumber = 0;
          var spriteInfo = this.CurrentAnimation.GetSpriteInfo(0);
          currentSprite = spriteInfo.Item1;
          subTexture = spriteInfo.Item2;
        }
        else
        {
          this.spriteTime += gameTime.ElapsedGameTime.TotalSeconds;
          if (spriteTime > this.CurrentAnimation.SpriteTime)
          {
            var nextSpriteNumber = this.spriteNumber + (int)(this.spriteTime / this.CurrentAnimation.SpriteTime);

            if (nextSpriteNumber > 0)
              nextSpriteNumber = nextSpriteNumber % this.CurrentAnimation.NumberOfSprites;

            var spriteInfo = this.CurrentAnimation.GetSpriteInfo(nextSpriteNumber);
            currentSprite = spriteInfo.Item1;
            subTexture = spriteInfo.Item2;
            prevNumber = nextSpriteNumber;
            this.spriteTime = this.spriteTime % this.CurrentAnimation.SpriteTime;
            this.spriteNumber = nextSpriteNumber;
          }
          else
          {
            var spriteInfo = this.CurrentAnimation.GetSpriteInfo(this.spriteNumber);
            currentSprite = spriteInfo.Item1;
            subTexture = spriteInfo.Item2;
          }
        }

        spriteBatch.Draw(currentSprite, new Rectangle
        {
          X = (int)(this.Entity.Position.X * renderer.PixelMetersMultiplier),
          Y = (int)(this.Entity.Position.Y * renderer.PixelMetersMultiplier),
          Height = (int)(this.Entity.Scale.X * renderer.PixelMetersMultiplier),
          Width = (int)(this.Entity.Scale.Y * renderer.PixelMetersMultiplier)
        },
        subTexture, Color.White, 0, new Vector2(0, 0),
        this.FlipHorizontal ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
        1);

        if (this.hasAnimationChanged)
          this.hasAnimationChanged = false;
      }
    }
  }
}