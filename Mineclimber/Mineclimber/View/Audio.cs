using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Mineclimber.View
{
    class Audio
    {

        private SoundEffect Jumpsound;
        private SoundEffect HitSound;

        /*
        private SoundEffect SartingScreenMusic;
        */
        private Song GameMusic;

        public Audio(ContentManager Content)
        {
            Jumpsound = Content.Load<SoundEffect>("jump");
            HitSound = Content.Load<SoundEffect>("hit");
            GameMusic = Content.Load<Song>("Ascending");
        }

        internal void PlayGameMusic()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(GameMusic);
        }

        internal void StopGameMusic()
        {
            MediaPlayer.Stop();
        }

        internal void PlayJumpSound()
        {
            Jumpsound.Play();
        }

        internal void PlayHitSound()
        {
            HitSound.Play();
        }
    }
}
