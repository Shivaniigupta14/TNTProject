using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TNTapp.Droid.Dependencies;
using TNTapp.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserContactService))]

namespace TNTapp.Droid.Dependencies
{
    public interface UserContactService : IUserContactService
    {

    }
}