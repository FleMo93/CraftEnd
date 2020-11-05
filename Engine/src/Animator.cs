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
      public Vector2 Offset { get; set; } = new Vector2(0, 0);
      public Vector2 Scale { get; set; } = new Vector2(1, 1);
      public RenderPivot RenderPivot { get; set; } = RenderPivot.TopLeft;

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
      internal override void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch)
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

        var height = this.Entity.Scale.Y * renderLayer.PixelMetersMultiplier;
        var width = this.Entity.Scale.X * renderLayer.PixelMetersMultiplier;

        if (subTexture.HasValue)
          if (subTexture.Value.Height > subTexture.Value.Width)
            width = width * subTexture.Value.Width / subTexture.Value.Height;
          else
            height = height * subTexture.Value.Width / subTexture.Value.Height;
        else
        {
          if (currentSprite.Height > currentSprite.Width)
            width = width * currentSprite.Height / currentSprite.Width;
          else
            height = height * currentSprite.Width / currentSprite.Height;
        }

        height = height * this.Scale.Y;
        width = width * this.Scale.X;
        var x = this.Entity.Position.X * renderLayer.PixelMetersMultiplier +
            this.Offset.X * renderLayer.PixelMetersMultiplier * this.Entity.Scale.X +
            renderLayer.Position.X * renderLayer.PixelMetersMultiplier;
        var y = this.Entity.Position.Y * renderLayer.PixelMetersMultiplier +
            this.Offset.Y * renderLayer.PixelMetersMultiplier * this.Entity.Scale.Y +
            renderLayer.Position.Y * renderLayer.PixelMetersMultiplier;

        switch (this.RenderPivot)
        {
          case RenderPivot.Center:
            x = x - width / 2;
            y = y - height / 2;
            break;
        }

        spriteBatch.Draw(currentSprite, new Rectangle
        {
          X = (int)x,
          Y = (int)y,
          Height = (int)height,
          Width = (int)width
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