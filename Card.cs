using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011MemoryGameComplete
{
    internal class Card
    {
        //card attributes
        private bool _isDown, _isDestroyed;  //state booleans
        private string _type;    //used for finding matches
        private int _myX, _myY;  //coordinates

        private Texture2D _faceUpTexture, _faceDownTexture;  //textures

        //this one is to make the mouse click last for only a single press
        private MouseState _lastMouseState, _currentMouseState;

        //argumented constructor!!! Set all the attributes
        public Card(string type, int myX, int myY, Texture2D faceUpTexture, Texture2D faceDownTexture)
        {
            _type = type;
            _myX = myX;
            _myY = myY;
            _faceUpTexture = faceUpTexture;
            _faceDownTexture = faceDownTexture;
            _isDown = true;  //this one is hard-coded because we don't want the cards to be face up when they start
        }

        //flip method
        public void flip(Dealer dealer)
        {
            _isDown = ! _isDown;  //change the flip state
            if (!_isDown)
            {
                //if the card is face up (! down) register the card
                //with the dealer
                dealer.RegisterFlip(this);
            }
        }

        //here's our card's always-running code!!
        public void Update(Dealer dealer)
        {
            if (!_isDestroyed) //if we are NOT destroyed (i.e., don't do this if we are destroyed)
            {
                //set the mouse state
                _lastMouseState = _currentMouseState;  
                _currentMouseState = Mouse.GetState();

                if (_lastMouseState.LeftButton == ButtonState.Released && _currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    //did we get a mouse click ON the card's coordinates - note the use of _myX, _myY and the texture bounds
                    //so that the cards can be anywhere and any shape and this will work. This is collision code 101.
                    if (_currentMouseState.X > _myX
                                && _currentMouseState.X < (_myX + _faceDownTexture.Width)
                                && _currentMouseState.Y > _myY
                                && _currentMouseState.Y < (_myY + _faceDownTexture.Height))
                    {
                        flip(dealer); //we got a click on the card!!!!! Let's flip it. Pass the dealer object.
                    }

                }
            }

        }

        //Lets draw nice things!
        public void Draw(SpriteBatch spritebatch)
        {
            if (!_isDestroyed)  //if we are not destroyed draw!! If we are destroyed, ignore.
            {
                spritebatch.Begin(); //always
                if (_isDown)  //face down draw
                {
                    spritebatch.Draw(_faceDownTexture, new Vector2(_myX, _myY), Color.White);
                }
                else //face up draw
                {
                    spritebatch.Draw(_faceUpTexture, new Vector2(_myX, _myY), Color.White);
                }
                spritebatch.End(); //always
            }
        }

        //easy method to get the type of the card for match checking
        public string GetCardType() { return _type; }

        //easy method to flag the card as destroyed after a match!!
        public void SetDestroyed() { _isDestroyed = true; }

    }
}
