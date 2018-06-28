/* 
 * Copyright (C) 2018 Kristofer Dovstam - All Rights Reserved
 */
 
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WarsUtils
{
    class XNAShape
    {
        Texture2D pixel;
        GraphicsDevice myDevice;

        public XNAShape(GraphicsDevice myDevice)
        {
            this.myDevice = myDevice;
            CreatePixelTexture();
            myDevice.DeviceReset += new EventHandler<EventArgs>(myDevice_DeviceReset);
        }      //Each time the device is reset, we need to recreate the Texture - otherwise it crashes in windowed mode

        void myDevice_DeviceReset(object sender, EventArgs e)
        {
            CreatePixelTexture();
        }

        //Creates a white 1*1 Texture that is used for the lines by scaling and rotating it      
        public void CreatePixelTexture()
        {
            int TargetWidth = 1;
            int TargetHeight = 1;
            RenderTarget2D LevelRenderTarget = new RenderTarget2D(myDevice, TargetWidth, TargetHeight, true, //XNA3140: used to be 1 should be false?
                myDevice.PresentationParameters.BackBufferFormat, myDevice.PresentationParameters.DepthStencilFormat,
                myDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.PreserveContents);
// XNA3140: What to do about the DepthStencilBuffer? Just ignore?
//            RenderTarget2D stencilBuffer = new RenderTarget2D(myDevice, TargetWidth, TargetHeight, true, //XNA3140: should be true or false?
//                SurfaceFormat.Color, DepthFormat.Depth24Stencil8, myDevice.PresentationParameters.MultiSampleCount, RenderTargetUsage.DiscardContents);  //XNA3140: should be discard or preserv content?
            myDevice.SetRenderTarget(LevelRenderTarget);          // Cache the current depth buffer          
//            DepthStencilBuffer old = myDevice.DepthStencilBuffer;          // Set our custom depth buffer          
//            myDevice.DepthStencilBuffer = stencilBuffer;
            myDevice.Clear(Color.White);
            myDevice.SetRenderTarget(null);          // Reset the depth buffer          
//            myDevice.DepthStencilBuffer = old;
// END XNA3140: What to do about the DepthStencilBuffer? Just ignore?
            pixel = LevelRenderTarget;
        }      //Calculates the distances and the angle and than draws a line      
        public void DrawLine(SpriteBatch sprite, Vector2 start, Vector2 end, Color color)
        {
            int distance = (int)Vector2.Distance(start, end);
            Vector2 connection = end - start;
            Vector2 baseVector = new Vector2(1, 0);
            float alpha = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
            if (pixel != null)
                sprite.Draw(pixel, new Rectangle((int)start.X, (int)start.Y, distance, 1),
                    null, color, alpha, new Vector2(0, 0), SpriteEffects.None, 0);
        }      //Draws a rect with the help of DrawLine      
        public void DrawRect(SpriteBatch sprite, Rectangle rect, Color color)
        {
            // | left          
            DrawLine(sprite, new Vector2(rect.X, rect.Y), new Vector2(rect.X, rect.Y + rect.Height), color);
            // - top          
            DrawLine(sprite, new Vector2(rect.X, rect.Y), new Vector2(rect.X + rect.Width, rect.Y), color);
            // - bottom          
            DrawLine(sprite, new Vector2(rect.X, rect.Y + rect.Height),
                new Vector2(rect.X + rect.Width, rect.Y + rect.Height), color);
            // | right          
            DrawLine(sprite, new Vector2(rect.X + rect.Width, rect.Y),
                new Vector2(rect.X + rect.Width, rect.Y + rect.Height), color);
        }
    }
}

