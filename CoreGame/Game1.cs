﻿using CraftEnd.CoreGame;
using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using CraftEnd.Engine.Physics;
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
    private Cursor cursor;
    private PositionAxis positionAxis;

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      _graphics.PreferredBackBufferWidth = 800;
      _graphics.PreferredBackBufferHeight = 800;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;

      this.player = new Player();
      this.player.Position = new Vector3(2, 2, 0);
      this.cursor = new Cursor();
      this.positionAxis = new PositionAxis();
    }

    protected override void Initialize()
    {
      Entity.Entities.ForEach((Entity entity) => entity.Initialize());

      base.Initialize();
      IsMouseVisible = false;
    }

    protected override void LoadContent()
    {
      Renderer.LoadContent(GraphicsDevice, _graphics, Content.Load<Texture2D>(CraftEnd.CoreGame.Content.Content.Texture2DCoordinateAxis), Content.Load<SpriteFont>("Arial"));
      this.camera = new Camera(_graphics, GraphicsDevice);
      this.camera.Zoom = 40;
      this.camera.RenderPivot = RenderPivot.Center;

      var tiledTileSet = new TiledTileset(CraftEnd.CoreGame.Content.Content.FilePathTiled0x72DungenTileset, Content);
      var devLevelMap = new TiledMap(CraftEnd.CoreGame.Content.Content.FilePathTiledLevelDev, tiledTileSet);
      var devLevel = new Level(devLevelMap);
      this.camera.AddEntity(devLevel);
      devLevel.Children.ForEach(t => this.camera.AddEntity(t));

      this.camera.AddEntity(this.player);
      this.camera.AddEntity(this.positionAxis);
      Entity.Entities.ForEach((Entity entity) => entity.LoadContent(Content));

      var dungeonTileSet0x72Loader = new DungenonTilesetII0x72Loader();
      dungeonTileSet0x72Loader.LoadContent(Content);

      var characterShadow = Content.Load<Texture2D>(CraftEnd.CoreGame.Content.Content.Texture2DCharacterShadow);
      this.player.LoadContent(dungeonTileSet0x72Loader, characterShadow);

      var guiCamera = new Camera(_graphics, GraphicsDevice);
      guiCamera.Zoom = 60;
      var uiLife = new Life();
      guiCamera.AddEntity(uiLife);
      uiLife.LoadContent(dungeonTileSet0x72Loader);
      uiLife.NumberOfHearts = 5;
      uiLife.Position = new Vector3(0.1f, 0, 0);
      guiCamera.AddEntity(this.cursor);
      this.cursor.LoadContent(Content.Load<Texture2D>(CraftEnd.CoreGame.Content.Content.Texture2DCursor), guiCamera);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      Entity.Entities.ForEach((Entity entity) => entity.Update(gameTime));
      this.camera.Position.X = this.player.Position.X;
      this.camera.Position.Y = this.player.Position.Y;

      RaycastHit hit;
      Vector2 startPosition = new Vector2(this.player.Position.X, this.player.Position.Y);
      Vector2 endPosition = this.camera.ScreenToWorldPosition(Mouse.GetState().X, Mouse.GetState().Y);

      if (Raycast.Cast(
        startPosition,
        Vector2.Normalize(endPosition - startPosition),
        Vector2.Distance(startPosition, endPosition),
        out hit))
      {
        this.positionAxis.Position.X = hit.Point.X;
        this.positionAxis.Position.Y = hit.Point.Y;
      }
      else
      {
        this.positionAxis.Position.X = float.MaxValue;
        this.positionAxis.Position.Y = float.MaxValue;
      }

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      Renderer.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}
