﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Entities
{
    public class FriendEntity
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int friendId { get; set; }
    }
}
