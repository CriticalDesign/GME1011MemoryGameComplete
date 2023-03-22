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
    internal class Dealer
    {
        //the dealer only needs a few attributes.
        private Card _cardUp1, _cardUp2; //which cards are face up?
        private bool _actionFrozen;  //should we pause the action?
        private float _flipTime; //how long do we wait to flip cards back over?

        private int _numCards = 12;

        //in this method we notify the dealer of which cards have been flipped face up.
        //this method is called by the Card class' flip( ) method.
        public void RegisterFlip(Card cardFlipped)
        {
            if(_cardUp1 == null) //there's no card in cardUp1 right now
            {
                _cardUp1 = cardFlipped;  //put this card in cardUp1
            }
            else if(_cardUp2 == null) //no card in cardUp2 right now
            {
                //two cards have been flipped, lots to do!!!
                _cardUp2 = cardFlipped; //put the second card in cardUp2
                _actionFrozen = true; //freeze the action (see Game1.cs Update())
                _flipTime = 0.75f; //how long to wait (in seconds) to flip the cards back over?
            }
        }

        //here is the dealer's always-check work
        public void Update(GameTime gametime, ScoreKeeper scoreKeeper)
        {
            //notice above how we set flip time to > 0 (0.75f above) when we need to?
            //if flip time is > 0
            if (_flipTime > 0)
            {
                _flipTime -= (float)gametime.ElapsedGameTime.TotalSeconds;  //wait
                if (_flipTime <= 0)
                {
                    _flipTime = 0; //quick error correcton if needed
                    if (!CheckMatch()) //if the up cards are NOT a match...?
                    {
                        _cardUp1.flip(this);  //flip them back over after the delay
                        _cardUp2.flip(this);
                    }
                    else //what? A match was found? 
                    {
                        _cardUp1.SetDestroyed();  //flag them to be "destroyed"
                        _cardUp2.SetDestroyed();
                        _numCards -= 2;
                        scoreKeeper.updateScore(1);
                    }
                    _cardUp1 = null;  //empty the dealer card slots for the next round
                    _cardUp2 = null;
                    _actionFrozen = !_actionFrozen; //unfreeze the action so the player can play
                }
            }
        }

        //check the card types - if they are they same, we have a match!!!
        public bool CheckMatch()
        {
            if (_cardUp1.GetCardType() == _cardUp2.GetCardType())
            {
                return true;
            }
            else
                return false;
        }

        //return the action frozen boolean, because, encapsulation
        public bool isActionFrozen() { return _actionFrozen; }

        //return game over because no cards are left
        public bool isGameOver() { return _numCards <= 0; }
    }
}
