﻿using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blog.Entities.Models.Concrete
{
    [Serializable()]
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; } 
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
        public string PostImageURL { get; set; }



        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    }
}
