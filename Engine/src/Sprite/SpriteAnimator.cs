using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public partial class Entity
  {
    public class SpriteAnimator : Sprite
    {
      public string CurrentAnimationName { get; private set; }
      public SpriteAnimation CurrentAnimation { get { return this.animations[this.CurrentAnimationName]; } }
      private bool hasAnimationChanged = false;
      private double spriteTime = 0;
      private int spriteNumber = 0;
      private Dictionary<string, SpriteAnimation> animations;

      public SpriteAnimator(Entity entity, SpriteAnimation[] animations) : base(entity)
      {
        if (animations == null || animations.Length == 0)
          throw new System.Exception("At least one animation is required");

        this.animations = new Dictionary<string, SpriteAnimation>();

        foreach (var animation in animations)
        {
          if (this.animations.ContainsKey(animation.Name))
            throw new System.Exception("Animation with name already exists");

          this.animations.Add(animation.Name, animation);
        }

        this.CurrentAnimationName = animations[0].Name;
        var spriteInfo = animations[0].GetSpriteInfo(0);
        this.Texture = spriteInfo.Item1;
        this.SpriteCoordinates = spriteInfo.Item2;
      }
      public SpriteAnimator(Entity entity, SpriteAnimation[] animations, string startAnimation) : this(entity, animations)
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
      internal override void Update(GameTime gameTime)
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

        this.Texture = currentSprite;
        this.SpriteCoordinates = subTexture;

        if (this.hasAnimationChanged)
          this.hasAnimationChanged = false;
      }
    }
  }
}