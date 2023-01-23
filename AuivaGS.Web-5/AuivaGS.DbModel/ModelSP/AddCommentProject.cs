

 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuivaGS.DbModel.ModelSP
    {
        public partial class AddCommentProject
        {
            public int ProjectID { get; set; }
            public string CommentProject { get; set; }
        }
    }