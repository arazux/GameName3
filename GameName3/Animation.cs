using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameName3
{
    class Animation
    {
        private Texture2D[] frames;
        private float time;
        private float delay;
        private int currentFrame;

        private int timesPlayed;

        public Animation(){}

        public Animation(Texture2D[] frames, float delay)
        {
            this.frames = frames;
            this.delay = delay;
            time = 0;
            timesPlayed = 0;
        }

#region Private Members
        private void step()
        {
            time -= delay;
            currentFrame++;
            if (currentFrame == frames.Length)
            {
                currentFrame = 0;
                timesPlayed++;
            }
        }
#endregion

#region Public Members
        public void update(float delta)
        {
            if (delay <= 0)
            {
                return;
            }
            time += delta;
            while (time >= delay)
            {
                step();
            }
        }

        public void setDelay(float delay){
            this.delay = delay;
        }

        public void setCurrentFrame(int i )
        {
            if (i < frames.Length)
            {
                currentFrame = i;
            }
        }

        public Texture2D getFrame()
        {
            return frames[currentFrame];
        }

        public void resetTimesPlayed()
        {
            timesPlayed = 0;
            currentFrame = 0;
        }

        public int getTimesPlayed(){
            return timesPlayed;
        }

        public Boolean hasPlayedOnce(){
            return timesPlayed > 0;
        }

    }
    #endregion
}
