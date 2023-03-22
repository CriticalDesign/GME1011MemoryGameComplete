using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GME1011MemoryGameComplete
{
    public class Game1 : Game
    {
        //attributes that MonoGame uses
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //here are our 12 cards
        private Card _card1, _card2, _card3, _card4, _card5, _card6;
        private Card _card7, _card8, _card9, _card10, _card11, _card12;
        private Timer _timer;
        private GameOver _gameOver;
        private ScoreKeeper _scoreKeeper;

        //here is our dealer 
        private Dealer _dealer;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            //this is how you can easily change resolutions (the size of the window)
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            //MonoGame stuff.
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        //we didn't put anything in initialize for our card memory game
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //MonoGame did this
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Initialize our 12 cards by calling the card constructor 12x with a variety 
            //of card types, x and y values, and textures
            int startX = 167; //left and right border pixels
            int startY = 100; //top border pixels
            int cardWidth = 125; //card width with spacing
            int cardHeight = 167; //card height with spacing

            string[] labels = { "crocHero", "crocHero", "star", "star", "dragonHero", "dragonHero", "mana", "mana", "skellie", "skellie", "health", "health"};
            Random rng = new Random();
            for(int i = 0; i < labels.Length; i++)
            {
                int randomIndex = rng.Next(0, labels.Length);
                string temp = labels[i];
                labels[i] = labels[randomIndex];
                labels[randomIndex] = temp;
            }

            _card1 = new Card(labels[0], startX, startY, Content.Load<Texture2D>(labels[0]), Content.Load<Texture2D>("CardGreen"));
            _card2 = new Card(labels[1], startX+cardWidth, startY, Content.Load<Texture2D>(labels[1]), Content.Load<Texture2D>("CardGreen"));
            _card3 = new Card(labels[2], startX + cardWidth*2, startY, Content.Load<Texture2D>(labels[2]), Content.Load<Texture2D>("CardGreen"));
            _card4 = new Card(labels[3], startX + cardWidth*3, startY, Content.Load<Texture2D>(labels[3]), Content.Load<Texture2D>("CardGreen"));
            _card5 = new Card(labels[4], startX, startY + cardHeight, Content.Load<Texture2D>(labels[4]), Content.Load<Texture2D>("CardGreen"));
            _card6 = new Card(labels[5], startX + cardWidth, startY + cardHeight, Content.Load<Texture2D>(labels[5]), Content.Load<Texture2D>("CardGreen"));

            _card7 = new Card(labels[6], startX + cardWidth * 2, startY + cardHeight, Content.Load<Texture2D>(labels[6]), Content.Load<Texture2D>("CardGreen"));
            _card8 = new Card(labels[7], startX + cardWidth * 3, startY + cardHeight, Content.Load<Texture2D>(labels[7]), Content.Load<Texture2D>("CardGreen"));
            _card9 = new Card(labels[8], startX, startY + cardHeight*2, Content.Load<Texture2D>(labels[8]), Content.Load<Texture2D>("CardGreen"));
            _card10 = new Card(labels[9], startX + cardWidth, startY + cardHeight*2, Content.Load<Texture2D>(labels[9]), Content.Load<Texture2D>("CardGreen"));
            _card11 = new Card(labels[10], startX + cardWidth*2, startY + cardHeight*2, Content.Load<Texture2D>(labels[10]), Content.Load<Texture2D>("CardGreen"));
            _card12 = new Card(labels[11], startX + cardWidth*3, startY + cardHeight*2, Content.Load<Texture2D>(labels[11]), Content.Load<Texture2D>("CardGreen"));


            //initialize the dealer
            _dealer = new Dealer();
            _timer = new Timer(Content.Load<SpriteFont>("TextFont"));
            _gameOver = new GameOver(Content.Load<Texture2D>("gameOver"));
            _scoreKeeper = new ScoreKeeper(Content.Load<SpriteFont>("TextFont"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //call the dealer's update - the dealer has stuff to do
            //every time step
            _dealer.Update(gameTime, _scoreKeeper);

            //If there are two cards flipped over, then the action
            //is frozen (no more cards can be flipped).
            //If this is the case, don't called the cards' update.
            if (_dealer.isActionFrozen() == false)
            {
                //if the action isn't frozen, call the card update
                _card1.Update(_dealer);
                _card2.Update(_dealer);
                _card3.Update(_dealer);
                _card4.Update(_dealer);
                _card5.Update(_dealer);
                _card6.Update(_dealer);
                _card7.Update(_dealer);
                _card8.Update(_dealer);
                _card9.Update(_dealer);
                _card10.Update(_dealer);
                _card11.Update(_dealer);
                _card12.Update(_dealer);
            }
            if(!_dealer.isGameOver())
                _timer.Update(gameTime);

            //MonoGame did this.
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //the cards need to draw their awesomeness!!
            _card1.Draw(_spriteBatch);
            _card2.Draw(_spriteBatch);
            _card3.Draw(_spriteBatch);
            _card4.Draw(_spriteBatch);
            _card5.Draw(_spriteBatch);
            _card6.Draw(_spriteBatch);
            _card7.Draw(_spriteBatch);
            _card8.Draw(_spriteBatch);
            _card9.Draw(_spriteBatch);
            _card10.Draw(_spriteBatch);
            _card11.Draw(_spriteBatch);
            _card12.Draw(_spriteBatch);

            _timer.Draw(_spriteBatch);

            if (_dealer.isGameOver())
                _gameOver.Draw(_spriteBatch);

            _scoreKeeper.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}