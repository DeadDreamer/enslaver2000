using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnslaverCore;

namespace EnslaverFrontEnd.Logic
{
    public enum UserStates : int
    {
        Fine = 0,
        Smiling = 1,
        EyesNotFound = 2,
        HeadNotFound = 3
    }

    class User : IEquatable<User>
    {
        public string name;
        public int headFound = 0;
        public int eyesFound = 0;
        public int smiling = 0;

        public User(string name)
        {
            this.name = name;
        }

        bool IEquatable<User>.Equals(User other)
        {
            return other.name == this.name;
        }
    }

    class UserStatus
    {
        private List<User> users=new List<User>();
        int frameCounter;
        DateTime last = DateTime.Now;
        const int accuracyMargin = 90;

        public void CheckFrame(List<HeadInformation> heads)
        {
            foreach (HeadInformation h in heads)
            {
                foreach (User u in users)
                {
                    frameCounter++;
                    if (u.name == h.Name)
                    {
                        u.headFound++;
                        if (h.IsSmile) u.smiling++;
                        if (h.Eye1 != null || h.Eye2 != null) u.eyesFound++;
                    }
                }
            }
        }

        public void ResetCounters()
        {
            foreach (User u in users)
            {
                u.smiling = 0;
                u.headFound = 0;
                u.eyesFound = 0;
            }
            frameCounter = 0;
            last = DateTime.Now;
        }

        public UserStates GetUserStatus(string owner)
        {
            if (users != null)
            {
                User ownerUser = users.Find(c => c.name == owner);
                if (ownerUser != null)
                {
                    return GetUserStatus(ownerUser);
                }
            }
            return UserStates.HeadNotFound;
        }

        public UserStates GetUserStatus(User u)
        {
            if (frameCounter != 0)
            {
                if (((double)u.headFound / frameCounter) * 100 > accuracyMargin)
                {
                    if (((double)u.eyesFound / frameCounter) * 100 > accuracyMargin)
                    {
                        if (((double)u.smiling / frameCounter) * 100 > accuracyMargin) return UserStates.Smiling;
                        else return UserStates.Fine;
                    }
                    else return UserStates.EyesNotFound;
                }
                else return UserStates.HeadNotFound;
            }
            return UserStates.Fine;
        }
    }
}
