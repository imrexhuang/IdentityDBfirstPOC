using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityDBfirstPOC.Models.ViewModels
{
    public class SpendingTrackerViewModel
    {
        [Key]
        public Guid ID { get; set; }

        [DisplayName("類別")]
        public int TYPE { get; set; }

        [DisplayName("日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DATE { get; set; }

        [DisplayName("金額")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public int AMOUMT { get; set; }

        [DisplayName("備註")]
        public String REMARK { get; set; }
    }

    //public enum TypeEnum
    //{
    //    [Display(Name = "收入")]
    //    收入 = 'D',

    //    [Display(Name = "支出")]
    //    支出 = 'C'
    //}
}