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
    private SpriteBatch _spriteBatch;
    List<Entity> entities = new List<Entity>();

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
      entities.Add(new Player());
    }

    protected override void Initialize()
    {
      entities.ForEach((Entity entity) => entity.Initialize());

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

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
      GraphicsDevice.Clear(Color.CornflowerBlue);

      entities.ForEach((Entity entity) => entity.Draw(gameTime, _spriteBatch));

      base.Draw(gameTime);
    }
  }
}
