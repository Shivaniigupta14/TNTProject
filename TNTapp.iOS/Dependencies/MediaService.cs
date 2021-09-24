using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using TNTapp.Interfaces;
using TNTapp.iOS.Dependencies;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaService))]

namespace TNTapp.iOS.Dependencies
{
    public class MediaService : IMediaService
    {
        //Define Local Variable
        #region Constructor
        public MediaService()
        {

        }

        #endregion
       

        public async System.Threading.Tasks.Task<byte[]> GetMediaInBytes(string filePath)
        {
            var imgbyte = File.ReadAllBytes(filePath);
            ImageFromByteArray(imgbyte);
            return File.ReadAllBytes(filePath);
        }
        
      
        public string ViewMediaInPdf(byte[] fileStream, string fileName)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task<byte[]> ResizeImage(byte[] imageStream, float width, float height)
        {
            //Create image using byte array...
            UIImage originalImage = ImageFromByteArray(imageStream);

            //Send image for roation... if its not up...
            byte[] rotatedimg = RotateImage(originalImage);

            //Create a new image with the byte array returned from RotateImage()...

            UIImage newimage = ImageFromByteArray(rotatedimg);
            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,(int)width, (int)height, 8,(int)(4 * width), CGColorSpace.CreateDeviceRGB(), CGImageAlphaInfo.PremultipliedFirst))
            {
                CGRect imageRect = new CGRect(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, newimage.CGImage);

                //UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);
                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage());

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        //Create a Method to set orientation of image...
        private byte[] RotateImage(UIImage image)
        {
            UIImage imageToReturn = null;
            if (image.Orientation == UIImageOrientation.Up)
            {
                imageToReturn = image;
            }
            else
            {
                CGAffineTransform transform = CGAffineTransform.MakeIdentity();

                switch (image.Orientation)
                {
                    case UIImageOrientation.Down:
                    case UIImageOrientation.DownMirrored:
                        transform.Rotate((float)Math.PI);
                        transform.Translate(image.Size.Width, image.Size.Height);
                        break;

                    case UIImageOrientation.Left:
                    case UIImageOrientation.LeftMirrored:
                        transform.Rotate((float)Math.PI / 2);
                        transform.Translate(image.Size.Width, 0);
                        break;

                    case UIImageOrientation.Right:
                    case UIImageOrientation.RightMirrored:
                        transform.Rotate(-(float)Math.PI / 2);
                        transform.Translate(0, image.Size.Height);
                        break;
                    case UIImageOrientation.Up:
                    case UIImageOrientation.UpMirrored:
                        break;
                }

                switch (image.Orientation)
                {
                    case UIImageOrientation.UpMirrored:
                    case UIImageOrientation.DownMirrored:
                        transform.Translate(image.Size.Width, 0);
                        transform.Scale(-1, 1);
                        break;

                    case UIImageOrientation.LeftMirrored:
                    case UIImageOrientation.RightMirrored:
                        transform.Translate(image.Size.Height, 0);
                        transform.Scale(-1, 1);
                        break;
                    case UIImageOrientation.Up:
                    case UIImageOrientation.Down:
                    case UIImageOrientation.Left:
                    case UIImageOrientation.Right:
                        break;
                }

                //now draw image
                using (var context = new CGBitmapContext(IntPtr.Zero,
                                                        (int)image.Size.Width,
                                                        (int)image.Size.Height,
                                                        image.CGImage.BitsPerComponent,
                                                        image.CGImage.BytesPerRow,
                                                        image.CGImage.ColorSpace,
                                                        image.CGImage.BitmapInfo))
                {
                    context.ConcatCTM(transform);
                    switch (image.Orientation)
                    {
                        case UIImageOrientation.Left:
                        case UIImageOrientation.LeftMirrored:
                        case UIImageOrientation.Right:
                        case UIImageOrientation.RightMirrored:
                            // Grr...
                            context.DrawImage(new RectangleF(PointF.Empty, new SizeF((float)image.Size.Height, (float)image.Size.Width)), image.CGImage);
                            break;
                        default:
                            context.DrawImage(new RectangleF(PointF.Empty, new SizeF((float)image.Size.Width, (float)image.Size.Height)), image.CGImage);
                            break;
                    }

                    using (var imageRef = context.ToImage())
                    {
                        imageToReturn = new UIImage(imageRef);
                    }
                }
            }

            using (NSData imageData = imageToReturn.AsJPEG())
            {
                Byte[] byteArray = new Byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, byteArray, 0, Convert.ToInt32(imageData.Length));
                return byteArray;
            }
        }
        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
                var wsize = image.Size.Width;
                var wheight = image.Size.Height;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Image load failed: " + ex.Message);
                return null;
            }
            return image;
        }
       

       

       
    }
}