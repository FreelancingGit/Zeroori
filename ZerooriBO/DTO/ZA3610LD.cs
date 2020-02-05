using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZerooriBO
{
    /// <summary>
    /// Sign Up Page
    /// User Creation
    /// </summary>
    public class ZA3610LD
    {
        int? _PropADMastID = null;
        ZA3000D _UserData = new ZA3000D();
        ZA3210DCol _BedroomCol = new ZA3210DCol();
        ZA3210DCol _BathRoomCol = new ZA3210DCol();
        ZA3210DCol _SizeCol = new ZA3210DCol();
        ZA3210DCol _FurnishedCol = new ZA3210DCol();
        ZA3210DCol _ApartmentForCol = new ZA3210DCol();
        ZA3210DCol _RentIsPaidCol = new ZA3210DCol();
        ZA3210DCol _ListedByCol = new ZA3210DCol();
        ZA3210DCol _CategoryCol = new ZA3210DCol();
        ZA3610SD _SelectedData = new ZA3610SD();

        public ZA3000D UserData { get => _UserData; set => _UserData = value; }
        public int? PropADMastID { get => _PropADMastID; set => _PropADMastID = value; }
        public ZA3210DCol BedroomCol { get => _BedroomCol; set => _BedroomCol = value; }
        public ZA3210DCol BathRoomCol { get => _BathRoomCol; set => _BathRoomCol = value; }
        public ZA3210DCol SizeCol { get => _SizeCol; set => _SizeCol = value; }
        public ZA3210DCol FurnishedCol { get => _FurnishedCol; set => _FurnishedCol = value; }
        public ZA3210DCol ApartmentForCol { get => _ApartmentForCol; set => _ApartmentForCol = value; }
        public ZA3210DCol RentIsPaidCol { get => _RentIsPaidCol; set => _RentIsPaidCol = value; }
        public ZA3210DCol ListedByCol { get => _ListedByCol; set => _ListedByCol = value; }
        public ZA3210DCol CategoryCol { get => _CategoryCol; set => _CategoryCol = value; }
        public ZA3610SD SelectedData { get => _SelectedData; set => _SelectedData = value; }
    }


   


}
