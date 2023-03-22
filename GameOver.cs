using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace GME1011MemoryGameComplete
{
    internal class GameOver
    {
        private Texture2D _gameOverTexture;

        public GameOver(Texture2D gameOverTexture)
        {
            _gameOverTexture = gameOverTexture;
        }

        public void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Begin();
            spritebatch.Draw(_gameOverTexture, new Vector2(400-250, 300-252), Color.White);
            spritebatch.End();
        }
    }
}
