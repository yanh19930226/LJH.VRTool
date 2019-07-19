using Abp.Specifications;
using LJH.VRTool.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LJH.VRTool.Users.Specification
{
    public class UserSearchSpecification : Specification<User>
    {
        public string KeyWord { get; }
        public DateTime? TimeMin { get;  }
        public DateTime? TimeMax { get; }
        public UserSearchSpecification(string KeyWord, DateTime? TimeMin, DateTime? TimeMax)
        {
            this.KeyWord = KeyWord;
            this.TimeMin = TimeMin;
            this.TimeMax = TimeMax;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (q) => (q.FullName.Contains(KeyWord)||q.UserName.Contains(KeyWord)||q.Name.Contains(KeyWord)|| q.CreationTime>= TimeMin&& q.CreationTime<= TimeMax);
        }
    }
}
