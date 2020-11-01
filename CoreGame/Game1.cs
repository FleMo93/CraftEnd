using System.Collections.Generic;
using CraftEnd.CoreGame;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd
{
  public class Game1 : Game
  {
    private GraphicsDeviceManager _graphics;
    List<Entity> entities = new List<Entity>();

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      _graphics.PreferredBackBufferWidth = 800;
      _graphics.PreferredBackBufferHeight = 800;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
      entities.Add(new Player { Position = new Vector2(0, 0) });
    }

    protected override void Initialize()
    {
      entities.ForEach((Entity entity) => entity.Initialize());

      base.Initialize();
    }

    protected override void LoadContent()
    {
      Renderer.LoadContent(GraphicsDevice, _graphics, 8, Content.Load<SpriteFont>("Arial"));
      entities.ForEach((Entity entity) => entity.LoadContent(Content));
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      entities.ForEach((Entity entity) => entity.Update(gameTime));

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      Renderer.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}
