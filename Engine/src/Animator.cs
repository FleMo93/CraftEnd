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
      internal override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
      {
        Texture2D currentSprite;

        if (this.hasAnimationChanged)
        {
          this.spriteTime = 0;
          this.spriteNumber = 0;
          currentSprite = this.CurrentAnimation.Sprites[0];
        }
        else
        {
          this.spriteTime += gameTime.ElapsedGameTime.TotalSeconds;
          if (spriteTime > this.CurrentAnimation.SpriteTime)
          {
            var nextSpriteNumber = this.spriteNumber + (int)(this.spriteTime / this.CurrentAnimation.SpriteTime);

            if (nextSpriteNumber > 0)
              nextSpriteNumber = nextSpriteNumber % this.CurrentAnimation.Sprites.Length;

            currentSprite = this.CurrentAnimation.Sprites[nextSpriteNumber];
            prevNumber = nextSpriteNumber;
            this.spriteTime = this.spriteTime % this.CurrentAnimation.SpriteTime;
            this.spriteNumber = nextSpriteNumber;
          }
          else
            currentSprite = this.CurrentAnimation.Sprites[this.spriteNumber];
        }

        spriteBatch.Begin();
        spriteBatch.Draw(currentSprite, this.Entity.Position, Color.White);
        spriteBatch.End();

        if (this.hasAnimationChanged)
          this.hasAnimationChanged = false;
      }
    }
  }
}