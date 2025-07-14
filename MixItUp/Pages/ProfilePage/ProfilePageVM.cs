using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MixItUp.Pages.ProfilePage
{
   public class ProfilePageVM : BaseViewModel
    {
        ////TODO : To Define Constructor..
        //public ProfilePageVM(INavigation _nav)
        //{
        //    Navigation = _nav;
        //    CocktailCommand = new Command(CocktailCommandAsync);
        //    FrozenCommand = new Command(FrozenCommandAsync);
        //    UserCreationCommand = new Command(UserCreationCommandAsync);
        //    cartclickedCommand = new Command(cartclickedCommandAsync);
        //    CabinetClickedCommand = new Command(CabinetClickedCommandAsync);
        //    RecipedetailClickedCommand = new Command(RecipedetailClickedCommandAsync);

        //}

        //#region Properties
        //private bool _IsCocktailOrngVsibl = true;
        //public bool IsCocktailOrngVsibl
        //{
        //    get { return _IsCocktailOrngVsibl; }
        //    set
        //    {
        //        if (_IsCocktailOrngVsibl != value)
        //        {
        //            _IsCocktailOrngVsibl = value;
        //            OnPropertyChanged("IsCocktailOrngVsibl");
        //        }
        //    }
        //}

        //private bool _IslblCktlOrng = true;
        //public bool IslblCktlOrng
        //{
        //    get { return _IslblCktlOrng; }
        //    set
        //    {
        //        if (_IslblCktlOrng != value)
        //        {
        //            _IslblCktlOrng = value;
        //            OnPropertyChanged("IslblCktlOrng");
        //        }
        //    }
        //}

        //private bool _IsCocktailBlkVsibl = false;
        //public bool IsCocktailBlkVsibl
        //{
        //    get { return _IsCocktailBlkVsibl; }
        //    set
        //    {
        //        if (_IsCocktailBlkVsibl != value)
        //        {
        //            _IsCocktailBlkVsibl = value;
        //            OnPropertyChanged("IsCocktailBlkVsibl");
        //        }
        //    }
        //}

        //private bool _IslblCktlblk = false;
        //public bool IslblCktlblk
        //{
        //    get { return _IslblCktlblk; }
        //    set
        //    {
        //        if (_IslblCktlblk != value)
        //        {
        //            _IslblCktlblk = value;
        //            OnPropertyChanged("IslblCktlblk");
        //        }
        //    }
        //}


        //private bool _IsCreateProfileOrngVsibl = true;
        //public bool IsCreateProfileOrngVsibl
        //{
        //    get { return _IsCreateProfileOrngVsibl; }
        //    set
        //    {
        //        if (_IsCreateProfileOrngVsibl != value)
        //        {
        //            _IsCreateProfileOrngVsibl = value;
        //            OnPropertyChanged("IsCreateProfileOrngVsibl");
        //        }
        //    }
        //}

        //private bool _IslblCreateProfileOrng = true;
        //public bool IslblCreateProfileOrng
        //{
        //    get { return _IslblCreateProfileOrng; }
        //    set
        //    {
        //        if (_IslblCreateProfileOrng != value)
        //        {
        //            _IslblCreateProfileOrng = value;
        //            OnPropertyChanged("IslblCreateProfileOrng");
        //        }
        //    }
        //}

        //private bool _IsCreateProfileBlkVsibl = false;
        //public bool IsCreateProfileBlkVsibl
        //{
        //    get { return _IsCreateProfileBlkVsibl; }
        //    set
        //    {
        //        if (_IsCreateProfileBlkVsibl != value)
        //        {
        //            _IsCreateProfileBlkVsibl = value;
        //            OnPropertyChanged("IsCreateProfileBlkVsibl");
        //        }
        //    }
        //}

        //private bool _IslblCreateProfileblk = false;
        //public bool IslblCreateProfileblk
        //{
        //    get { return _IslblCreateProfileblk; }
        //    set
        //    {
        //        if (_IslblCreateProfileblk != value)
        //        {
        //            _IslblCreateProfileblk = value;
        //            OnPropertyChanged("IslblCreateProfileblk");
        //        }
        //    }
        //}


        //private bool _IsProfileOrngVsibl = true;
        //public bool IsProfileOrngVsibl
        //{
        //    get { return _IsProfileOrngVsibl; }
        //    set
        //    {
        //        if (_IsProfileOrngVsibl != value)
        //        {
        //            _IsProfileOrngVsibl = value;
        //            OnPropertyChanged("IsProfileOrngVsibl");
        //        }
        //    }
        //}

        //private bool _IslblProfileOrng = true;
        //public bool IslblProfileOrng
        //{
        //    get { return _IslblProfileOrng; }
        //    set
        //    {
        //        if (_IslblProfileOrng != value)
        //        {
        //            _IslblProfileOrng = value;
        //            OnPropertyChanged("IslblProfileOrng");
        //        }
        //    }
        //}

        //private bool _IsProfileBlkVsibl = false;
        //public bool IsProfileBlkVsibl
        //{
        //    get { return _IsProfileBlkVsibl; }
        //    set
        //    {
        //        if (_IsProfileBlkVsibl != value)
        //        {
        //            _IsProfileBlkVsibl = value;
        //            OnPropertyChanged("IsProfileBlkVsibl");
        //        }
        //    }
        //}

        //private bool _IslblProfileBlkVsibl = false;
        //public bool IslblProfileBlkVsibl
        //{
        //    get { return _IslblProfileBlkVsibl; }
        //    set
        //    {
        //        if (_IslblProfileBlkVsibl != value)
        //        {
        //            _IslblProfileBlkVsibl = value;
        //            OnPropertyChanged("IslblProfileBlkVsibl");
        //        }
        //    }
        //}
        //#endregion

        //#region Command
        //public Command CocktailCommand { get; set; }
        //public Command FrozenCommand { get; set; }
        //public Command UserCreationCommand { get; set; }
        //public Command cartclickedCommand { get; set; }
        //public Command CabinetClickedCommand { get; set; }
        //public Command RecipedetailClickedCommand { get; set; }

        //#endregion

        //#region Methods
        //private async void CocktailCommandAsync(object obj)
        //{
        //    if(IsCocktailOrngVsibl == true)
        //    {
        //        IsCocktailOrngVsibl = false;
        //        IsCocktailBlkVsibl = true;
        //        IslblCktlOrng = false;
        //        IslblCktlblk = true;
        //    }
        //}
        //private async void FrozenCommandAsync(object obj)
        //{
        //    if (IsCocktailOrngVsibl == true)
        //    {
        //        IsCocktailOrngVsibl = false;
        //        IsCocktailBlkVsibl = true;
        //        IslblCktlOrng = false;
        //        IslblCktlblk = true;
        //    }
        //}
        //private async void UserCreationCommandAsync(object obj)
        //{
        //    if (IsCocktailOrngVsibl == true)
        //    {
        //        IsCocktailOrngVsibl = false;
        //        IsCocktailBlkVsibl = true;
        //        IslblCktlOrng = false;
        //        IslblCktlblk = true;
        //    }
        //}
        //private async void cartclickedCommandAsync(object obj)
        //{
        //    if (IsCocktailOrngVsibl == true)
        //    {
        //        IsCocktailOrngVsibl = false;
        //        IsCocktailBlkVsibl = true;
        //        IslblCktlOrng = false;
        //        IslblCktlblk = true;
        //    }
        //}
        //private async void CabinetClickedCommandAsync(object obj)
        //{
        //    if (IsCocktailOrngVsibl == true)
        //    {
        //        IsCocktailOrngVsibl = false;
        //        IsCocktailBlkVsibl = true;
        //        IslblCktlOrng = false;
        //        IslblCktlblk = true;
        //    }
        //}
        //private async void RecipedetailClickedCommandAsync(object obj)
        //{
        //    if (IsCocktailOrngVsibl == true)
        //    {
        //        IsCocktailOrngVsibl = false;
        //        IsCocktailBlkVsibl = true;
        //        IslblCktlOrng = false;
        //        IslblCktlblk = true;
        //    }
        //}
        //#endregion
    }
}
