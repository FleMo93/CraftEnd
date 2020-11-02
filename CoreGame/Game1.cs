using System.Collections.Generic;
using CraftEnd.CoreGame;
using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd
{
  public class Game1 : Game
  {
    private GraphicsDeviceManager _graphics;
    private List<Entity> entities = new List<Entity>();
    private Player player;

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      _graphics.PreferredBackBufferWidth = 800;
      _graphics.PreferredBackBufferHeight = 800;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;

      this.player = new Player();
      entities.Add(this.player);
    }

    protected override void Initialize()
    {
      entities.ForEach((Entity entity) => entity.Initialize());

      base.Initialize();
    }

    protected override void LoadContent()
    {
      Renderer.LoadContent(GraphicsDevice, _graphics, Content.Load<SpriteFont>("Arial"));
      var renderLayer = Renderer.CreateRenderLayer(10);
      entities.ForEach((Entity entity) => {
        entity.LoadContent(Content);
        renderLayer.AddEntity(entity);
      });

      
      var tiledTileSet = new TiledTileset(CraftEnd.CoreGame.Content.Content.FilePathTiled0x72DungenTileset, Content);
      var devLevel = new TiledMap(CraftEnd.CoreGame.Content.Content.FilePathTiledLevelDev, tiledTileSet);

      var dungeonTileSet0x72Loader = new DungenonTilesetII0x72Loader();
      dungeonTileSet0x72Loader.LoadContent(Content);
      this.player.LoadContent(dungeonTileSet0x72Loader);
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
