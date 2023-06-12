using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Interfaceses
{
    public interface IFriendRepository
    {
        public int Create(FriendEntity friendEntity);
        public IEnumerable<FriendEntity> FindAllByUserId(int userId);
        public int Delete(int id);
    }
}
