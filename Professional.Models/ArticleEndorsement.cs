using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Models
{
    public class ArticleEndorsement
    {
        public int ID { get; set; }

        [Required]
        [Range(0, 10)]
        public int Value { get; set; }
        public string Comment { get; set; }

        [Required]
        public int EndorsedArticleID { get; set; }

        public virtual User EndorsedArticle { get; set; }

        [Required]
        public int EndorsingUserID { get; set; }

        public virtual User EndorsingUser { get; set; }
    }
}
