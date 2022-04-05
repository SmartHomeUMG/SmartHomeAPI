using System.ComponentModel.DataAnnotations;

namespace SmartBuilding.Models{
    public class HomeVisitor{

        [Key]
        public int Id {get;set;}
        private string _guessPhoto;
        private DateTime _captureDate;
        public string GuessPhoto
        {
            get => this._guessPhoto;
            set
            {
                if (this._guessPhoto == null){
                    this._guessPhoto = value;
                }
            }
        }

        public DateTime CaptureDate
        {
            get => this._captureDate;
            set 
            {
                if(this._captureDate == null)
                {
                   this._captureDate = DateTime.Now;
                }
            } 
        }

    }
}