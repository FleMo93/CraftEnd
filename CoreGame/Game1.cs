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
    private Player player;
    private Camera camera;

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      _graphics.PreferredBackBufferWidth = 800;
      _graphics.PreferredBackBufferHeight = 800;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;

      this.player = new Player();
      this.player.Position = new Vector3(0, 17.5f, 0);
    }

    protected override void Initialize()
    {
      Entity.Entities.ForEach((Entity entity) => entity.Initialize());

      base.Initialize();
    }

    protected override void LoadContent()
    {
      Renderer.LoadContent(GraphicsDevice, _graphics, Content.Load<Texture2D>(CraftEnd.CoreGame.Content.Content.Texture2DCoordinateAxis), Content.Load<SpriteFont>("Arial"));
      var renderLayer = Renderer.CreateRenderLayer(25);
      this.camera = renderLayer.Camera;
      this.camera.RenderPivot = RenderPivot.Center;
      var tiledTileSet = new TiledTileset(CraftEnd.CoreGame.Content.Content.FilePathTiled0x72DungenTileset, Content);
      var devLevelMap = new TiledMap(CraftEnd.CoreGame.Content.Content.FilePathTiledLevelDev, tiledTileSet);
      var devLevel = new Level(devLevelMap);
      Entity.Entities.Insert(0, devLevel);

      Entity.Entities.ForEach((Entity entity) =>
      {
        entity.LoadContent(Content);
        renderLayer.AddEntity(entity);
      });

      var dungeonTileSet0x72Loader = new DungenonTilesetII0x72Loader();
      dungeonTileSet0x72Loader.LoadContent(Content);
      var characterShadow = Content.Load<Texture2D>(CraftEnd.CoreGame.Content.Content.Texture2DCharacterShadow);
      this.player.LoadContent(dungeonTileSet0x72Loader, characterShadow);

      var guiLayer = Renderer.CreateRenderLayer(20);
      var uiLife = new Life();
      guiLayer.AddEntity(uiLife);
      uiLife.LoadContent(dungeonTileSet0x72Loader);
      uiLife.NumberOfHearts = 5;
      uiLife.Position = new Vector3(0.1f, 0, 0);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      Entity.Entities.ForEach((Entity entity) => entity.Update(gameTime));
      this.camera.Position = this.player.Position;

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      Renderer.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}
