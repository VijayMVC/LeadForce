﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebCounter.DataAccessLayer
{
    [MetadataType(typeof(tbl_RequirementComplexityMetaData))]
    public partial class tbl_RequirementComplexity
    {
        public class tbl_RequirementComplexityMetaData
        {
            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object ID { get; set; }

            [ScaffoldColumn(false)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [ReadOnly(true)]
            public object SiteID { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Название")]
            [ScaffoldColumn(true)]
            [UIHint("Settings", null, "ShowInEdit", true)]
            [Required(AllowEmptyStrings = false)]
            [ReadOnly(false)]
            public object Title { get; set; }
        }
    }
}
